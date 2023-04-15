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


            }
        }
    }

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
                if (subCards[1] != null)
                {
                    // Add the appropriate card to this.
                    labels.Add(((Card)subCards[1]).getValue(), new KeyPosPair(j, i));
                }
            }
        }
    }
}
