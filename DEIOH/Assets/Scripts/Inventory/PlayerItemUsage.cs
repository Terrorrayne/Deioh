using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class PlayerItemUsage : MonoBehaviour
{

	// this allows player to use items in their inventory

	InventoryItem PrimaryItem; // primary item - sword
	InventoryItem SecondaryItem; // secondary item - shield - changes usage of primary in some way, or just does its own thing
	InventoryItem UsableItem; // usable - healthpack/consumable

	Inventory inventory;

	public RectTransform itemWheel;
	Image[] wheelImages;
	public RectTransform equippedItems;
	Image[] equipImages;

	public Sprite emptyItemSprite;

	void Awake()
	{
		inventory = GetComponent<Inventory>();

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
		if (equippedItems == null)
		{
			print("epuippedItems is unassigned");
			weGood = false;
		}

		if (!weGood)
			return;

		// need inventory, itemWheel, and equippedItems assigned for stuff to be functional
		wheelImages = new Image[inventory.numItemSlots];
		equipImages = new Image[3];

		for (int i = 0; i < wheelImages.Length; i++)
		{
			wheelImages[i] = itemWheel.GetChild(i).GetComponent<Image>();
		}

		UpdateInventoryImages();
	}

	// Update is called once per frame
	void Update()
	{
		// if we press the item button
		// bring up item menu
		// allow us to select one of the items
		// set that item as Primary/secondary/whatever

		// alternatively, if we scroll we auto select an item, then are given the chance to equip item. by default we use it tho

		if (Input.GetButtonDown("InventoryMenu"))
		{
			// open/close inventory menu
		}
	}

	public void UpdateInventoryImages()
	{
		for (int i = 0; i < inventory.items.Length; i++)
		{
			// cycle through all items
			if (inventory.items[i] == null)
			{
				wheelImages[i].sprite = emptyItemSprite;
			}
			else
			{
				wheelImages[i].sprite = inventory.items[i].itemSprite;
			}
		}
	}
}
