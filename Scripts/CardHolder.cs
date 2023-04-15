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


    private ArrayList cards;
    private Suit suit;

    // Index should always be valid. Update with a call to adjustPointer
    private int pointerIndex = 3;

    public void updateCards()
    {
        sortCards();
        if(cards.Count > 0)
        {
            // Place the 3 cards
            for(int i = 0; i < 3; i++)
            {
                GameObject card = Instantiate(cardPrefab, new Vector3(transform.position.x + cardOffsets[i], transform.position.y, transform.position.z), Quaternion.identity, dm.getCanvas().transform);
                CardObj newCardObj = card.GetComponent<CardObj>();
                // I'm not null checking this. If it crashes, it crashes.
                newCardObj.setCanvas(dm.getCanvas().GetComponent<Canvas>());
                // Just set the values.
                newCardObj.setCardObjVals(((Card) cards[adjustPointer(pointerIndex + i - 1)]).getValue(), suit);
            }           
        }   
    }
    
    // Adjust the pointer when it's out of bounds.
    private int adjustPointer(int newPointerVal)
    {
        if(newPointerVal < 0)
        {
            newPointerVal = cards.Count;
        }
        if(newPointerVal >= cards.Count) 
        {
            newPointerVal = 0;
        }
        return newPointerVal;
    }

    // Hecking insertion sort. [heck] you.
    private void sortCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int minDex = findMinFrom(i);
            Card card1 = (Card) cards[i];
            Card card2 = (Card)cards[minDex];

            cards[i] = card2;
            cards[minDex] = card1;
        }
    }

    // This is stupid and inefficient. But I could figure it out at midnight.
    private int findMinFrom(int index)
    {
        Card minCard = (Card) cards[index];
        int minDex = index;
        for(int i = index; i < cards.Count; i++)
        {
            if ((int)((Card)cards[i]).getValue() < (int)minCard.getValue())
            {
                minCard = (Card)cards[i];
                minDex = index;
            }
        }
        return minDex;
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
}