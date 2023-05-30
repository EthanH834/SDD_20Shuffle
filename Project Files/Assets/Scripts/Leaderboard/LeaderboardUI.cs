using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void initialise(playerData entryPlayerData)
    {
        entryNameText.text = entryPlayerData.playerName;
        entryScoreText.text = entryPlayerData.playerScore.ToString();
    }
}
