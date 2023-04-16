using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    private Facing facing = Facing.RIGHT;

    public Facing getFacing() { return facing; }
    public void setFacing(Facing f) { facing = f; }
}
