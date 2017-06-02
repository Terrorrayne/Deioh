using UnityEngine;

public class BasicFollowCam : MonoBehaviour
{

	public Transform followObj;
	Vector3 offset;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - followObj.position;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = followObj.position + offset;
	}
}
