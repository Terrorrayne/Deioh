using UnityEngine;
using UnityEngine.UI;

public class UIObjectFollow : MonoBehaviour
{

	public Transform objToFollow;
	public float lerpSpd = 7.5f;
	public Vector2 offset = Vector2.zero;


	private void LateUpdate()
	{
		if (objToFollow != null)
		{
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(objToFollow.position);
			screenPoint += (Vector3)offset;
			transform.position = Vector3.Lerp(transform.position, screenPoint, lerpSpd * Time.deltaTime);

		}
		else
		{
			// disable rendering
			GetComponent<Text>().enabled = false;
		}
	}

	public void SetNewObjToFollow(Transform obj)
	{
		if (obj != objToFollow)
		{
			objToFollow = obj;
			GetComponent<Text>().enabled = true;

			// snap to new obj
			transform.position = Camera.main.WorldToScreenPoint(objToFollow.position + transform.up * 5);

			// eventually i'd like stuff to fade in
		}
	}
}
