using UnityEngine;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
	// manually set references
	public GameObject commandList;
	public GameObject objectPlacementTool;
	public GameObject[] objectsWeCanPlace;
	public GameObject gameUI;
	public GameObject musicPlayer;


	public void Start()
	{
		Init();
	}

	public void Init()
	{
		commandList.SetActive(true);
		objectPlacementTool.SetActive(false);
	}

	public void RestartScene()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitGame()
	{
		if (Application.isEditor)
		{
			UnityEditor.EditorApplication.isPlaying = false;
		}
		Application.Quit();
	}

	public void ObjectPlacementTool()
	{
		// lets us place an object in the scene
		// place enemies, place items

		commandList.SetActive(false);
		objectPlacementTool.SetActive(true);
	}

	public void PlaceObject()
	{

	}

	public void ExitOPT()
	{
		Init();
	}

	public void DisablePostProcessing()
	{

	}

	public void EditPlayerHealthSettings()
	{
		// deathless (can't die, but still takes damage)
		// no health drain (health doesn't change, is maxed)
		// no stamina drain (stamina doesn't change, is maxed)
	}

	public void ToggleGUI()
	{
		gameUI.SetActive(!gameUI.activeSelf);
	}

	public void ToggleMusic()
	{
		musicPlayer.SetActive(!musicPlayer.activeSelf);
	}

	public void ToggleActive(GameObject objectToToggle)
	{
		objectToToggle.SetActive(!objectToToggle.activeSelf);
	}
}
