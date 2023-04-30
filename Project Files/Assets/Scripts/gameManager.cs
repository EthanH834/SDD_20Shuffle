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

    public playerController playerController;
    public playerController dealerController;

    public TMPro.TMP_Text playerScoreText;
    public TMPro.TMP_Text dealerScoreText;
    public TMPro.TMP_Text playerWinsText;
    public TMPro.TMP_Text dealerWinsText;
    public TMPro.TMP_Text resultText;

    public int dealerWinCounter = 0;
    public int playerWinCounter = 0;


    void Start()
    {
        // Add on click listeners to the buttons
        dealButton.onClick.AddListener(() => dealClick());
        hitButton.onClick.AddListener(() => hitClick());
        standButton.onClick.AddListener(() => standClick());
    }

    // Shuffles the deck of cards and starts the hand    
    private void dealClick()
    {
        resultText.text = "";

        playerController.resetHand();
        dealerController.resetHand();
        GameObject.Find("Deck").GetComponent<deckManager>().cardShuffle();
        playerController.startHand();
        dealerController.startHand();

        playerScoreText.SetText(playerController.handValue.ToString());
        dealerScoreText.SetText(dealerController.handValue.ToString());
        playerWinsText.SetText(playerWinCounter.ToString()); 
        dealerWinsText.SetText(dealerWinCounter.ToString());
    }

    //Plays when the "hit" button is clicked, gets a card for the player
    private void hitClick()
    {
        playerController.getCard();
        playerScoreText.SetText(playerController.handValue.ToString());
        
        if (playerController.handValue > 20)
        {
            dealerWinCounter += 10;
            dealerWinsText.SetText(dealerWinCounter.ToString());
            resultText.text = "dealer won 10 points";
            
            if (dealerWinCounter >= 100)
            {
                gameOver();
                playerController.resetHand();
                dealerController.resetHand();
            }
        }
    }

    //Plays when the "stand" button is clicked, ends turn for the player
    private void standClick()
    {
        hitDealer();
    }

    // Dealer's Turn
    private void hitDealer()
    {
        while (dealerController.handValue < playerController.handValue)
        {
            dealerController.getCard();
        }

        dealerScoreText.SetText(dealerController.handValue.ToString());
        if (playerController.handValue > dealerController.handValue || dealerController.handValue > 20)
        {
            if (playerController.handValue < 21)
            {
                playerWinCounter += 10;
                playerWinsText.SetText(playerWinCounter.ToString()); 
                resultText.text = "player won 10 points";
            }
        }

        else if (dealerController.handValue > 20 && playerController.handValue > 20)
        {
            dealerWinsText.SetText(dealerWinCounter.ToString());
            resultText.text = "nobody won any points"; 
        }

        else
        {
            dealerWinCounter += 10;
            dealerWinsText.SetText(dealerWinCounter.ToString()); 
            resultText.text = "dealer won 10 points";
        }
    }

    public void gameOver()
    {
        if (playerWinCounter > 100 || dealerWinCounter > 100)
        {
            Debug.Log("Still working on it");
        }

        else
        {
            Debug.Log("Still working on it");
        }
    }
}


