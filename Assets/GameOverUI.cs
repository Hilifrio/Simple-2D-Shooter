using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	private AudioManager audioManager;

	[SerializeField]
	string hoverOverSound = "ButtonHover";
	[SerializeField]
	string pressButtonSound = "ButtonPress";

	public string gameOverSound = "GameOver";
	public string gameOverMusic = "GameOverMusic";


	void Start()
    {
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("No audio manager reference in Menu manager!");
		}
		audioManager.PlaySound(gameOverMusic);
	}

	public void Quit()
	{
		Debug.Log("GO TO MENU");
		audioManager.PlaySound(pressButtonSound);
		SceneManager.LoadScene("Menu");
		audioManager.PlaySound(pressButtonSound);
		audioManager.StopSound(gameOverMusic);
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		audioManager.PlaySound(pressButtonSound);
		audioManager.StopSound(gameOverMusic);
	}

	public void OnMouseOver()
	{
		audioManager.PlaySound(hoverOverSound);
	}
}
