using UnityEngine;

public class DeiohGame : MonoBehaviour
{
	// omg this acts as a manager for everything everywhere ever in DEIOH whatever DEIOH turns out to be

	public static DeiohGame self;

	public PlayableCharacter player; // which game is the player in control of
	public BasicFollowCam gameCamera; // camera that follows the player

	bool isConsoleOpen = false;
	public GameObject consoleCanvas;

	// Use this for initialization
	void Start()
	{
		// we singlton as hell bitch
		if (self == null)
		{
			self = this;
		}
		else
		{
			Destroy(gameObject);
		}

		Time.timeScale = 1;
		consoleCanvas.SetActive(false);
		// lock our mouse plz
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			isConsoleOpen = !isConsoleOpen;

			if (isConsoleOpen)
			{
				// pause game, and ai
				Time.timeScale = 0;
				// open console
				consoleCanvas.SetActive(true);
				// unlock cusor
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

				consoleCanvas.GetComponent<Console>().Init();
			}
			else
			{
				Time.timeScale = 1;
				consoleCanvas.SetActive(false);
				// lock our mouse plz
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}

		}
	}
}