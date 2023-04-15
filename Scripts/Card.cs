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

    // used for dynamic resource loading.
    public string getTexturePath()
    {
        // Get the suit
        string s = string.Empty;
        switch (suit)
        {
            case (Suit.CLUB):
                s = "club";
                break;
            case (Suit.SPADE):
                s = "spade";
                break;
            case (Suit.DIAMOND):
                s = "diamond";
                break;
            case (Suit.HEART):
                s = "heart";
                break;
            default:
                s = string.Empty;
                break;
        }
        // Get the value
        string v = string.Empty;
        switch (value)
        {
            case (Value.ONE):
                s = "one";
                break;
            case (Value.TWO):
                s = "two";
                break;
            case (Value.THREE):
                s = "three";
                break;
            case (Value.FOUR):
                s = "four";
                break;
            case (Value.FIVE):
                s = "five";
                break;
            case (Value.SIX):
                s = "six";
                break;
            case (Value.SEVEN):
                s = "seven";
                break;
            case (Value.EIGHT):
                s = "eight";
                break;
            case (Value.NINE):
                s = "nine";
                break;
            case (Value.TEN):
                s = "ten";
                break;
            case (Value.JACK):
                s = "jack";
                break;
            case (Value.QUEEN):
                s = "queen";
                break;
            case (Value.KING):
                s = "king";
                break;
            default:
                s = string.Empty;
                break;
        }
        return s + "_" + v;
    }
}
