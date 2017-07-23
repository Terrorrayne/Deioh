using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
	public float startingHP = 20f; // you a squishy bish
	protected float maximumHP = 20f;
	protected float currentHP = 20;
	public AnimationCurve cOverMCurve;

	public float startingStamina = 100f;
	protected float maximumStamina = 100f;
	protected float currentStamina = 100f;

	public float recoverySpeed = 20; // hp/min

	public float MaximumHP { get { return maximumHP; } }

	public float CurrentHP { get { return currentHP; } }

	public bool StaminaReady { get { return currentStamina > 0; } }

	public float CurrentStamina { get { return currentStamina; } }

	public float stRecoveryWait = 0;

	// Update is called once per frame
	public virtual void Update()
	{
		if (currentHP < 0)
		{
			Die();
		}
		else
		{
			if (currentHP < maximumHP) // recover hp, reduce stamina
			{
				float recovery = recoverySpeed / 60 * Time.deltaTime; // heal by recoverySpeed per minute
				currentHP += recovery;

				maximumStamina -= recovery;
				currentStamina -= recovery;
			}
			else if (maximumStamina < startingStamina) // recover stamina
			{
				maximumStamina += 20f / 60 * Time.deltaTime;
			}

			if (currentStamina < maximumStamina)
			{
				if (stRecoveryWait > 0)
				{
					stRecoveryWait -= Time.deltaTime;
				}
				else
				{
					currentStamina += 1800f / 60 * Time.deltaTime;

				}
			}

			if (maximumHP < startingHP && maximumStamina >= startingStamina) // recover recoverableHP
			{
				maximumHP += 8f / 60 * Time.deltaTime;
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
		recDmg = cOverMCurve.Evaluate(currentHP / maximumHP);
		absDmg = 1 - recDmg;

		currentHP -= absDmg * dmg;
		maximumHP -= recDmg * dmg;
	}

	public virtual void Die()
	{

	}
}
