using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private AudioManager audioManager;
    // Start is called before the first frame update
    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    public string pauseMenuMusic = "MenuMusic";

    public string currentLevelMusic = "LevelMusic";
    // Start is called before the first frame update
    public bool isGamePaused = false;
    [SerializeField]
    private GameObject pauseMenuUI;


    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager");
        }
    }

    public void Menu()
    {
        audioManager.StopSound(pauseMenuMusic);
        audioManager.PlaySound(pressButtonSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        SaveSystem.SavePlayer();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        audioManager.PlaySound(currentLevelMusic);
        audioManager.StopSound(pauseMenuMusic);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        audioManager.PlaySound(pauseMenuMusic);
        audioManager.StopSound(currentLevelMusic);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Quit()
    {
        SaveSystem.SavePlayer();
        Debug.Log("WE QUIT THE GAME WITH SAVING!");
        Application.Quit();
    }

}
