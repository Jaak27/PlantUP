using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false); // zu beginn nicht sichtbar
		
	}
	
	// Update is called once per frame
	void Update () {

        // Positon für das Infofenster für die Erklärungen zu den Fähigkeiten auf die Maus setzen

        Vector3 mousePositon = Input.mousePosition;
        //float testX = Mathf.Lerp(gameObject.GetComponent<RectTransform>().position.x, Input.mousePosition.x, Time.deltaTime * 20);
        //float testY = Mathf.Lerp(gameObject.GetComponent<RectTransform>().position.y, Input.mousePosition.y, Time.deltaTime * 20);
        gameObject.GetComponent<RectTransform>().position = mousePositon;
		
	}
}
