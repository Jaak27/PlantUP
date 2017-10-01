using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrintText : MonoBehaviour {

    public selectedBP test;
	
	// Update is called once per frame
	void Update () {

        if(test.getBlueprintSelect() != null)
        {

            gameObject.GetComponent<Text>().text = "" + test.getBlueprintSelect().ToString();
        }
        
		
	}
}
