using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	string hoverOverSound = "ButtonHover";
	[SerializeField]
	string pressButtonSound = "ButtonPress";
	[SerializeField]
	string menuMusic = "MenuMusic";

	AudioManager audioManager;

	void Start()
    {
		audioManager = AudioManager.instance;
        if (audioManager == null)
        {
			Debug.LogError("No audio manager reference in Menu manager!");
        }
		audioManager.PlaySound(menuMusic);
	}
	public void StartGame()
	{
		audioManager.PlaySound(pressButtonSound);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		audioManager.StopSound(menuMusic);
	}

	public void QuitGame()
	{
		audioManager.PlaySound(pressButtonSound);
		Debug.Log("WE QUIT THE GAME!");
		Application.Quit();
	}

	public void OnMouseOver()
    {
		audioManager.PlaySound(hoverOverSound);
    }

	public void LoadGame()
    {
		PlayerData data = SaveSystem.LoadPlayer();
		SceneManager.LoadScene(data.activeSceneIndex);
		Debug.Log("Load Game");
    }

}

