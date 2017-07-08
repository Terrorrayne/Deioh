using UnityEngine;

public class IB_Shield : ItemBehavior
{
	public Renderer shieldGFX;
	CharacterMovement myMovement;

	public override void EquipThisItem(GameObject characterUsingItem)
	{
		myMovement = characterUsingItem.GetComponent<CharacterMovement>();
		shieldGFX.enabled = false;

	}

	public override void PrimaryBehavior()
	{
	}

	public override void SecondaryBehavior()
	{
		if (Input.GetButtonDown("Fire2")) // im holding rmb
		{
			// hold the shield up
			// slow movement speed
			//and idk.
			myMovement.movementMultiplier = 0.5f;
			shieldGFX.enabled = true;
		}

		if (Input.GetButtonUp("Fire2"))
		{
			myMovement.movementMultiplier = 1f;
			shieldGFX.enabled = false;
		}
	}

	public override void UsableBehavior()
	{
	}

	public override void RemoveSelf()
	{
		base.RemoveSelf();
	}


}
