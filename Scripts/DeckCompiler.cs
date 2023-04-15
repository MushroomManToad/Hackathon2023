using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCompiler
{
    DeckManager dm;

    private ArrayList cardFunctions = new ArrayList();

    Dictionary<Value, KeyPosPair> labels = new Dictionary<Value, KeyPosPair>();

    public DeckCompiler(DeckManager deckManager) 
    { 
        this.dm = deckManager;
        this.cardFunctions = dm.getCardFunctions();
    }

    // Call to run the game.
    public void runFunctions()
    {
        compile();
        run();
    }

    private void compile()
    {
        identifyLabels();
    }

    // Walk the pointer through each function and act accordingly.
    private void run()
    {
        // Pointer, first value is the function id, second value is the index of the subarray to check.
        KeyPosPair rsp = new KeyPosPair(0,0);

        Value prev = Value.ONE;
        bool validPrev = false;
        // Label, # of times
        for(int i = 0; i < cardFunctions.Count; i++)
        {
            for(int j = 0; j < ((CardFunction)cardFunctions[i]).getFunctionState().Count; j++)
            {
                rsp.key = i;
                rsp.pos = j;

                ArrayList cardsAtPos = (((CardFunction)cardFunctions[i]).getFunctionState())[j];

                // Eventually, replace this with a dynamic function system. For now, tho,
                // level 1 is hardcoded D:
                if (cardsAtPos.Count > 1 || (cardsAtPos.Count == 1 && ((Card)cardsAtPos[0]).getFlipped()))
                {
                    // Do nothing, this is a label.
                }
                else
                {
                    Card card = (Card)cardsAtPos[0];
                    // Number card
                    if ((int)card.getValue() < 10)
                    {
                        if(validPrev)
                        {
                            // Do the loop, Value + 1 times
                            for(int k = 0; k < (int)card.getValue() + 1; k++)
                            {
                                functionLoopFrom(labels[prev]);
                            }
                            // Consume validprev
                            validPrev = false;
                        }
                        else
                        {
                            // Set these variables for usage in the loop.
                            prev = card.getValue();
                            validPrev = true;
                        }
                    }
                    else
                    {
                        // Function cards
                        if(validPrev)
                        {
                            Debug.LogError("ERROR: Found extra value card before a face card. Discarding prev.");
                            validPrev = false;
                        }
                        else
                        {
                            faceFunctions(card);
                        }
                    }
                }
            }
        }
    }

    private void functionLoopFrom(KeyPosPair rax)
    {
        KeyPosPair rbx = new KeyPosPair(rax.key, rax.pos);
        bool condition = false;
        // While we haven't reached a closing block OR we haven't reached the end.
        while(!condition)
        {
            // Vars
            Value prev = Value.ONE;
            bool validPrev = false;
            ArrayList cardsAtPos = (((CardFunction)cardFunctions[rax.key]).getFunctionState())[rax.pos];
            if(cardsAtPos.Count > 1)
            {
                // Do nothing and advance pointer
            }
            else
            {
                if (cardsAtPos.Count == 1 && ((Card)cardsAtPos[0]).getFlipped())
                {
                    condition = true;
                    rax.key = rbx.key;
                    rax.pos = rbx.pos;
                    continue;
                }
                // Do function stuff
                Card card = (Card)cardsAtPos[0];
                // Number card
                if ((int)card.getValue() < 10)
                {
                    if (validPrev)
                    {
                        // Do the loop, Value + 1 times
                        for (int k = 0; k < (int)card.getValue() + 1; k++)
                        {
                            functionLoopFrom(labels[prev]);
                        }
                        // Consume validprev
                        validPrev = false;
                    }
                    else
                    {
                        // Set these variables for usage in the loop.
                        prev = card.getValue();
                        validPrev = true;
                    }
                }
                else
                {
                    // Function cards
                    if (validPrev)
                    {
                        Debug.LogError("ERROR: Found extra value card before a face card. Discarding prev.");
                        validPrev = false;
                    }
                    else
                    {
                        faceFunctions(card);
                    }
                }
            }


            // Update rax manually
            // If we can advance in subindex, advance in subindex.
            if(rax.pos + 1 < (((CardFunction)cardFunctions[rax.key]).getFunctionState()).Count)
            {
                rax.pos += 1;
            }
            // If we can't advance in subindex, check if we can advance in index.
            else
            {
                // If we can, do
                if(rax.key + 1 < cardFunctions.Count -1)
                {
                    rax.pos += 1;
                }
                // Else return.
                else
                {
                    Debug.LogError("ERROR: No closing bracket on loop found. Returning to rsp.");
                    condition = true;
                }
            }
        }
    }

    private void faceFunctions(Card card)
    {
        switch (card.getValue())
        {
            case Value.JACK:
                Debug.Log("JACK");
                break;
            case Value.QUEEN:
                Debug.Log("QUEEN");
                break;
            case Value.KING:
                Debug.Log("KING");
                break;
        }
    }
    // END LEVEL 1

    // Find all the labelled functions for when they get called.
    private void identifyLabels()
    {
        // Go through each function
        for(int j = 0; j < cardFunctions.Count; j++)
        {
            CardFunction function = (CardFunction) cardFunctions[j];
            // Get the data from the function.
            Dictionary<int, ArrayList> cIF = function.getFunctionState();
            // Read each card (or card pair)
            foreach (int i in cIF.Keys)
            {
                ArrayList subCards = cIF[i];
                if (subCards.Count > 1)
                {
                    // Add the appropriate card to this.
                    labels.Add(((Card)subCards[1]).getValue(), new KeyPosPair(j, i));
                }
            }
        }
    }
}
