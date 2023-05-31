using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class persistentName : MonoBehaviour
{
    public static persistentName scene1;
    
    public TMP_InputField inputField;
    public TMP_InputField inputField2;
    public string playerName;
    public string player2Name;

    // On start of new scene dont destroy Name Manager gameobject
    public void Start()
    {
        if (scene1 == null)
        {
            scene1 = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        inputField.characterLimit = 10;
        inputField2.characterLimit = 10;
    }

    public void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log("Scene name is " + sceneName);

        if (sceneName == "Main Menu")
        {
            Destroy(gameObject);
        }
    }

    // Set the input field text to a variable
    public void setPlayerName()
    {
        playerName = inputField.text;

        if (playerName == "")
        {
            playerName = "Player 1";
        }

        Debug.Log(playerName);
        // Loads Game Screen
        SceneManager.LoadScene(4);
    }

    public void setPlayer2Name()
    {
        playerName = inputField.text;
        player2Name = inputField2.text;

        if (playerName == "")
        {
            playerName = "Player 1";
        }

        if (player2Name == "")
        {
            player2Name = "Player 2";
        }
        Debug.Log(playerName);
        Debug.Log(player2Name);
        // Loads Game Screen
        SceneManager.LoadScene(5);
    }
}
