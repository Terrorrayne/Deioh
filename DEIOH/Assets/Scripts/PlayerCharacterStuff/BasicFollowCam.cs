using UnityEngine;

public class BasicFollowCam : MonoBehaviour
{
	public float lerpSpeed = 20f;
	public float lerpSpeedSlow = 1f;
	public AnimationCurve lerpSlowCurve;
	[Tooltip("how far ahead we want to look when we are moving (will never reach this unless lerp is set very high)")]
	public float movingFocusAhead = 5f;
	[Tooltip("how fare ahead we want to look when we come to a standstill, should be less than MovingFocusAhead")]
	public float restFocusAhead = 2f;
	[Tooltip("camera will move slower when within radius, center is set from forward*foc*restFocusAhead, should be smaller than restFocusAhead")]
	public float restRadius = 3f;
	public Transform followObj;
	Vector3 offset;

	Vector3 restCenter;
	bool _cameraResting = false;
	float restAndMoveTimer = 0;
	Vector3 foc;

	float goalLerp;
	float currentLerp;
	float lerpLerp;

	public bool CameraResting
	{
		get
		{
			return _cameraResting;
		}

		set
		{
			_cameraResting = value;
			if (value)
				restAndMoveTimer = 0;
		}
	}

	// Use this for initialization
	void Start()
	{
		offset = transform.position - followObj.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		currentLerp = Mathf.Lerp(currentLerp, goalLerp, lerpLerp * Time.deltaTime);


		if (CameraResting)
		{
			// check to see that we are still in the correct radius
			float distFromCenter = Vector3.Distance(followObj.transform.position, restCenter);

			if (distFromCenter > restRadius)
			{
				CameraResting = false;
			}
			else
			{
				// slowly move camera
				foc = followObj.transform.forward * restFocusAhead;

				//MoveCamera(lerpSpeedSlow);
				MoveCamera(lerpSlowCurve.Evaluate(distFromCenter / restRadius));
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

	}

	void MoveCamera(float camSpd)
	{
		// todo: make camSpd lerp so transitions between rest and non rest are fluid-er. more fluid

		goalLerp = camSpd;
		lerpLerp = 1.5f;

		transform.position = Vector3.Lerp(transform.position, followObj.transform.position + foc + offset, currentLerp * Time.deltaTime);
	}

	void SetNewRestArea()
	{
		if (!CameraResting)
		{
			//check to see if we're close to our restFocus point
			// if we are then go ahead and set us resting 

			print(transform.position + " || " + (followObj.transform.position + foc + offset));
			if (Vector3.Distance(transform.position, (followObj.transform.position + foc + offset)) < 2)
			{
				CameraResting = true;
				restCenter = followObj.transform.position + foc;
			}
		}
		else
		{
			restCenter = followObj.transform.position + foc;
		}
	}


}
