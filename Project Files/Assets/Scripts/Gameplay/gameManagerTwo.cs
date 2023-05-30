using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManagerTwo : MonoBehaviour
{
    public Button hitButton;
    public Button hitButton2;
    public Button standButton;
    public Button standButton2;
    public Button dealButton;
    public Button swapButton;

    public playerController playerController;
    public playerController player2Controller;
    public playerController dealerController;
    public gameManager gameManager;

    public TMPro.TMP_Text playerScoreText;
    public TMPro.TMP_Text player2ScoreText;
    public TMPro.TMP_Text dealerScoreText;
    public TMPro.TMP_Text playerWinsText;
    public TMPro.TMP_Text player2WinsText;
    public TMPro.TMP_Text dealerWinsText;
    public TMPro.TMP_Text resultText;
    public TMPro.TMP_Text winText;
    public TMPro.TMP_Text playerFinalScoreText;
    public TMPro.TMP_Text player2FinalScoreText;
    public TMPro.TMP_Text dealerFinalScoreText;

    public GameObject winScreen;
    public GameObject gameScreen;
    public GameObject cardSwapButtons;

    public int dealerPointCounter = 0;
    public int playerPointCounter = 0;
    public int player2PointCounter = 0;
    
    public int playerScore = 0;
    public int player2Score = 0;
    public string playerNewName;
    public string player2NewName;

    void Start()
    {
        winScreen.SetActive(false);
        gameScreen.SetActive(true);
        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        swapButton.gameObject.SetActive(false);

        playerNewName = persistentName.scene1.playerName;
        player2NewName = persistentName.scene1.player2Name;

        Debug.Log("player name is" + playerNewName);
        dealClick();
    }

    // Shuffles the deck of cards and starts the hand    
    public void dealClick()
    {
        resultText.text = "";

        playerController.resetHand();
        player2Controller.resetHand();
        dealerController.resetHand();

        GameObject.Find("Deck").GetComponent<deckManager>().cardShuffle();

        playerController.startHand();
        player2Controller.startHand();
        dealerController.startHand();

        playerScoreText.SetText(playerController.handValue.ToString());
        player2ScoreText.SetText(player2Controller.handValue.ToString());
        dealerScoreText.SetText(dealerController.handValue.ToString());

        playerWinsText.SetText(playerPointCounter.ToString());
        player2WinsText.SetText(player2PointCounter.ToString()); 
        dealerWinsText.SetText(dealerPointCounter.ToString());

        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);

        resultText.text = playerNewName + "'s turn";
        Debug.Log(player2NewName);
    }

    // Plays when the "hit" button is clicked, gets a card for the player
    public void hitClick()
    {
        Debug.Log("hit clicked");
        playerController.getCard();
        playerScoreText.SetText(playerController.handValue.ToString());
        
        if (playerController.handValue > 20)
        {
            resultText.text = player2NewName + "'s turn";

            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            hitButton2.gameObject.SetActive(true);
            standButton2.gameObject.SetActive(true);
            
            if (dealerPointCounter >= 100)
            {
                gameOver();
                playerController.resetHand();
                player2Controller.resetHand();
                dealerController.resetHand();
            }
        }
    }

    // Plays when the "stand" button is clicked, ends turn for the player
    public void standClick()
    {
        resultText.text = player2NewName + "'s turn";

        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        hitButton2.gameObject.SetActive(true);
        standButton2.gameObject.SetActive(true);
    }

    public void hitClick2()
    {
        player2Controller.getCard();
        player2ScoreText.SetText(player2Controller.handValue.ToString());
        
        if (player2Controller.handValue > 20)
        {
            hitDealer();
            
            if (dealerPointCounter >= 100)
            {
                gameOver();
                playerController.resetHand();
                player2Controller.resetHand();
                dealerController.resetHand();
            }
        }
    }

    // Plays when the "stand" button is clicked, ends turn for the player
    public void standClick2()
    {
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        hitDealer();
        dealButton.gameObject.SetActive(true);
    }

    public void swapActive()
    {
        swapButton.gameObject.SetActive(true);
    }

    public void swapClick()
    {
        swapButton.gameObject.SetActive(false);
        cardSwapButtons.gameObject.SetActive(true);
    }

    // Dealer's Turn
    private void hitDealer()
    {
        while (dealerController.handValue < playerController.handValue)
        {
            dealerController.getCard();
        }
        dealerScoreText.SetText(dealerController.handValue.ToString());

        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        hitButton2.gameObject.SetActive(false);
        standButton2.gameObject.SetActive(false);
        winnerCheck();
    }

    // Checks who won the round
    private void winnerCheck()
    {
        if ((playerController.handValue > dealerController.handValue && playerController.handValue > player2Controller.handValue) || (dealerController.handValue > 20 && player2Controller.handValue > 20))
        {
            if (playerController.handValue < 21)
            {
                playerPointCounter += 10;
                playerWinsText.SetText(playerPointCounter.ToString()); 
                resultText.text = playerNewName + " won 10 points";
            }
        }

        else if ((player2Controller.handValue > dealerController.handValue && player2Controller.handValue > playerController.handValue) || (dealerController.handValue > 20 && playerController.handValue > 20))
        {
            if (playerController.handValue < 21)
            {
                player2PointCounter += 10;
                player2WinsText.SetText(playerPointCounter.ToString()); 
                resultText.text = player2NewName + " won 10 points";
            }
        }

        else if ((dealerController.handValue == playerController.handValue) || (dealerController.handValue == player2Controller.handValue) || (playerController.handValue == player2Controller.handValue))
        {
            resultText.text = "nobody won any points"; 
        }

        else if (dealerController.handValue > 20  && playerController.handValue > 20 && player2Controller.handValue > 20)
        {
            resultText.text = "nobody won any points"; 
        }

        else
        {
            dealerPointCounter += 10;
            dealerWinsText.SetText(dealerPointCounter.ToString()); 
            resultText.text = "dealer won 10 points";
        }
        gameOver();
    }


    public void gameOver()
    {
        if (playerPointCounter >= 10)
        {
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = playerNewName + " wins";
            playerScore += 1;
            playerFinalScoreText.SetText(playerPointCounter.ToString());
            player2FinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
            Debug.Log("player's current wins are" + playerScore);

        }

        else if (player2PointCounter >= 10)
        {
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = player2NewName + " wins";
            player2Score += 1;
            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            player2FinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
            Debug.Log("player 2 current wins are" + player2Score);

            gameManager.playerNewName = player2NewName;
            gameManager.playerScore = player2Score;

        }

        else if (dealerPointCounter >= 100)
        {
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = "dealer wins";
            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            player2FinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
        }
    }
}


