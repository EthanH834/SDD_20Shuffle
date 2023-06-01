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
    public TMPro.TMP_Text turnText;
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

    bool playerBustCheck = false;
    bool player2BustCheck = false;
    bool dealerBustCheck = false;
    int turncheck = 1;


    void Start()
    {
        // Enable/Disable UI elements
        winScreen.SetActive(false);
        gameScreen.SetActive(true);
        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        swapButton.gameObject.SetActive(false);

        // Set players name for persisent storage
        playerNewName = persistentName.scene1.playerName;
        player2NewName = persistentName.scene1.player2Name;

        dealClick();
    }

    // Shuffles the deck of cards and starts the hand    
    public void dealClick()
    {
        
        resultText.text = "";
        turnText.text = playerNewName + " Turn " + turncheck;

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

        // Enable/Disable UI elements
        hitButton2.gameObject.SetActive(false);
        standButton2.gameObject.SetActive(false);
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);

        playerBustCheck = false;
        player2BustCheck = false;
        dealerBustCheck = false;

        resultText.text = playerNewName + "'s turn";
    }

    // Plays when the "hit" button is clicked, gets a card for player 1
    public void hitClick()
    {
        playerController.getCard();
        playerScoreText.SetText(playerController.handValue.ToString());
        
        // if player busts then
        if (playerController.handValue > 20)
        {
            turnText.text = player2NewName + " Turn " + turncheck;
            playerBustCheck = true;

            // Enable/Disable UI elements
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            hitButton2.gameObject.SetActive(true);
            standButton2.gameObject.SetActive(true);
        
        }
    }

    // Plays when the "stand" button is clicked, ends turn for player 1
    public void standClick()
    {
        // Enable/Disable UI elements
        turnText.text = player2NewName + " Turn " + turncheck;
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        hitButton2.gameObject.SetActive(true);
        standButton2.gameObject.SetActive(true);
    }
    // Plays when the "hit" button is clicked, gets a card for player 2
    public void hitClick2()
    {
        player2Controller.getCard();
        player2ScoreText.SetText(player2Controller.handValue.ToString());
        
        // if player busts then
        if (player2Controller.handValue > 20)
        {
            turnText.text = "Dealer Turn " + turncheck;
            player2BustCheck = true;
            hitDealer();
        }
    }

    // Plays when the "stand" button is clicked, ends turn for player 2
    public void standClick2()
    {
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        turnText.text = "Dealer Turn " + turncheck;
        hitDealer();
        dealButton.gameObject.SetActive(true);
    }

    // Dealer's Turn
    private void hitDealer()
    {
        // When players are beating dealer, dealer draws a card
        while (dealerController.handValue < playerController.handValue || dealerController.handValue < player2Controller.handValue)
        {
            dealerController.getCard();
        }
        dealerScoreText.SetText(dealerController.handValue.ToString());

        if (dealerController.handValue > 20)
        {
            dealerBustCheck = true;
        }

        // Enable/Disable UI elements
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
        // Check who busted and sets thier score to an auto lose score
        if (dealerBustCheck == true)
        {
            dealerController.handValue = -1;
        }

        if (playerBustCheck == true)
        {
            playerController.handValue = -1;
        }

        if (player2BustCheck == true)
        {
            player2Controller.handValue = -1;
        }
        
        if (playerController.handValue > dealerController.handValue && playerController.handValue > player2Controller.handValue)
        {
            playerPointCounter += 10;
            playerWinsText.SetText(playerPointCounter.ToString()); 
            resultText.text = playerNewName + " won 10 points";
        }

        if (player2Controller.handValue > dealerController.handValue && player2Controller.handValue > playerController.handValue)
        {
            player2PointCounter += 10;
            player2WinsText.SetText(playerPointCounter.ToString()); 
            resultText.text = player2NewName + " won 10 points";
        }

        if (dealerController.handValue > playerController.handValue && dealerController.handValue > player2Controller.handValue)
        {
            dealerPointCounter += 10;
            dealerWinsText.SetText(dealerPointCounter.ToString()); 
            resultText.text = "dealer won 10 points";
        }
        
        if ((dealerController.handValue == playerController.handValue) || (dealerController.handValue == player2Controller.handValue) || (playerController.handValue == player2Controller.handValue))
        {
            resultText.text = "nobody won any points"; 
        }

        turncheck++;
        gameOver();
    }

   // Check if game is over
    public void gameOver()
    {
        if (playerPointCounter >= 100)
        {
            // Enable/Disable UI elements
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = playerNewName + " Wins";

            playerScore += 1;
            playerFinalScoreText.SetText(playerPointCounter.ToString());
            player2FinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
        }

        else if (player2PointCounter >= 100)
        {
            // Enable/Disable UI elements
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = player2NewName + " Wins";

            player2Score += 1;
            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            player2FinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());

            // Saves player 2 name and score
            gameManager.playerNewName = player2NewName;
            gameManager.playerScore = player2Score;

        }

        else if (dealerPointCounter >= 100)
        {
            // Enable/Disable UI elements
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = "Dealer Wins";

            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            player2FinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
        }
    }
}


