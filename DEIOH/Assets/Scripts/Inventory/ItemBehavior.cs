using UnityEngine;

public abstract class ItemBehavior : MonoBehaviour, IButton
{
	//controls how this item works once equipped

	public InventoryItem thisItem;

	protected bool isEquipped = false;

	public GameObject world;
	public GameObject equip;
	public Rigidbody myrigidbody;
	public Collider mycollider;

	public bool IsEquipped
	{
		get { return isEquipped; }

		set
		{
			isEquipped = value;
			world.SetActive(!isEquipped);
			equip.SetActive(isEquipped);
			myrigidbody.useGravity = !isEquipped;
			mycollider.enabled = !isEquipped;
		}
	}

	public abstract void EquipThisItem(GameObject characterUsingItem);

	public abstract void PrimaryBehavior();

	public abstract void SecondaryBehavior();

	public abstract void UsableBehavior();

	public virtual void Start()
	{

		IsEquipped = IsEquipped;
	}

	public virtual void RemoveSelf()
	{
		Destroy(gameObject);
	}

	public void ButtonPress(PlayerButtonInteraction player)
	{
		if (!IsEquipped)
			player.GetComponent<Inventory>().AddItem(thisItem);
		Destroy(gameObject);
	}

	public bool ButtonIsActive()
	{
		return true;
	}


}