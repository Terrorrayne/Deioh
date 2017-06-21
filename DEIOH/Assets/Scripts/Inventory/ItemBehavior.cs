using UnityEngine;

public abstract class ItemBehavior : MonoBehaviour
{
	//controls how this item works once equipped

	public abstract void EquipThisItem();

	public abstract void PrimaryBehavior();

	public abstract void SecondaryBehavior();

	public abstract void UsableBehavior();

	public virtual void RemoveSelf()
	{
		Destroy(gameObject);
	}
}