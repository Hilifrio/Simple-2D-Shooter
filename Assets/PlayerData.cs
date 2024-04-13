using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int activeSceneIndex;

    public PlayerData()
    {
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Trying to save the current Scene :" + activeSceneIndex);
    }
}
