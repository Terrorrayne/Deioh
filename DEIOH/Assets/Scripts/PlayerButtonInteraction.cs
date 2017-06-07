using UnityEngine;

public class PlayerButtonInteraction : MonoBehaviour
{

	public LayerMask buttonMask;

	float progress = 0;
	public float Progress
	{
		get { return progress; }

		set
		{
			progress = Mathf.Clamp01(value);
			UIController.self.InteractProgressBubble(progress);
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		Collider[] col = Physics.OverlapSphere(transform.position + Vector3.up, 1.5f, buttonMask);

		if (col.Length > 0)
		{
			// we have stuff to do

			// move interact UI over this button
			UIController.self.MoveInteractElement(col[0].transform);

			if (Input.GetButtonDown("Fire1"))
			{
				Progress += Time.deltaTime * 1.5f;
			}

			if (Input.GetButton("Fire1") && Progress > 0)
			{
				Progress += Time.deltaTime * 1.5f;
			}

			if (Progress == 1)
			{
				col[0].GetComponent<IButton>().ButtonPress();
				Progress = 0;
			}
		}
		if (Input.GetButtonUp("Fire1"))
		{
			Progress = 0;
		}
	}
}
