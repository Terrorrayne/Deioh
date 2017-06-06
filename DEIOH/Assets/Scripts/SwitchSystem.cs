using UnityEngine;

public class SwitchSystem : MonoBehaviour
{

	public Transform[] switches;

	public int[] sequences;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void RecieveSignal(string signalmessage)
	{
		print("hubba hubba " + signalmessage);
	}
}
