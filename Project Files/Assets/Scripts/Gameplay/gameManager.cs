using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class gameManager : MonoBehaviour
{
    public Button hitButton;
    public Button standButton;
    public Button dealButton;
    public Button swapButton;

    public playerController playerController;
    public playerController dealerController;

    public TMPro.TMP_Text playerScoreText;
    public TMPro.TMP_Text dealerScoreText;
    public TMPro.TMP_Text playerWinsText;
    public TMPro.TMP_Text dealerWinsText;
    public TMPro.TMP_Text playerWinNameText;
    public TMPro.TMP_Text resultText;
    public TMPro.TMP_Text turnText;
    public TMPro.TMP_Text winText;
    public TMPro.TMP_Text playerFinalScoreText;
    public TMPro.TMP_Text dealerFinalScoreText;

    public GameObject winScreen;
    public GameObject gameScreen;
    public GameObject dealerSwapCard1;
    public GameObject dealerSwapCard2;
    public GameObject playerSwapCard;

    public int dealerPointCounter = 0;
    public int playerPointCounter = 0;
    public int playerScore = 0;
    public string playerNewName;

    int jokerCheck;
    int turncheck = 1;
    bool playerTurnCheck = false;
    bool player2TurnCheck = false;

    void Start()
    {
        // Enable/Disable UI elements
        winScreen.SetActive(false);
        gameScreen.SetActive(true);
        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        swapButton.gameObject.SetActive(false);

        // Set player name for persisent storage
        playerNewName = persistentName.scene1.playerName;

        playerTurnCheck = true;
        dealClick();
    }

    // Shuffles the deck of cards and starts the hand    
    public void dealClick()
    {
        turnText.text = playerNewName + " Turn " + turncheck;
        resultText.text = "";

        playerController.resetHand();
        dealerController.resetHand();
        GameObject.Find("Deck").GetComponent<deckManager>().cardShuffle();
        playerController.startHand();
        dealerController.startHand();

        playerScoreText.SetText(playerController.handValue.ToString());
        dealerScoreText.SetText(dealerController.handValue.ToString());
        playerWinsText.SetText(playerPointCounter.ToString()); 
        dealerWinsText.SetText(dealerPointCounter.ToString());

        // Enable/Disable UI elements
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        swapButton.gameObject.SetActive(false);
        playerTurnCheck = true;

        if (playerController.jokerFlag == true)
        {
            swapActive();
        }

    }

    // Plays when the "hit" button is clicked, gets a card for the player
    public void hitClick()
    {
        playerController.getCard();
        playerScoreText.SetText(playerController.handValue.ToString());
        
        // if player busts then
        if (playerController.handValue > 20)
        {
            dealerPointCounter += 10;
            dealerWinsText.SetText(dealerPointCounter.ToString());
            resultText.text = "Dealer won 10 Points";

            // Enable/Disable UI elements
            dealButton.gameObject.SetActive(true);
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            gameOver();
            playerTurnCheck = false;
            player2TurnCheck = true;
        }

        if (playerController.jokerFlag == true)
        {
            swapActive();
        }
    }

    // Plays when the "stand" button is clicked, ends turn for the player
    public void standClick()
    {
        // Enable/Disable UI elements
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);

        turnText.text = "Dealer Turn " + turncheck;
        hitDealer();

        // Enable/Disable UI elements
        dealButton.gameObject.SetActive(true);
        playerTurnCheck = false;
        player2TurnCheck = true;
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

    //  Swaps the dealers second card
    public void swapCard2()
    {
        dealerSwapCard1.gameObject.SetActive(false);
        dealerSwapCard2.gameObject.SetActive(false);  
        
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

    // Dealer's Turn
    private void hitDealer()
    {
        playerTurnCheck = false;
        // When player is beating dealer, dealer draws a card
        while (dealerController.handValue < playerController.handValue)
        {
            dealerController.getCard();
        }

        dealerScoreText.SetText(dealerController.handValue.ToString());
        winnerCheck();
    }

    // Checks who won the round
    private void winnerCheck()
    {
        // If player score is greater and player didn't bust then player wins
        if (playerController.handValue > dealerController.handValue || dealerController.handValue > 20)
        {
            if (playerController.handValue < 21)
            {
                playerPointCounter += 10;
                playerWinsText.SetText(playerPointCounter.ToString()); 
                resultText.text = playerNewName + " won 10 Points";
            }
        }

        // If both bust or both scores equal then draw
        else if (dealerController.handValue > 20 && playerController.handValue > 20 || dealerController.handValue == playerController.handValue)
        {
            dealerWinsText.SetText(dealerPointCounter.ToString());
            resultText.text = "Nobody won any Points"; 
        }

        // Dealer win condition
        else
        {
            dealerPointCounter += 10;
            dealerWinsText.SetText(dealerPointCounter.ToString()); 
            resultText.text = "Dealer won 10 Points";
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
            playerWinNameText.text = playerNewName + " Final Score:";

            playerScore += 1;
            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
        }

        else if (dealerPointCounter >= 100)
        {
            // Enable/Disable UI elements
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = "Dealer wins";
            playerWinNameText.text = playerNewName + " Final Score:";

            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
        }
    }
}


