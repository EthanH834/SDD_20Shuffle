using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class saveData : MonoBehaviour
{
    public gameManager gameManager;
    public playerData playerdata = new playerData();

    public void saveAllData()
    {
        playerdata.playerScore = gameManager.playerScore;
        playerdata.playerName = gameManager.playerNewName;

        Debug.Log(playerdata.playerScore);
        Debug.Log(playerdata.playerName);
        saveToJSON();
    }

    public void saveToJSON()
    {
        // Read existing JSON data
        string filePath = Application.persistentDataPath + "/playerData.json";
        string existingJson = System.IO.File.ReadAllText(filePath);
        Debug.Log(filePath);

        Debug.Log("Existing JSON: " + existingJson); // Debug output

        // Parse existing JSON into a collection or object
        PlayerDataWrapper existingPlayerDataWrapper = JsonUtility.FromJson<PlayerDataWrapper>(existingJson);
        List<playerData> existingPlayerData = existingPlayerDataWrapper != null ? existingPlayerDataWrapper.highscores : new List<playerData>();

        // Modify existing data or create new instance
        // Modify or append other values as needed

        // Append new data to existing collection
        existingPlayerData.Add(playerdata);

        // Create a new wrapper object
        PlayerDataWrapper updatedPlayerDataWrapper = new PlayerDataWrapper();
        updatedPlayerDataWrapper.highscores = existingPlayerData;

        // Convert updated data back to JSON format
        string updatedJson = JsonUtility.ToJson(updatedPlayerDataWrapper);

        // Overwrite the file with updated JSON data
        System.IO.File.WriteAllText(filePath, updatedJson);
    }
}

[System.Serializable]
public class playerData
{
    public string playerName;
    public int playerScore;
}

[System.Serializable]
public class PlayerDataWrapper
{
    public List<playerData> highscores;
}