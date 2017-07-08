using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
	public float maxHitPoints = 20f; // you a squishy bish
	protected float recoverableHP = 20f;
	protected float absoluteHP = 20;
	public AnimationCurve aOverRCurve;

	public float maxStamina = 100f;
	protected float recoverableStamina = 100f;
	protected float absoluteStamina = 100f;

	public float recoverySpeed = 20; // hp/min

	public float RecoverableHP { get { return recoverableHP; } }

	public float AbsoluteHP { get { return absoluteHP; } }

	public bool StaminaReady { get { return absoluteStamina > 0; } }

	public float AbsoluteStamina { get { return absoluteStamina; } }

	public float stRecoveryWait = 0;

	// Update is called once per frame
	public virtual void Update()
	{
		if (absoluteHP < 0)
		{
			Die();
		}
		else
		{
			if (absoluteHP < recoverableHP) // recover hp, reduce stamina
			{
				float recovery = recoverySpeed / 60 * Time.deltaTime; // heal by recoverySpeed per minute
				absoluteHP += recovery;

				recoverableStamina -= recovery;
				absoluteStamina -= recovery;
			}
			else if (recoverableStamina < maxStamina) // recover stamina
			{
				recoverableStamina += 20f / 60 * Time.deltaTime;
			}

			if (absoluteStamina < recoverableStamina)
			{
				if (stRecoveryWait > 0)
				{
					stRecoveryWait -= Time.deltaTime;
				}
				else
				{
					absoluteStamina += 1800f / 60 * Time.deltaTime;

				}
			}

			if (recoverableHP < maxHitPoints && recoverableStamina >= maxStamina) // recover recoverableHP
			{
				recoverableHP += 8f / 60 * Time.deltaTime;
			}

		}
	}

	public virtual void Damage(float dmg, Vector3 pushDir, Vector3 atkPos)
	{
		float recDmg = 0;

		//// based off a-hp/r-hp
		//recDmg = Mathf.Lerp(0.5f, 0.1f, absoluteHP / recoverableHP);
		//recDmg *= dmg;

		// based off r-st/max-st
		//recDmg = 1 - (recoverableStamina / maxStamina);
		//print(recDmg);
		//recDmg *= dmg;

		//absoluteHP -= dmg;
		//recoverableHP -= recDmg;

		// based off a-hp/r-hp, damage is applied accross both evenly
		float absDmg = 0;
		//recDmg = Mathf.Lerp(0.5f, 0, absoluteHP / recoverableHP);
		recDmg = aOverRCurve.Evaluate(absoluteHP / recoverableHP);
		absDmg = 1 - recDmg;

		absoluteHP -= absDmg * dmg;
		recoverableHP -= recDmg * dmg;
	}

	public virtual void Die()
	{

	}
}
