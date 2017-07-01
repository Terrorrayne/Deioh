using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	public float maxHitPoints = 20f; // you a squishy bish
	float recoverableHP = 20f;
	float absoluteHP = 20;
	public AnimationCurve aOverRCurve;

	public float maxStamina = 100f;
	float recoverableStamina = 100f;
	float absoluteStamina = 100f;

	public float recoverySpeed = 20; // hp/min

	public Slider rHPSlider;
	public Slider aHPSlider;
	public Slider rStSlider;
	public Slider aStSlider;
	public variableframerate varfararaararrar;

	public float RecoverableHP
	{
		get
		{
			return recoverableHP;
		}
	}

	public float AbsoluteHP
	{
		get
		{
			return absoluteHP;
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
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
				recoverableStamina += 8f / 60 * Time.deltaTime;
			}

			if (absoluteStamina < recoverableStamina)
			{
				absoluteStamina += 50 / 60 * Time.deltaTime;
			}

			if (recoverableHP < maxHitPoints && recoverableStamina >= maxStamina) // recover recoverableHP
			{
				recoverableHP += 8f / 60 * Time.deltaTime;
			}

		}


		rHPSlider.value = recoverableHP / maxHitPoints;
		aHPSlider.value = absoluteHP / maxHitPoints;

		rStSlider.value = recoverableStamina / maxStamina;
		aStSlider.value = absoluteStamina / maxStamina;
	}


	public void Damage(float dmg)
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

	public void Die()
	{
		// when we die do a sequence of events
		// oh i know. when you die:
		// you get a small window of time you can revive yourself equal to the amount of recovHP you have left

		if (recoverableHP < 0)
		{
			// you are dead dead
			//activate our dead stuff
			varfararaararrar.enabled = true;
		}
		else
		{
			recoverableHP -= 3 * Time.deltaTime;
		}
	}
}
