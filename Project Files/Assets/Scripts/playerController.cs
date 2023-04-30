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

    public void startHand()
    {
        getCard();
        getCard();
    }

    public int getCard()
    {
        int cardValue = deckManager.dealCards(hand[cardIndex].GetComponent<cardSetup>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;
        if (cardValue == 1 || cardValue == 11)
        {
            if (handValue < 10)
            {
                cardSetup.setValue(11);
            }

            else 
            {
                cardSetup.setValue(1);
            }
        }

        cardIndex++;
        return handValue;
    }

    // Resets the hand on round end
    public void resetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<cardSetup>().resetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
    }
}
    
