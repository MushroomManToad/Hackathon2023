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

    public Card(Value v, Suit s)
    { 
        this.value = v;
        this.suit = s;
    }

    public Card()
    {
        this.value = Value.ONE;
        this.suit = Suit.CLUB;
    }

    // Getters and setters
    public Value getValue() { return value; }

    public void setValue(Value v) {this.value = v;}
    public void setSuit(Suit s) { this.suit = s; }

    public Suit getSuit() { return suit; }

    public bool getFlipped() { return flipped; }
    public bool flip() { flipped = !flipped; return flipped; }

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
                v = "ace";
                break;
            case (Value.TWO):
                v = "two";
                break;
            case (Value.THREE):
                v = "three";
                break;
            case (Value.FOUR):
                v = "four";
                break;
            case (Value.FIVE):
                v = "five";
                break;
            case (Value.SIX):
                v = "six";
                break;
            case (Value.SEVEN):
                v = "seven";
                break;
            case (Value.EIGHT):
                v = "eight";
                break;
            case (Value.NINE):
                v = "nine";
                break;
            case (Value.TEN):
                v = "ten";
                break;
            case (Value.JACK):
                v = "jack";
                break;
            case (Value.QUEEN):
                v = "queen";
                break;
            case (Value.KING):
                v = "king";
                break;
            default:
                v = string.Empty;
                break;
        }
        return s + "_" + v;
    }
}
