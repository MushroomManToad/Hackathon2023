using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPosPair
{
    public int key, pos;

    public KeyPosPair(int index, int subIndex)
    {
        this.key = index;
        this.pos = subIndex;
    }
}
