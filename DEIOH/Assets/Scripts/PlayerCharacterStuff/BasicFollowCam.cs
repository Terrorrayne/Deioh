using UnityEngine;

public class BasicFollowCam : MonoBehaviour
{
	public float lerpSpeed = 20f;
	public float lerpSpeedSlow = 1f;
	[Tooltip("how far ahead we want to look when we are moving (will never reach this unless lerp is set very high)")]
	public float movingFocusAhead = 5f;
	[Tooltip("how fare ahead we want to look when we come to a standstill, should be less than MovingFocusAhead")]
	public float restFocusAhead = 2f;
	[Tooltip("camera will move slower when within radius, center is set from forward*foc*restFocusAhead, should be smaller than restFocusAhead")]
	public float restRadius = 3f;
	public Transform followObj;
	Vector3 offset;

	Vector3 restCenter;
	bool cameraResting = false;

	Vector3 foc;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - followObj.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (cameraResting)
		{
			// check to see that we are still in the correct radius
			if (Vector3.Distance(followObj.transform.position, restCenter) > restRadius)
			{
				cameraResting = false;
			}
			else
			{
				// slowly move camera
				foc = followObj.transform.forward * restFocusAhead;
				MoveCamera(lerpSpeedSlow);
			}
		}
		else
		{
			if (followObj.GetComponentInParent<CharacterMovement>().focusposition.magnitude > 0.3f)
			{
				foc = followObj.transform.forward * movingFocusAhead;
			}
			else
			{
				foc = followObj.transform.forward * restFocusAhead;
				// set new rest area
				SetNewRestArea();
			}
			MoveCamera(lerpSpeed);

		}

		// todo: create small area where camrea will track more slowly or not at all, once player leaves this area camrea switches to more aggressive tracking
		// goal is to give the feel of zelda/super metroid where camera wouldn't immediatly follow player
	}

	void MoveCamera(float camSpd)
	{

		transform.position = Vector3.Lerp(transform.position, followObj.transform.position + foc + offset, camSpd * Time.deltaTime);
	}

	void SetNewRestArea()
	{
		if (!cameraResting)
		{
			//check to see if we're close to our restFocus point
			// if we are then go ahead and set us resting 

			print(transform.position + " || " + (followObj.transform.position + foc + offset));
			if (Vector3.Distance(transform.position, (followObj.transform.position + foc + offset)) < 1)
			{
				cameraResting = true;
				restCenter = followObj.transform.position + foc;
			}
		}
		else
		{
			restCenter = followObj.transform.position + foc;
		}
	}


}
