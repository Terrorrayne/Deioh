using System;
using System.Collections;
using UnityEngine;

public class SwitchSystem : MonoBehaviour
{

	public Transform[] switches;

	public sequence[] sequences;

	[Serializable]
	public class sequence
	{
		public int[] seq;
	}

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
		switch (signalmessage)
		{
			case "A":
				StartCoroutine(playSequence(sequences[0].seq, 0.5f));
				break;
			case "B":
				StartCoroutine(playSequence(sequences[1].seq, 0.5f));
				break;
			case "0":
				break;
			case "1":
				break;
			case "2":
				break;
			case "3":
				break;
		}
	}

	// play a seq to be repeated

	IEnumerator playSequence(int[] seq, float delayBetweenThings)
	{


		for (int i = 0; i < seq.Length; i++)
		{
			// play the anim
			switches[seq[i]].GetComponent<Switch>().TriggerAnim();
			// wait a bit
			yield return new WaitForSeconds(delayBetweenThings);
		}
	}
}
