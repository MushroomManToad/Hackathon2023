using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        cardDataUpdate();
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

        }
    }
}
