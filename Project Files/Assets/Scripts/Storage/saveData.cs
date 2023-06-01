using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class saveData : MonoBehaviour
{
    public gameManager gameManager;
    public playerData playerdata = new playerData();

    public void saveAllData()
    {
        // Preparing to save player data to file
        playerdata.playerScore = gameManager.playerScore;
        playerdata.playerName = gameManager.playerNewName;

        saveToJSON();
    }

    public void saveToJSON()
    {
        // Read existing JSON data
        string filePath = Application.streamingAssetsPath + "/playerData.json";
        string existingJson = System.IO.File.ReadAllText(filePath);

        // Parse existing JSON into a collection or object
        PlayerDataWrapper existingPlayerDataWrapper = JsonUtility.FromJson<PlayerDataWrapper>(existingJson);
        List<playerData> existingPlayerData = existingPlayerDataWrapper != null ? existingPlayerDataWrapper.highscores : new List<playerData>();
        
        // Append new data to existing collection
        existingPlayerData.Add(playerdata);

        PlayerDataWrapper updatedPlayerDataWrapper = new PlayerDataWrapper();
        updatedPlayerDataWrapper.highscores = existingPlayerData;
        string updatedJson = JsonUtility.ToJson(updatedPlayerDataWrapper);

        // Overwrite the file with updated JSON data
        System.IO.File.WriteAllText(filePath, updatedJson);
    }
}

// Player data to be saved
[System.Serializable]
public class playerData
{
    public string playerName;
    public int playerScore;
}

// List the contains the player data
[System.Serializable]
public class PlayerDataWrapper
{
    public List<playerData> highscores;
}