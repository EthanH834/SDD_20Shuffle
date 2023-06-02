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
    public GameObject dealerSwapCard1;
    public GameObject dealerSwapCard2;
    public GameObject playerSwapCard;
    public GameObject player2SwapCard;

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
    bool playerTurnCheck = false;
    bool player2TurnCheck = false;

    int turncheck = 1;
    int jokerCheck;

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

        playerTurnCheck = true;
        player2TurnCheck = false;

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
        playerTurnCheck = true;
        player2TurnCheck = false;

        resultText.text = playerNewName + "'s turn";
        if (playerController.jokerFlag == true)
        {
            swapActive();
        }

        if (player2Controller.jokerFlag == true)
        {
            swapActive();
        }
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
            playerTurnCheck = false;
            player2TurnCheck = true;

            // Enable/Disable UI elements
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            hitButton2.gameObject.SetActive(true);
            standButton2.gameObject.SetActive(true);
        
        }

        if (playerController.jokerFlag == true)
        {
            swapActive();
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

        playerTurnCheck = false;
        player2TurnCheck = true;
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
            playerTurnCheck = false;
            player2TurnCheck = false;
            hitDealer();
        }

        if (player2Controller.jokerFlag == true)
        {
            swapActive();
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
        playerTurnCheck = false;
        player2TurnCheck = false;
    }

    // Turn on the swap button when joker is found in player's hand
    public void swapActive()
    {
        swapButton.gameObject.SetActive(true);
    }

    // When the "Swap" button is pressed, turn on card buttons
    public void swapPressed()
    {
        swapButton.gameObject.SetActive(false);
        dealerSwapCard1.gameObject.SetActive(true);
        dealerSwapCard2.gameObject.SetActive(true);  
        resultText.text = "Click Dealer Card to Swap With";
    }

    // Swaps the dealers first card
    public void swapCard1()
    {   
        dealerSwapCard1.gameObject.SetActive(false);
        dealerSwapCard2.gameObject.SetActive(false);  

        if (playerTurnCheck = true)
        {
            // Swap cards
            Vector3 tempPos = dealerSwapCard1.transform.parent.position;
            dealerSwapCard1.transform.parent.position = playerSwapCard.transform.parent.position;
            playerSwapCard.transform.parent.position = tempPos;

            // Change hand values to match
            dealerController.handValue = dealerController.handValue - dealerController.savedCardValue + playerController.savedCardValue;
            playerController.handValue = playerController.handValue - playerController.savedCardValue + dealerController.savedCardValue;

            // Change UI to match hand values
            playerScoreText.SetText(playerController.handValue.ToString());
            dealerScoreText.SetText(dealerController.handValue.ToString());
            resultText.text = "Cards Swapped";
        }

        if (player2TurnCheck = true)
        {
            // Swap cards
            Vector3 tempPos = dealerSwapCard1.transform.parent.position;
            dealerSwapCard1.transform.parent.position = player2SwapCard.transform.parent.position;
            player2SwapCard.transform.parent.position = tempPos;

            // Change hand values to match
            dealerController.handValue = dealerController.handValue - dealerController.savedCardValue + player2Controller.savedCardValue;
            playerController.handValue = player2Controller.handValue - player2Controller.savedCardValue + dealerController.savedCardValue;

            // Change UI to match hand values
            playerScoreText.SetText(player2Controller.handValue.ToString());
            dealerScoreText.SetText(dealerController.handValue.ToString());
            resultText.text = "Cards Swapped";
        }
    }

    //  Swaps the dealers second card
    public void swapCard2()
    {
        dealerSwapCard1.gameObject.SetActive(false);
        dealerSwapCard2.gameObject.SetActive(false);  

        if (playerTurnCheck = true)
        {
            // Swap cards
            Vector3 tempPos = dealerSwapCard2.transform.position;
            dealerSwapCard2.transform.position = playerSwapCard.transform.position;
            playerSwapCard.transform.position = tempPos;

            // Change hand values to match
            dealerController.handValue = dealerController.handValue - dealerController.savedCardValue2 + playerController.savedCardValue;
            playerController.handValue = playerController.handValue - playerController.savedCardValue + dealerController.savedCardValue2;

            // Change UI to match hand values
            playerScoreText.SetText(playerController.handValue.ToString());
            dealerScoreText.SetText(dealerController.handValue.ToString());
            resultText.text = "Cards Swapped";
        }

        if (player2TurnCheck = true)
        {
            // Swap cards
            Vector3 tempPos = dealerSwapCard2.transform.position;
            dealerSwapCard2.transform.position = player2SwapCard.transform.position;
            player2SwapCard.transform.position = tempPos;

            // Change hand values to match
            dealerController.handValue = dealerController.handValue - dealerController.savedCardValue2 + player2Controller.savedCardValue;
            playerController.handValue = player2Controller.handValue - player2Controller.savedCardValue + dealerController.savedCardValue2;

            // Change UI to match hand values
            playerScoreText.SetText(player2Controller.handValue.ToString());
            dealerScoreText.SetText(dealerController.handValue.ToString());
            resultText.text = "Cards Swapped";
        }
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

    public void savePlayerData(saveData saveData)
    {
        gameManager.playerNewName = player2NewName;
        gameManager.playerScore = player2Score;
        saveData.saveAllData();
    }
}


