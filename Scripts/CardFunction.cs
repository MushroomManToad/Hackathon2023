using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFunction : MonoBehaviour
{
    [SerializeField]
    private Vector2 cfDims;

    [SerializeField]
    private Image cfBG;

    [SerializeField]
    private Canvas canvas;

    int openPos = 0;

    [SerializeField]
    int offsetByCard;

    Dictionary<int, ArrayList> functionState = new Dictionary<int, ArrayList>();

    

    public void updateFunctionState()
    {
        // If open pos is occupied by a non-flipped card, advance openPos by 1, and expand deck

        
    }

    public void addCardToFunction(Card card)
    {

        // Add the card
        if(functionState.ContainsKey(openPos))
        {
            if(card.getFlipped()) 
            { 
                changeSize(1);
                ArrayList arrayList = new ArrayList { card };
                functionState.Add(openPos, arrayList);
            }
            else
            {
                functionState[openPos].Add(card);
            }
        }
        else
        {
            ArrayList arrayList = new ArrayList { card };
            functionState.Add(openPos, arrayList);
        }

        // Set variable
        CardObj obj = card.getCardObj();
        if(obj != null )
        {
            obj.setCardFunction(this);
        }
        

        // Snap card to pos.
        CardObj cardObj = card.getCardObj();
        if(cardObj != null )
        {
            // Target pos
            Vector2 placePos = transform.position;

            // Adjusted by offset.
            placePos.x += openPos * offsetByCard;

            // Set position.
            cardObj.gameObject.transform.position = placePos;
        }

        // Advance openPos if not flipped
        if(!card.getFlipped()) changeSize(1);
    }

    public void changeSize(int amount)
    {
        openPos += amount;
        cfDims.x += offsetByCard * amount;
        cfBG.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cfDims.x);
        cfBG.transform.position = new Vector3((((float)cfDims.x) / 2.0f) + transform.position.x - (offsetByCard /2), transform.position.y, transform.position.z);
    }

    public Vector2 getCFDims()
    {
        return cfDims;
    }

    public void setCanvas(Canvas c)
    {
        this.canvas = c;
    }

    public int Count()
    {
        return functionState.Count;
    }

    public Dictionary<int, ArrayList> getFunctionState()
    {
        return functionState;
    }
}
