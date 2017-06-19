using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedBP : MonoBehaviour {

    GameObject blueprint;

    public void setBlueprint(GameObject bp)
    {
        blueprint = bp;
    }

    public GameObject getBlueprint()
    {
        return blueprint;
    }
}
