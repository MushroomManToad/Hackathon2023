using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardObj : MonoBehaviour
{
    [SerializeField]
    private Value v;
    [SerializeField]
    private Suit s;
    [SerializeField]
    Image image;
    Card card = new Card();

    // Track the active card holder containing it. Can be null when not in a card holder
    CardHolder ch = null;

    Vector2 initialPos;

    [SerializeField]
    private Canvas canvas;

    private DeckManager dm;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        cardDataUpdate();

        // UI Thing
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            transform.position,
            canvas.worldCamera,
            out initialPos);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cardDataUpdate();
    }

    // Each frame checks and updates the card's visuals if the card has changed in editor or for
    // Any other reason.
    private void cardDataUpdate()
    {
        bool flag = false;
        if (this.v != card.getValue()) { card.setValue(v); flag = true; }
        if (this.s != card.getSuit()) { card.setSuit(s); flag = true; }
        if (flag)
        {
            // Load image if anything has updated.
            image.sprite = Resources.Load<Sprite>("Sprites/" + card.getTexturePath());
        }
    }


    // Movement handlers. These are kinda magic.
    public void DragHandler(BaseEventData data)
    {
        // Tracker to fix a weird bug.
        isMoving = true;

        // Adjust the position
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out mousePos);

        transform.position = canvas.transform.TransformPoint(mousePos);

        // Set parent
        if(transform.parent != dm.getCarryCardParent())
        {
            transform.SetParent(dm.getCarryCardParent().transform);
        }

        // Remove from appropriate card holder's livingCards and cards.
        if (ch != null)
        {
            ch.removeLivingCard(card);
            ch.removeCard(card);
            ch = null;
        }
    }

    public void DropHandler(BaseEventData data)
    {
        // Only drop if selected
        if(isMoving)
        {
            PointerEventData pointerData = (PointerEventData)data;

            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out mousePos);

            // Set parent
            transform.SetParent(transform.parent.transform);

            //Vector2 snapPos = Vector2.zero;

            // Smart drop logic goes here.

            // Add to CardHolder on Drop if it's over it. Also mark CardObj for Destruction
            if(dm.isOnCardHolder(mousePos) != null)
            {
                dm.getCardHolderBySuit(getCard().getSuit()).addCard(card);
                Destroy(gameObject);
            }

            //transform.position = canvas.transform.TransformPoint(snapPos);
            isMoving = false;
        }
    }

    public void ScrollHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        if (ch != null)
        {
            ch.addToPointerIndex(pointerData.scrollDelta.y < 0.0f);
        }
    }

    public void pointerHandler(BaseEventData data)
    {
        // Empty Event
    }

    public void setCardObjVals(Value v, Suit s)
    {
        this.v = v;
        this.s = s;
    }

    public Card getCard()
    {
        return card;
    }

    public void setCanvas(Canvas screen)
    {
        canvas = screen;
    }

    public void setCardHolder(CardHolder holder)
    {
        this.ch = holder;
    }

    public void setDeckManager(DeckManager deckManager)
    {
        this.dm = deckManager;
    }
}
