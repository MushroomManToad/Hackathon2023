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
    // The card functions
    private ArrayList cardFunctions = new ArrayList();

    [SerializeField]
    private GameObject cardHolderPrefab, cardFunctionPrefab;

    [Header("xPos, yPos, yOffset")]
    [SerializeField]
    float[] posOffsetCardHolder, posOffsetCardFunction;

    [SerializeField]
    GameObject screen;

    [SerializeField]
    private Vector2 cardHolderArea;

    [SerializeField]
    private GameObject carryCardParent, flippyCardParent;

    private DeckCompiler compiler;

    [SerializeField]
    private GameObject playerObj;

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

        // Init the first Card Function
        createNewCardFunction();

        // Init the compiler
         compiler = new DeckCompiler(this);
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

    public GameObject getFlippyCardParent()
    {
        return flippyCardParent;
    }

    // Returns the cardholder currently being hovered. null if none.
    public CardHolder isOnCardHolder(Vector2 mousePos)
    {
        CardHolder retCH = null;
        Vector2 screenTransVar = Vector2.zero;

        foreach (CardHolder ch in cardHolders.Values)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)screen.GetComponent<Canvas>().transform,
                ch.transform.position,
                screen.GetComponent<Canvas>().worldCamera,
                out screenTransVar);
            if (mousePos.x >= screenTransVar.x - cardHolderArea[0] 
                && mousePos.x <= screenTransVar.x + cardHolderArea[0]
                && mousePos.y >= screenTransVar.y - cardHolderArea[1]
                && mousePos.y <= screenTransVar.y + cardHolderArea[1])
            {
                retCH = ch;
            }
        }
        return retCH;
    }

    // Returns the cardholder currently being hovered. null if none.
    public CardFunction isOnCardFunction(Vector2 mousePos)
    {
        CardFunction retCF = null;
        Vector2 screenTransVar = Vector2.zero;

        foreach (CardFunction cf in cardFunctions)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)screen.GetComponent<Canvas>().transform,
                cf.transform.position,
                screen.GetComponent<Canvas>().worldCamera,
                out screenTransVar);
            if (mousePos.x >= screenTransVar.x - cf.getCFDims().x
                && mousePos.x <= screenTransVar.x + cf.getCFDims().x
                && mousePos.y >= screenTransVar.y - cf.getCFDims().y
                && mousePos.y <= screenTransVar.y + cf.getCFDims().y)
            {
                retCF = cf;
            }
        }
        return retCF;
    }

    public CardHolder getCardHolderBySuit(Suit s)
    {
        return (CardHolder) cardHolders[s];
    }

    public void createNewCardFunction()
    {
        GameObject cardFunction = Instantiate(cardFunctionPrefab,
            new Vector3(posOffsetCardFunction[0], posOffsetCardFunction[1] + (cardFunctions.Count * posOffsetCardFunction[2]), transform.position.z),
            Quaternion.identity,
            screen.transform);

        CardFunction function = cardFunction.GetComponent<CardFunction>();
        function.setCanvas(screen.GetComponent<Canvas>());

        cardFunctions.Add(function);
    }

    public ArrayList getCardFunctions()
    {
        return cardFunctions;
    }

    public void compile()
    {
        compiler.runFunctions();
    }

    public void movePlayer(int amount)
    {
        PlayerScripts ps = playerObj.GetComponent<PlayerScripts>();
        switch(ps.getFacing())
        {
            case(Facing.UP):
                playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + amount, playerObj.transform.position.z);
                break;
            case (Facing.DOWN):
                playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y - amount, playerObj.transform.position.z);
                break;
            case (Facing.RIGHT):
                playerObj.transform.position = new Vector3(playerObj.transform.position.x + amount, playerObj.transform.position.y, playerObj.transform.position.z);
                break;
            case (Facing.LEFT):
                playerObj.transform.position = new Vector3(playerObj.transform.position.x - amount, playerObj.transform.position.y, playerObj.transform.position.z);
                break;
        }
    }

    public void rotatePlayer(bool clockwise)
    {
        if(clockwise)
        {
            PlayerScripts ps = playerObj.GetComponent<PlayerScripts>();
            switch (ps.getFacing())
            {
                case (Facing.UP):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    ps.setFacing(Facing.RIGHT);
                    break;
                case (Facing.DOWN):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    ps.setFacing(Facing.LEFT);
                    break;
                case (Facing.RIGHT):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    ps.setFacing(Facing.DOWN);
                    break;
                case (Facing.LEFT):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    ps.setFacing(Facing.UP);
                    break;
            }
        }
        else
        {
            PlayerScripts ps = playerObj.GetComponent<PlayerScripts>();
            switch (ps.getFacing())
            {
                case (Facing.UP):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    ps.setFacing(Facing.LEFT);
                    break;
                case (Facing.DOWN):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    ps.setFacing(Facing.RIGHT);
                    break;
                case (Facing.RIGHT):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    ps.setFacing(Facing.UP);
                    break;
                case (Facing.LEFT):
                    playerObj.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                    ps.setFacing(Facing.DOWN);
                    break;
            }
        }
    }
}
