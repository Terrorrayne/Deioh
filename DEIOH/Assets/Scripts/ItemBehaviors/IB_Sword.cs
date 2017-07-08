using UnityEngine;

public class IB_Sword : ItemBehavior
{

	public Animator animator;
	public PlayerHealth myHealth;

	public Transform hitboxDefinition;
	public LayerMask layermask;
	public float damage = 9f;

	public override void Start()
	{
		base.Start();
		myHealth = GetComponentInParent<PlayerHealth>();
	}

	public override void EquipThisItem(GameObject characterUsingItem) // used for initialization of item
	{
	}

	public override void PrimaryBehavior()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			if (myHealth.StaminaReady)
			{
				// get forward dir

				// apply hitbox and play animation
				animator.SetTrigger("attack");

				Collider[] hitObj = Physics.OverlapBox(hitboxDefinition.transform.position, hitboxDefinition.localScale * 0.5f, hitboxDefinition.rotation, layermask);
				for (int i = 0; i < hitObj.Length; i++)
				{
					Vector3 dir = hitObj[i].transform.position - transform.position;
					hitObj[i].GetComponent<IDamageable>().Damage(damage, dir, dir);
				}


				// movement change


				myHealth.DrainStamina(20, 0.35f);
			}
		}
	}

	public override void SecondaryBehavior()
	{
	}

	public override void UsableBehavior()
	{
	}

	public override void RemoveSelf()
	{
		base.RemoveSelf();
	}
}
