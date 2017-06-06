using UnityEngine;

public class UIObjectFollow : MonoBehaviour
{

	public Transform objToFollow;
	public float lerpSpd = 7.5f;

	private void LateUpdate()
	{
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(objToFollow.position);
		transform.position = Vector3.Lerp(transform.position, screenPoint, lerpSpd * Time.deltaTime);
	}
}
