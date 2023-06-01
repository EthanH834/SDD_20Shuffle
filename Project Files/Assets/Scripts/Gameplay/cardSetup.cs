using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSetup : MonoBehaviour
{
    public int value = 0;

    // Gets the value of a card
    public int getCardValue()
    {
        return value;
    }

    // Sets the value of a card to the ccorresponding value
    public void setValue(int newValue)
    {
        value = newValue;
    }

    // Sets the sprite of the card according to shuffled array
    public void setSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    // Resets value of card to zero and hides the sprite
    public void resetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<deckManager>().displayBackCard();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}