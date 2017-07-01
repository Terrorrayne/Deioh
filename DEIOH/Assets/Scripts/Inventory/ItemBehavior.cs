using System;
using UnityEngine;

public abstract class ItemBehavior : MonoBehaviour, IButton
{
	//controls how this item works once equipped

	public InventoryItem thisItem;

	public abstract void EquipThisItem();

	public abstract void PrimaryBehavior();

	public abstract void SecondaryBehavior();

	public abstract void UsableBehavior();

	public virtual void RemoveSelf()
	{
		Destroy(gameObject);
	}

	public void ButtonPress(PlayerButtonInteraction player)
	{
		throw new NotImplementedException();
	}

	public bool ButtonIsActive()
	{
		throw new NotImplementedException();
	}
}