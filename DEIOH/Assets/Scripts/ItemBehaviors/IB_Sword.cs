using UnityEngine;

public class IB_Sword : ItemBehavior
{

	public Animator spriteAnimator;
	PlayerHealth myHealth;
	Animator myAnimator;

	public Transform hitboxDefinition;
	public LayerMask layermask;
	public float damage = 9f;

	float lastAttackTime = 0;

	public override void Start()
	{
		base.Start();
		myHealth = GetComponentInParent<PlayerHealth>();
		myAnimator = transform.parent.GetComponentInChildren<Animator>();
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
				spriteAnimator.SetTrigger("attack");

				// which animation do we play
				// is this an initial attack or is it a sequential one
				//if (Time.time - lastAttackTime > 0.25f)
				//{
				//	// start off an attack
				//	myAnimator.Play("AttackIn");
				//	spriteAnimator.transform.localScale = Vector3.one * 2.5f;
				//}
				//else
				//{
				//	// subsequent attack
				//	// flip shit

				spriteAnimator.transform.localScale = new Vector3(-spriteAnimator.transform.localScale.x, 2.5f, 2.5f); // flip the sprite
				if (spriteAnimator.transform.localScale.x < 0)
				{
					myAnimator.Play("AttackOut");
				}
				else
				{
					myAnimator.Play("AttackIn");
				}
				//}
				//lastAttackTime = Time.time; // set this so subsequent attacks will animate correctly, kinda important



				Collider[] hitObj = Physics.OverlapBox(hitboxDefinition.transform.position, hitboxDefinition.localScale * 0.5f, hitboxDefinition.rotation, layermask);
				for (int i = 0; i < hitObj.Length; i++)
				{
					Vector3 dir = hitObj[i].transform.position - transform.position;
					hitObj[i].GetComponent<IDamageable>().Damage(damage, dir, dir);
				}


				// movement change


				myHealth.DrainStamina(5, 0.35f);
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
