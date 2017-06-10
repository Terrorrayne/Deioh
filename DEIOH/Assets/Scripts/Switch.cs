using UnityEngine;

public class Switch : MonoBehaviour, IButton
{

	public SwitchSystem signalTarget;
	public string signalMessage;

	Animation anim;

	public void Start()
	{
		anim = GetComponent<Animation>();
	}

	public bool ButtonIsActive()
	{
		return true;
	}

	public void ButtonPress(PlayerButtonInteraction player)
	{
		signalTarget.RecieveSignal(signalMessage);
		TriggerAnim();
	}

	public void TriggerAnim()
	{
		if (anim != null)
		{
			anim.clip.legacy = true;
			anim.Play();
		}
	}
}
