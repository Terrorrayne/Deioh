using UnityEngine;

public class Switch : MonoBehaviour, IButton
{

	public SwitchSystem signalTarget;
	public string signalMessage;

	public bool ButtonIsActive()
	{
		return true;
	}

	public void ButtonPress()
	{
		signalTarget.RecieveSignal(signalMessage);
	}
}
