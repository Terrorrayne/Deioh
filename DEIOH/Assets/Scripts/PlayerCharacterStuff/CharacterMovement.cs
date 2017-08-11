using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public Vector3 focusposition;
	public Transform characterModel;
	public float moveSpeed = 5f;
	public AnimationCurve movementAngleLookup;

	public Transform mouseCursorIndicator;
	public Transform rotationIndicator;

	Vector3 mousePos;

	Vector3 inertia;

	public float movementMultiplier = 1f;

	Animator myAnimator;

	private void Start()
	{
		myAnimator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		Vector2 input = Vector2.zero;
		float inputMag = 0;

		// get input
		input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		// normalize input
		inputMag = Mathf.Clamp01(input.magnitude);
		// translate input to animation
		float angle = Vector2.Angle(Vector2.up, input);
		if (input.x < 0)
		{
			angle = -angle;
		}

		//if (Input.GetMouseButton(1))
		if (true)
		{
			// use mouse cursor for direction
			if (Input.GetMouseButtonDown(1)) // if we just clicked RMB
			{
				// set mouse position some point infront of the player
				mousePos = characterModel.forward * 3f;

				mouseCursorIndicator.position = transform.position + mousePos;

			}
			else
			{
				mousePos.x += Input.GetAxis("Mouse X") * 0.65f;
				mousePos.z += Input.GetAxis("Mouse Y") * 0.65f;

				// add some from our input so its easier to track targets
				//mousePos.x += input.x * -0.007f; // 0.02 seems to negate movement
				//mousePos.z += input.y * -0.007f; // don't ask why, as this implies speed is 50 instead of 5

				// clamp indicator radius by a certain amount
				if (mousePos.sqrMagnitude > 25)
				{
					mousePos = mousePos.normalized * 5;
				}

				// lerp to new mosue pos? idk maybe
				mouseCursorIndicator.position = Vector3.Lerp(mouseCursorIndicator.position, transform.position + mousePos, 40 * Time.deltaTime);

			}

			// char model should follow mouse position
			characterModel.localRotation = Quaternion.LookRotation(mouseCursorIndicator.localPosition, Vector3.up);
			// movement speed should be adjusted by angle of movemnt (running backwards is hard to do irl)
			// compare move pos vs look pos to get difference, use mvoementAngleLookup to eval speed

			float cursorangle = Vector3.Angle(mouseCursorIndicator.localPosition, Vector3.forward);
			if (mouseCursorIndicator.localPosition.x < 0)
			{
				cursorangle = -cursorangle;
			}

			//print(angle - cursorangle);

			//GetComponent<CharacterController>().SimpleMove(new Vector3(input.x, 0, input.y) * moveSpeed * inputMag * movementAngleLookup.Evaluate(Mathf.Abs(angle - cursorangle) % 180f));
			input.Normalize();
			GetComponent<CharacterController>().SimpleMove(movementMultiplier * (new Vector3(input.x, 0, input.y) * moveSpeed * inputMag) + inertia);

			focusposition = characterModel.forward * moveSpeed * inputMag;

			// idk
			if (inputMag > 0) // we are walking 
			{

				myAnimator.SetBool("Walking", true);
			}
			else
			{
				myAnimator.SetBool("Walking", false);
			}

		}
		else
		{
			if (inputMag > 0) // we are walking 
			{
				characterModel.localRotation = Quaternion.Euler(0, angle, 0);
				//transform.localRotation = Quaternion.Euler(0, angle, 0);

				myAnimator.SetBool("Walking", true);
			}
			else
			{
				myAnimator.SetBool("Walking", false);
			}

			// apply input to movement

			GetComponent<CharacterController>().SimpleMove(characterModel.forward * moveSpeed * inputMag + inertia);
			focusposition = characterModel.forward * moveSpeed * inputMag;
		}

		//GetComponent<CharacterController>().Move(inertia * Time.deltaTime);

		inertia = Vector3.Lerp(inertia, Vector3.zero, Time.deltaTime * 30);

	}

	public void Knockback(Vector3 force)
	{
		inertia += force;
	}
}
