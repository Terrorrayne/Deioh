using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public Vector3 focusposition;
	public Transform characterModel;
	public float moveSpeed = 5f;

	private void Update()
	{
		// get input
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		// normalize input
		float inputmag = Mathf.Clamp01(input.magnitude);
		// translate input to animation
		float angle = Vector2.Angle(Vector2.up, input);

		if (input.x < 0)
		{
			angle = -angle;
		}
		if (inputmag > 0)
		{
			characterModel.localRotation = Quaternion.Euler(0, angle, 0);

		}
		// apply input to movement

		GetComponent<CharacterController>().SimpleMove(characterModel.forward * moveSpeed * inputmag);
		focusposition = characterModel.forward * moveSpeed * inputmag;
	}
}
