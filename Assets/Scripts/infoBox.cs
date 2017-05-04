using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {

        // Positon für das Infofenster für die Erklärungen zu den Fähigkeiten auf die Maus setzen

        Vector3 mousePositon = Input.mousePosition;
        gameObject.GetComponent<RectTransform>().position = mousePositon;
		
	}
}
