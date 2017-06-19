using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knowBlueprint : MonoBehaviour {

    Blueprint blueprint;
    
    public void setBlueprint(Blueprint bp)
    {
        blueprint = bp;
    }

    public Blueprint getBlueprint()
    {
        return blueprint;
    }
	
}
