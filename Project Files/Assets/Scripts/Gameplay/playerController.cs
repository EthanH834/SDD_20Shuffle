using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    public cardSetup cardSetup;
    public deckManager deckManager;
    public gameManager gameManager;

    public GameObject[] hand;
    public int handValue = 0;
    public int cardIndex = 0;
    
    int cardCount = 0;
    public int savedCardValue = 0;
    public int savedCardValue2 = 0;
    public bool jokerFlag = false;

    public void startHand()
    {
        jokerFlag = false;
        getCard();
        savedCardValue = handValue;
        getCard();
        savedCardValue2 = handValue - savedCardValue;

        // Corrects ace check if second card equals 10
        if (handValue > 20)
        {
            handValue = handValue - 10;
        }

    }

    // Deals a single card
    public int getCard()
    {
        int cardValue = deckManager.dealCards(hand[cardIndex].GetComponent<cardSetup>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;

        // Changes the value of aces
        if (cardValue == 1 && handValue < 10)  
        {
            cardValue = 11;
        }

        // Checks if card is a joker
        if (cardValue == 0)
        {
            jokerFlag = true;
        }

        cardCount++;
        handValue += cardValue;
        cardIndex++;
        return handValue;
    }

    // Resets the hand on round end
    public void resetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<cardSetup>().resetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
    }
}
    
