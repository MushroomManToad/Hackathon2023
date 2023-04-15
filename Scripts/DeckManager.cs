using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField]
    private Suit[] excludedSuits;
    [SerializeField]
    private Value[] excludedValues;
    // Might need a custom data type to exclude individual cards... :shrug:


    // The active deck with all of its cards.
    private ArrayList cards = new ArrayList();
    // The values currently in the cardHolders
    private Dictionary<Suit, ArrayList> cardHolderVals = new Dictionary<Suit, ArrayList>();
    // The cardHolders
    private Dictionary<Suit, CardHolder> cardHolders = new Dictionary<Suit, CardHolder>();

    [SerializeField]
    private GameObject cardHolderPrefab;

    [Header("xPos, yPos, yOffset")]
    [SerializeField]
    float[] posOffsetCardHolder;

    [SerializeField]
    GameObject screen;

    [SerializeField]
    private Vector2 cardHolderArea;

    [SerializeField]
    private GameObject carryCardParent;

    // Called first. Don't do anything else in any other class besides controlls with Awake
    private void Awake()
    {
        // Initialize Dictionary entry for each suit.
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            cardHolderVals.Add(s, new ArrayList());
        }
        // Set up the deck according to passed rules.
        setupDeck();

        // Init the card holders
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            if (cardHolderVals[s].Count > 0)
            {
                // Instantiate the card holder, adjust position, and set initial cards
                GameObject cardHolderS = Instantiate(cardHolderPrefab,
                    new Vector3(posOffsetCardHolder[0], posOffsetCardHolder[1] + (int)(s) * posOffsetCardHolder[2], transform.position.z),
                    Quaternion.identity,
                    screen.transform);
                CardHolder ch = cardHolderS.GetComponent<CardHolder>();
                ch.setCards(cardHolderVals[s]);
                ch.setSuit(s);
                ch.setDM(this);
                ch.updateCards();

                cardHolders.Add(s, ch);
            }
        }
    }

    // Tries to generate a full deck
    private void setupDeck()
    {
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            if(!isExcluded(s))
            {
                foreach (Value v in Enum.GetValues(typeof(Value)))
                {
                    if (!isExcluded(v))
                    {
                        // Add the card to the overall deck
                        cards.Add(new Card(v,s));

                        // Add the card to the appropriate CardHolderArray.
                        cardHolderVals[s].Add(new Card(v, s));
                    }
                }
            }
        }
    }

    private bool isExcluded(Suit s)
    {
        bool ret = false;
        foreach(Suit suit in excludedSuits)
        {
            if (s.Equals(suit))
            {
                ret = true; 
                break;
            }
        }
        return ret;
    }
    private bool isExcluded(Value v)
    {
        bool ret = false;
        foreach (Value val in excludedValues)
        {
            if (v.Equals(val))
            {
                ret = true;
                break;
            }
        }
        return ret;
    }

    public GameObject getCanvas()
    {
        return screen;
    }

    public GameObject getCarryCardParent()
    {
        return carryCardParent;
    }

    // Returns the cardholder currently being hovered. null if none.
    public CardHolder isOnCardHolder(Vector2 mousePos)
    {
        CardHolder retCH = null;
        foreach(CardHolder ch in cardHolders.Values)
        {
            if(mousePos.x >= ch.transform.position.x - posOffsetCardHolder[0] 
                && mousePos.x <= ch.transform.position.x + posOffsetCardHolder[0]
                && mousePos.y >= ch.transform.position.y - posOffsetCardHolder[1]
                && mousePos.y <= ch.transform.position.y + posOffsetCardHolder[1])
            {
                retCH = ch;
            }
        }
        return retCH;
    }

    public CardHolder getCardHolderBySuit(Suit s)
    {
        return (CardHolder) cardHolders[s];
    }
}
