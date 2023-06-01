using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    public gameManager gameManager;

    // Loads corresponding scene on button input
    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void toLeaderboard()
    {
        SceneManager.LoadScene(1);
    }

    public void toNameSelectSingle()
    {
        SceneManager.LoadScene(2);
    }
    
    public void toNameSelectTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void toGameSingle()
    {
        SceneManager.LoadScene(4);
    }

    public void toGameTwo()
    {
        SceneManager.LoadScene(5);
    }

    public void restartGame()
    {
        // Enable/Disable UI elements
        gameManager.winScreen.SetActive(false);
        gameManager.gameScreen.SetActive(true);
        gameManager.dealButton.gameObject.SetActive(true);
        gameManager.hitButton.gameObject.SetActive(false);
        gameManager.standButton.gameObject.SetActive(false);
        gameManager.swapButton.gameObject.SetActive(false);

        gameManager.dealerPointCounter = 0;
        gameManager.playerPointCounter = 0;

        gameManager.dealClick();
    }

    public void restartGame2(gameManagerTwo gameManagerTwo)
    {
        // Enable/Disable UI elements
        gameManagerTwo.winScreen.SetActive(false);
        gameManagerTwo.gameScreen.SetActive(true);
        gameManagerTwo.dealButton.gameObject.SetActive(true);
        gameManagerTwo.hitButton.gameObject.SetActive(false);
        gameManagerTwo.standButton.gameObject.SetActive(false);
        gameManagerTwo.swapButton.gameObject.SetActive(false);

        gameManagerTwo.dealerPointCounter = 0;
        gameManagerTwo.playerPointCounter = 0;

        gameManagerTwo.dealClick();
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
