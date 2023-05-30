using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    public cardSetup cardSetup;
    public deckManager deckManager;

    public GameObject[] hand;
    public int handValue = 0;
    public int cardIndex = 0;
    
    int cardCount = 0;

    public void startHand()
    {
        getCard();
        getCard();

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


        //Debug.Log("card value is " + cardValue);
        // Changes the value of aces
        if (cardValue == 1 && handValue < 10)  
        {
            cardValue = 11;
        }

        cardCount++;
        //Debug.Log("card count is " + cardCount);
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
    
