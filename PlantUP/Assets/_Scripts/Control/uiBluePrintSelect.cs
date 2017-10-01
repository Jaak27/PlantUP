using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiBluePrintSelect : MonoBehaviour {

    public selectedBP bp;
    public GameObject t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(bp.getBlueprintSelect() == null)
        {
            t.transform.position = new Vector3(-1000, -10000, 1);
        }
        else
        {
            //t.SetActive(true);
        }

		
	}
}
