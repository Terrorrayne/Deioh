using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth
{
	public Slider rHPSlider;
	public Slider aHPSlider;
	public Slider rStSlider;
	public Slider aStSlider;
	public variableframerate varfararaararrar;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		rHPSlider.value = recoverableHP / maxHitPoints;
		aHPSlider.value = absoluteHP / maxHitPoints;

		rStSlider.value = recoverableStamina / maxStamina;
		aStSlider.value = absoluteStamina / maxStamina;
	}


	public override void Damage(float dmg, Vector3 dir)
	{
		base.Damage(dmg, dir);
		// knockback
		GetComponent<CharacterMovement>().Knockback(dir.normalized * 20f);
	}

	public override void Die()
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

	public void DrainStamina(float stDrain, float wait = 0)
	{
		if (stDrain > absoluteStamina) // do the attack, but take away a portion of recoveryStamina
		{
			float penalty = stDrain - absoluteStamina;
			recoverableStamina -= penalty * 0.2f;
		}

		absoluteStamina -= stDrain;
		if (wait > stRecoveryWait) // wait a period of time before recovering
			stRecoveryWait = wait;
	}
}
