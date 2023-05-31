using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private Transform highscoresHolderTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;

    private string SavePath => Application.persistentDataPath + "/playerData.json";
    int leaderboardLimit = 1;

    private void Start()
    {
        LeaderboardSaveData savedScores = getSavedScores();
        //Debug.Log(savedScores);
        updateUI(savedScores);
    }

    private void updateUI(LeaderboardSaveData savedScores)
    {
        //Debug.Log("start");
        foreach (Transform child in highscoresHolderTransform)
        {
            Destroy(child.gameObject);
        }

        var n = savedScores.highscores.Count;
        foreach (playerData playerdata in savedScores.highscores)
        {
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (savedScores.highscores[j].playerScore < savedScores.highscores[j + 1].playerScore)
                    {
                        var tempVar = savedScores.highscores[j].playerScore;
                        savedScores.highscores[j].playerScore = savedScores.highscores[j + 1].playerScore;
                        savedScores.highscores[j + 1].playerScore = tempVar;

                        string tempString = savedScores.highscores[j].playerName;
                        savedScores.highscores[j].playerName = savedScores.highscores[j + 1].playerName;
                        savedScores.highscores[j + 1].playerName = tempString;
                    }
                }
            }
        }


        foreach (playerData playerdata in savedScores.highscores)
        {
            //Debug.Log("done one");
            Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<LeaderboardUI>().initialise(playerdata);
            leaderboardLimit++;
            if (leaderboardLimit >= 6)
            {
                break;
            }
        }
        leaderboardLimit = 1;
    }

    private LeaderboardSaveData getSavedScores()
    {
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new LeaderboardSaveData();
        }

        using (StreamReader stream = new StreamReader(SavePath))
        {
            string json = stream.ReadToEnd();
            //Debug.Log(json);
            return JsonUtility.FromJson<LeaderboardSaveData>(json);
        }
    }
}