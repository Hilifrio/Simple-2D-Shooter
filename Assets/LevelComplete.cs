using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private AudioManager audioManager;
    // Start is called before the first frame update
    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    public string levelCompleteSound = "LevelComplete";
    public string levelCompleteMusic = "LevelCompleteMusic";

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager reference in Menu manager!");
        }
        audioManager.PlaySound("LevelCompleteMusic");
        Pause();
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0f;
        yield return new WaitForSeconds(3f);
        
    }

    public void NextLevel()
    {
        Debug.Log("NEXT LEVEL!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        audioManager.PlaySound(pressButtonSound);
        audioManager.StopSound(levelCompleteMusic);
    }

    // Update is called once per frame
    public void Menu()
    {
        audioManager.StopSound(levelCompleteMusic);
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene("Menu");
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
