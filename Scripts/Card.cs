using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    // Card Fields, configurable from the Editor
    private Value value;
    private Suit suit;
    // Cards default to not flipped.
    private bool flipped = false;

    // Getters and setters
    public Value getValue() { return value; }

    public void setValue(Value v) {this.value = v;}
    public void setSuit(Suit s) { this.suit = s; }

    public Suit getSuit() { return suit; }

    public bool getFlipped() { return flipped; }
    public bool flip() { flipped = !flipped; return flipped; }

    public static Card newCard(Value value, Suit suit)
    {
        Card retCard = new Card();
        retCard.value = value;
        retCard.suit = suit;
        return retCard;
    }
}
