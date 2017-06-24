using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedBP : MonoBehaviour {

    public Blueprint blueprint;

    public void setBlueprint(Blueprint bp)
    {
        blueprint = bp;
    }

    public Blueprint getBlueprint()
    {
        return blueprint;
    }
}
