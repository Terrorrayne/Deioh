using System;
using UnityEngine;

public class IB_Sword : ItemBehavior
{

	public Animator animator;

	public override void EquipThisItem() // used for initialization of item
	{
		throw new NotImplementedException();
	}

	public override void PrimaryBehavior()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			// get forward dir

			// apply hitbox and play animation
			animator.SetTrigger("attack");


			// movement change

		}
	}

	public override void SecondaryBehavior()
	{
		throw new NotImplementedException();
	}

	public override void UsableBehavior()
	{
		throw new NotImplementedException();
	}

	public override void RemoveSelf()
	{
		base.RemoveSelf();
	}
}
