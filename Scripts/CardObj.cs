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

    Vector2 initialPos;

    [SerializeField]
    private Canvas canvas;

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
            Debug.Log("Loaded image from " + "Sprites/" + card.getTexturePath());
            image.sprite = Resources.Load<Sprite>("Sprites/" + card.getTexturePath());
        }
    }


    // Movement handlers. These are kinda magic.
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out mousePos);

        transform.position = canvas.transform.TransformPoint(mousePos);
    }

    public void DropHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out mousePos);



        Vector2 snapPos = Vector2.zero;

        // Smart drop logic goes here.

        transform.position = canvas.transform.TransformPoint(snapPos);
    }

    public void pointerHandler(BaseEventData data)
    {
        // Empty Event
    }
}
