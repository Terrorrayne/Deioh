using UnityEngine;

public class DeiohGame : MonoBehaviour
{
	// omg this acts as a manager for everything everywhere ever in DEIOH whatever DEIOH turns out to be

	DeiohGame self;

	// Use this for initialization
	void Start()
	{
		// we singlton as hell bitch
		if (self == null)
		{
			self = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
