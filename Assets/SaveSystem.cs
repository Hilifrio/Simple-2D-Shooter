using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    //string pathToSave = Application.persistentDataPath + "/player.sf"; "D:/Platformer 2D/First Platformer/Saves"

    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path =  "D:/Platformer 2D/RetroWave builds/Saves/player.sf";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        Debug.Log("Load player function: working");
        string path = Application.persistentDataPath + "/player.sf";
        if (File.Exists(path))
        {
            Debug.Log("file founded!!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
