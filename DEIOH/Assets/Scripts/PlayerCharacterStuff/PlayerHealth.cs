using PigeonCoopToolkit.Effects.Trails;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : CharacterHealth
{
	public Slider maxHPSlider;
	public Slider curHPSlider;
	public Slider maxStSlider;
	public Slider curStSlider;
	public variableframerate varfararaararrar;
	public LensFlare healthFlare1;
	public LensFlare healthFlare2;
	public AnimationCurve healthFlarePulseAmp;
	public AnimationCurve healthFlarePulseWave;
	public AnimationCurve healthHeartRate;
	public SmokeTrail healthTrail1;
	public SmokeTrail healthTrail2;

	private float _pulseTime = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();

		maxHPSlider.value = maximumHP / startingHP;
		curHPSlider.value = currentHP / startingHP;

		maxStSlider.value = maximumStamina / startingStamina;
		curStSlider.value = currentStamina / startingStamina;

		float healthValue = currentHP / startingHP;
		_pulseTime += Time.deltaTime * healthHeartRate.Evaluate(currentHP / maximumHP);
		float pulse1 = healthFlarePulseWave.Evaluate((_pulseTime + 0.1f) % 1f);
		float pulse2 = healthFlarePulseWave.Evaluate(_pulseTime % 1f);
		_pulseTime %= 1; // don't go crazy...

		healthFlare1.color = Color.Lerp(new Color(0.5f, 0.5f, 0.5f), new Color(0.2f, 0.2f, 0.2f), healthValue);
		healthFlare1.brightness = Mathf.Lerp(1.6f, 1, healthValue) * (1 + (healthFlarePulseAmp.Evaluate(healthValue) * pulse1 * 2f));

		healthFlare2.color = Color.Lerp(Color.white, Color.black, healthValue);
		healthFlare2.brightness = Mathf.Lerp(2.2f, 0.25f, healthValue) * (1 + (healthFlarePulseAmp.Evaluate(healthValue) * pulse2));

		healthValue = maximumHP / startingHP;
		healthTrail1.TrailData.Lifetime = Mathf.Lerp(0.2f, 0.6f, healthValue);
		healthTrail2.TrailData.Lifetime = Mathf.Lerp(0.2f, 0.8f, healthValue) * currentHP / maximumHP;
	}


	public override void Damage(float dmg, Vector3 pushDir, Vector3 atkPos)
	{
		base.Damage(dmg, pushDir, atkPos);
		// knockback
		GetComponent<CharacterMovement>().Knockback(pushDir.normalized * 20f);
	}

	public override void Die()
	{
		// when we die do a sequence of events
		// oh i know. when you die:
		// you get a small window of time you can revive yourself equal to the amount of recovHP you have left

		if (maximumHP < 0)
		{
			// you are dead dead
			//activate our dead stuff
			varfararaararrar.enabled = true;
		}
		else
		{
			maximumHP -= 3 * Time.deltaTime;
		}
	}

	public void DrainStamina(float stDrain, float wait = 0)
	{
		if (stDrain > currentStamina) // do the attack, but take away a portion of recoveryStamina
		{
			float penalty = stDrain - currentStamina;
			maximumStamina -= penalty * 0.2f;
		}

		currentStamina -= stDrain;
		if (wait > stRecoveryWait) // wait a period of time before recovering
			stRecoveryWait = wait;
	}
}
