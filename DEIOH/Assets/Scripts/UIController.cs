using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public static UIController self;

	public UIObjectFollow InteractUIElement;
	public Image InteractProgressImg;

	// Use this for initialization
	void Start()
	{
		if (self == null)
		{
			self = this;
		}
		else
		{
			Destroy(this);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void MoveInteractElement(Transform obj)
	{
		InteractUIElement.SetNewObjToFollow(obj);
	}
}
