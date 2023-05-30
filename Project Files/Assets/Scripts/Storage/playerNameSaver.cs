using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerNameSaver : MonoBehaviour
{
    public TMPro.TMP_Text displayPlayerName;
    public TMPro.TMP_Text displayPlayer2Name;


    public void Awake()
    {
        displayPlayerName.text = persistentName.scene1.playerName;
        displayPlayer2Name.text = persistentName.scene1.player2Name;
        Debug.Log(displayPlayerName);
        Debug.Log(displayPlayer2Name);
    }
}
