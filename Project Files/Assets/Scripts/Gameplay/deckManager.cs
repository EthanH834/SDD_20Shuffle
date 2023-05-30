using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deckManager : MonoBehaviour
{
    // Load card sprites into deck
    public Sprite[] cardSprites;
    public Sprite[] cardBack;

    public int[] cardValue = new int[55];

    int curIndex = 0;

    void Start()
    {
        assignCardValue();
    }

    // Assigns a numeric value to each card
    void assignCardValue()
    {
        int cardNum = 0;
        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardNum = i;
            // Assigns value to Jokers
            if (i > 51)
            {
                cardNum = 0;
            }

            else
            {
                // Assigns value to number cards and Aces
                cardNum = (cardNum % 13) + 1;
                // Assigns value to picture cards
                if (cardNum > 10 || cardNum == 0)
                {
                    cardNum = 10;
                }
            }
            cardValue[i] = cardNum++;
        }
        curIndex = 1;
    }

    // Shuffles the cards in the array 
    public void cardShuffle()
    {
       // Standard array data swapping technique
        for (int i = cardSprites.Length -1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValue[i];
            cardValue[i] = cardValue[j];
            cardValue[j] = value;
        }
        curIndex = 1;
    }

    public int dealCards(cardSetup cardSetup)
    {
        cardSetup.setSprite(cardSprites[curIndex]);
        cardSetup.setValue(cardValue[curIndex]);
        curIndex++;
        return cardSetup.getCardValue();
    }

    public Sprite displayBackCard()
    {
        return cardBack[0];
    }
}
