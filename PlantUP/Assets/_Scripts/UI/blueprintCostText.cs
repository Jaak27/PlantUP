using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blueprintCostText : MonoBehaviour {

    public selectedBP bp;

	// Update is called once per frame
	void Update () {
        
        if(bp.getBlueprintSelect() != null)
        {
            gameObject.GetComponent<Text>().text = "Blueprint Cost: " + bp.getBlueprintSelect().GetCostTypSequence();
        }
        
		
	}
}
