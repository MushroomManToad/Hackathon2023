using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardobj : MonoBehaviour
{
    [SerializeField]
    private Value v;
    [SerializeField]
    private Suit s;
    Card card = new Card();
    // Start is called before the first frame update
    void Start()
    {
        card.setValue(v);
        card.setSuit(s);
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
        if (this.v != card.getValue()) { card.setValue(v); }
        if (this.s != card.getSuit()) { card.setSuit(s); }
    }
}
