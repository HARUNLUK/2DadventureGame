using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonHelper
{
    public static void savePlayer(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/player1.json", json);
    }
    public static PlayerData LoadPlayer()
    {
        string json = File.ReadAllText(Application.dataPath + "/player1.json");
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        if (data is null) 
        {
            return null;
        }
        return data;
    }

}
