using UnityEngine;

public class BasicFollowCam : MonoBehaviour
{
	public float lerpSpeed = 20f;
	public float aheadScale = 1f;
	public CharacterMovement followObj;
	Vector3 offset;

	Vector3 foc;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - followObj.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (followObj.focusposition.magnitude > 0.3f)
		{
			foc = followObj.focusposition;
		}
		transform.position = Vector3.Lerp(transform.position, followObj.transform.position + foc * aheadScale + offset, lerpSpeed * Time.deltaTime);
	}
}
