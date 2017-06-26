using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addedSlotText : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        int i = GetComponentInParent<addedSlot>().getListpos() + 1;

        gameObject.GetComponent<Text>().text = "" + i;
		
	}
}
