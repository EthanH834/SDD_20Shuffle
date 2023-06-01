using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LeaderboardManager : MonoBehaviour
{
    // 
    [SerializeField] private Transform highscoresHolderTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;

    // Save to streamingAssets folder
    private string savePath => Application.streamingAssetsPath + "/playerData.json";
    int leaderboardLimit = 1;

    private void Start()
    {
        LeaderboardSaveData savedScores = getSavedScores();
        updateUI(savedScores);
    }

    private void updateUI(LeaderboardSaveData savedScores)
    {
        // Deletes old leaderboard data
        foreach (Transform child in highscoresHolderTransform)
        {
            Destroy(child.gameObject);
        }

        // Sorts leaderboard data contained within the list
        var n = savedScores.highscores.Count;
        foreach (playerData playerdata in savedScores.highscores)
        {
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Sort by player score in descending order
                    if (savedScores.highscores[j].playerScore < savedScores.highscores[j + 1].playerScore)
                    {
                        // Sort player scores
                        var tempVar = savedScores.highscores[j].playerScore;
                        savedScores.highscores[j].playerScore = savedScores.highscores[j + 1].playerScore;
                        savedScores.highscores[j + 1].playerScore = tempVar;

                        // Sort player name
                        string tempString = savedScores.highscores[j].playerName;
                        savedScores.highscores[j].playerName = savedScores.highscores[j + 1].playerName;
                        savedScores.highscores[j + 1].playerName = tempString;
                    }
                }
            }
        }

        // Creates new leaderboard items
        foreach (playerData playerdata in savedScores.highscores)
        {
            Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<LeaderboardUI>().initialise(playerdata);
            leaderboardLimit++;

            // Limit of 5 items
            if (leaderboardLimit >= 6)
            {
                break;
            }
        }
        leaderboardLimit = 1;
    }

    // Retrieves saved scores from JSON
    private LeaderboardSaveData getSavedScores()
    {
        if (!File.Exists(savePath))
        {
            File.Create(savePath).Dispose();
            return new LeaderboardSaveData();
        }

        using (StreamReader stream = new StreamReader(savePath))
        {
            string json = stream.ReadToEnd();
            return JsonUtility.FromJson<LeaderboardSaveData>(json);
        }
    }
}