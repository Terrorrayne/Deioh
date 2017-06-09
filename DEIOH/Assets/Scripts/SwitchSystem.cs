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

	public int seqPos;
	public bool[] validSequences;

	// Use this for initialization
	void Start()
	{
		validSequences = new bool[sequences.Length];
		ResetSeq();
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
				InputSeq(0);
				break;
			case "1":
				InputSeq(1);
				break;
			case "2":
				InputSeq(2);
				break;
			case "3":
				InputSeq(3);
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

	void ResetSeq()
	{
		seqPos = 0;
		for (int i = 0; i < sequences.Length; i++)
		{
			validSequences[i] = true; // you are all valid...
		}
	}

	void InputSeq(int input)
	{
		bool valid = false;

		// check to see if input matches any of our sequences
		for (int v = 0; v < validSequences.Length; v++)
		{
			if (validSequences[v])
			{
				// does input match corresponding int in sequence
				if (input == sequences[v].seq[seqPos])
				{
					// continue to next loop, and next num
					valid = true;

					if (seqPos + 1 == sequences[v].seq.Length)
					{
						// YAY you did the thing
						ResetSeq();
						SequenceComplete(v);
						return;
					}
				}
				else
				{
					validSequences[v] = false; // you are not valid. i will never see you again
				}
			}

			// nothings working, reset
			if (v == validSequences.Length - 1)
			{
				if (valid) // continue to next num in seq
				{
					seqPos++;
				}
				else // start over and look for the beginning of a new seq
				{
					ResetSeq();
				}
			}

		}

	}

	void SequenceComplete(int i)
	{
		print("seq" + i);
	}
}
