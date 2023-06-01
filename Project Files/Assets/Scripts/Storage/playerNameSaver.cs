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
        // Save inputfields and display them on the game scene
        displayPlayerName.text = persistentName.scene1.playerName;
        displayPlayer2Name.text = persistentName.scene1.player2Name;
    }
}
