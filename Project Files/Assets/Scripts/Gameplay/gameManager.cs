using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public TMPro.TMP_Text resultText;
    public TMPro.TMP_Text winText;
    public TMPro.TMP_Text playerFinalScoreText;
    public TMPro.TMP_Text dealerFinalScoreText;

    public GameObject winScreen;
    public GameObject gameScreen;
    public GameObject cardSwapButtons;

    public int dealerPointCounter = 0;
    public int playerPointCounter = 0;
    public int playerScore = 0;
    public string playerNewName;

    void Start()
    {
        winScreen.SetActive(false);
        gameScreen.SetActive(true);
        dealButton.gameObject.SetActive(true);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        swapButton.gameObject.SetActive(false);

        playerNewName = persistentName.scene1.playerName;

        Debug.Log("player name is" + playerNewName);
        dealClick();
    }

    // Shuffles the deck of cards and starts the hand    
    public void dealClick()
    {
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

        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
    }

    // Plays when the "hit" button is clicked, gets a card for the player
    public void hitClick()
    {
        playerController.getCard();
        playerScoreText.SetText(playerController.handValue.ToString());
        
        if (playerController.handValue > 20)
        {
            dealerPointCounter += 10;
            dealerWinsText.SetText(dealerPointCounter.ToString());
            resultText.text = "dealer won 10 points";
            dealButton.gameObject.SetActive(true);
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            gameOver();
            
            if (dealerPointCounter >= 100)
            {
                gameOver();
                playerController.resetHand();
                dealerController.resetHand();
            }
        }
    }

    // Plays when the "stand" button is clicked, ends turn for the player
    public void standClick()
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
        winnerCheck();
    }

    // Checks who won the round
    private void winnerCheck()
    {
        if (playerController.handValue > dealerController.handValue || dealerController.handValue > 20)
        {
            if (playerController.handValue < 21)
            {
                playerPointCounter += 10;
                playerWinsText.SetText(playerPointCounter.ToString()); 
                resultText.text = "player won 10 points";
            }
        }

        else if (dealerController.handValue > 20 && playerController.handValue > 20 || dealerController.handValue == playerController.handValue)
        {
            dealerWinsText.SetText(dealerPointCounter.ToString());
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
            winText.text = "player wins";
            playerScore += 1;
            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
            Debug.Log("player's current wins are" + playerScore);

            
        }

        else if (dealerPointCounter >= 100)
        {
            winScreen.SetActive(true);
            gameScreen.SetActive(false);
            winText.text = "dealer wins";
            playerFinalScoreText.SetText(playerPointCounter.ToString()); 
            dealerFinalScoreText.SetText(dealerPointCounter.ToString());
        }
    }
}


