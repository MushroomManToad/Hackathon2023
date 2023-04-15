using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardHolder : MonoBehaviour
{
    [SerializeField]
    private DeckManager dm;

    [SerializeField]
    private GameObject cardPrefab;

    // Should have 3 values.
    [SerializeField]
    float[] cardOffsets;

    private ArrayList livingCards = new ArrayList();


    private ArrayList cards;
    private Suit suit;

    // Index should always be valid. Update with a call to adjustPointer
    private int pointerIndex = 0;

    // Redraw cards after updating array
    public void updateCards()
    {
        pointerIndex = adjustPointer(pointerIndex);
        cards.Sort();
        refreshCards();
    }

    // Redraw the cards without checking order
    private void refreshCards()
    {
        // Destroy leftover gameobjects
        foreach (Card card in livingCards)
        {
            Destroy(card.getCardObj().gameObject);
        }
        livingCards.Clear();
        if (cards.Count > 0)
        {
            // Place the 3 cards
            for (int i = 0; i < 3; i++)
            {
                GameObject card = Instantiate(cardPrefab, new Vector3(transform.position.x + cardOffsets[i], transform.position.y, transform.position.z), Quaternion.identity, dm.getCanvas().transform);
                CardObj newCardObj = card.GetComponent<CardObj>();
                // I'm not null checking this. If it crashes, it crashes.
                newCardObj.setCanvas(dm.getCanvas().GetComponent<Canvas>());
                // Just set the values.
                newCardObj.setCardObjVals(((Card)cards[adjustPointer(pointerIndex + i - 1)]).getValue(), suit);
                newCardObj.getCard().setCardObj(newCardObj);
                newCardObj.setCardHolder(this);
                newCardObj.setDeckManager(dm);
                // Keep track of cards living in this card holder to kill at the appropriate time.
                livingCards.Add(newCardObj.GetComponent<CardObj>().getCard());
            }
        }
        else
        {
            // Empty array and reset size to 0
            livingCards.Clear();
        }
    }

    // Adjust the pointer when it's out of bounds.
    private int adjustPointer(int newPointerVal)
    {
        if(cards.Count > 0)
        {
            if (newPointerVal < 0)
            {
                newPointerVal = cards.Count - 1;
            }
            if (newPointerVal >= cards.Count)
            {
                newPointerVal = 0;
            }
        }
        else
        {
            newPointerVal = 0;
        }
        return newPointerVal;
    }

    // Setters for init.
    public void setSuit(Suit s)
    {
        this.suit = s;
    }

    public void setCards(ArrayList c)
    {
        this.cards = c;
    }

    public void setDM(DeckManager dm)
    {
        this.dm = dm;
    }

    public void removeLivingCard(Card card)
    {
        livingCards.Remove(card);
    }

    // This is the one that's gonna lag lol
    public void addCard(Card card)
    { 
        cards.Add(card);
        updateCards();
    }

    // This one is worse.
    public void removeCard(Card card)
    {
        // This card object is different, so we need to check each card for if it matches suit and val
        foreach (Card cardS in cards)
        {
            // Remove the first matching card, then break and update cards
            if (cardS.getValue() == card.getValue() && cardS.getSuit() == card.getSuit())
            {
                cards.Remove(cardS);
                break;
            }
        }
        updateCards();
    }

    public void addToPointerIndex(bool negative)
    {
        pointerIndex = adjustPointer(pointerIndex + (negative ? -1 : 1));
        refreshCards();
    }
}
