using UnityEngine;

public class PlayerButtonInteraction : MonoBehaviour
{

	public LayerMask buttonMask;

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

			col[0].GetComponent<IButton>().ButtonPress();
		}
	}
}
