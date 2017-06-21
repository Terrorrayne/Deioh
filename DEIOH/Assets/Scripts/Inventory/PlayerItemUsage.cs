using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class PlayerItemUsage : MonoBehaviour
{

	// this allows player to use items in their inventory

	int[] equipItems = new int[3];
	bool inventoryMenuOpen = false;

	InventoryItem PrimaryItem // primary item - sword
	{ get { return inventory.items[equipItems[0]]; } }
	InventoryItem SecondaryItem // secondary item - shield - changes usage of primary in some way, or just does its own thing
	{ get { return inventory.items[equipItems[1]]; } }
	InventoryItem UsableItem // usable - healthpack/consumable
	{ get { return inventory.items[equipItems[2]]; } }

	ItemBehavior PrimaryEquip, SecondaryEquip, UsableEquip; // references to the actual items equipped in the game

	Inventory inventory;

	public RectTransform itemWheel;
	Image[] itemImages;
	public RectTransform equipWheel;
	Image[] equipImages;

	public Text itemLabel;

	int currentItemSlot = 0;
	int currentEquipSlot = 0;

	public Sprite emptyItemSprite;

	void Awake()
	{
		inventory = GetComponent<Inventory>();

		//for (int i = 0; i < equipItems.Length; i++) // only do this if want the option to not equip anything
		//{
		//	equipItems[i] = -1;
		//}

		bool weGood = true;
		if (inventory == null)
		{
			print("inventory is unassigned");
			weGood = false;
		}
		if (itemWheel == null)
		{
			print("itemWheel is unassigned");
			weGood = false;
		}
		if (equipWheel == null)
		{
			print("equippedWheel is unassigned");
			weGood = false;
		}

		if (!weGood)
			return;

		// need inventory, itemWheel, and equippedItems assigned for stuff to be functional
		itemImages = new Image[inventory.numItemSlots];
		equipImages = new Image[3];

		for (int i = 0; i < itemImages.Length; i++) // grab all images
		{
			itemImages[i] = itemWheel.GetChild(i).GetComponent<Image>();
		}
		for (int i = 0; i < equipImages.Length; i++) // grab all images
		{
			equipImages[i] = equipWheel.GetChild(i).GetComponent<Image>();
		}

		UpdateInventoryImages();
		itemWheel.gameObject.SetActive(false);
		equipWheel.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		// if we press the item button
		// bring up item menu
		// allow us to select one of the items
		// set that item as Primary/secondary/whatever

		// alternatively, if we scroll we auto select an item, then are given the chance to equip item. by default we use it tho

		if (Input.GetButtonDown("InventoryMenu") || Input.GetKeyDown(KeyCode.Alpha2))
		{
			// open/close inventory menu
			inventoryMenuOpen = !inventoryMenuOpen;
			itemWheel.gameObject.SetActive(inventoryMenuOpen);
			equipWheel.gameObject.SetActive(inventoryMenuOpen);
			if (!inventoryMenuOpen)
			{
				UpdateEquippedItems();
			}
		}

		// if the menu is open, enable navigation controls
		if (inventoryMenuOpen)
		{
			Vector2 scroll = Input.mouseScrollDelta;
			scroll.y = -scroll.y;
			if (Input.GetKeyDown(KeyCode.Alpha1))
				scroll.x -= 1;
			if (Input.GetKeyDown(KeyCode.Alpha3))
				scroll.x += 1;


			currentEquipSlot = (currentEquipSlot + (int)scroll.x) % 3;
			if (currentEquipSlot < 0)
				currentEquipSlot = 2;

			currentItemSlot = equipItems[currentEquipSlot];

			currentItemSlot = (currentItemSlot + (int)scroll.y) % 8;
			if (currentItemSlot < 0)
				currentItemSlot = 7;

			equipItems[currentEquipSlot] = currentItemSlot;
			UpdateEquipImages();

			// todo: get label to work
			//itemLabel.text = inventory.items[equipItems[currentEquipSlot]].displayName;

			// update positions
			itemWheel.localPosition = Vector3.up * currentItemSlot * 45;
			equipWheel.localPosition = Vector3.left * currentEquipSlot * 60;

		}

		if (PrimaryEquip != null)
		{
			PrimaryEquip.PrimaryBehavior();
		}
	}

	public void UpdateInventoryImages()
	{
		for (int i = 0; i < inventory.items.Length; i++)
		{
			// cycle through all items
			if (inventory.items[i] == null)
			{
				itemImages[i].sprite = emptyItemSprite;
			}
			else
			{
				itemImages[i].sprite = inventory.items[i].itemSprite;
			}
		}

		UpdateEquipImages();
	}

	public void UpdateEquipImages()
	{

		for (int i = 0; i < equipItems.Length; i++)
		{
			//if (equipItems[i] == -1)
			//{
			//	equipImages[i].sprite = emptyItemSprite;
			//}
			//else
			//{
			//	equipImages[i].sprite = inventory.items[i].itemSprite;
			//}

			if (inventory.items[equipItems[i]] == null)
			{
				equipImages[i].sprite = emptyItemSprite;
			}
			else
			{
				equipImages[i].sprite = inventory.items[equipItems[i]].itemSprite;
			}
		}
	}

	public void UpdateEquippedItems()
	{
		// this swaps out equipped items so we can actually use them


		// basic rule of this system, is instanciate new objects for each item, and let those objects handle everything they need to handle on their own
		// just feed them input and nothing else

		// remove equipped items

		if (PrimaryEquip != null)
		{
			PrimaryEquip.RemoveSelf();
		}
		if (SecondaryEquip != null)
		{
			SecondaryEquip.RemoveSelf();
		}
		if (UsableEquip != null)
		{
			UsableEquip.RemoveSelf();
		}

		// then instanciate the new ones

		if (PrimaryItem.equippablePrefab != null)
		{
			PrimaryEquip = Instantiate(PrimaryItem.equippablePrefab, transform.position, transform.rotation, transform).GetComponent<ItemBehavior>();
		}
	}
}
