using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillBarUI : MonoBehaviour {

    // Script dient zum bewegen des UI elements Skillbar in der die Skills angezeigt werden

    bool onScreen; // legt fest ob sich das UI Element auf dem Bildschirm bewegen soll

	// Use this for initialization
	void Start () {

        onScreen = false; // zu beginnt nicht auf dem Bildschirm
		
	}
	
	// Update is called once per frame
	void Update () {

        if(onScreen == true) // falls true auf dem Bildschirm bewegen
        {
            float newY = Mathf.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition.y, 18, 0.3f);
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, newY, 1);
        }
        else // falls false offScreen bewegen
        {
            float newY = Mathf.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition.y, -40, 0.3f);
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, newY, 1);

        }
	}

    public void setUp()
    {
        if(onScreen) // falls beim Klick auf den SKILL button onScreen = true wird onScreen auf false gesetzt (und andersherum)
        {
            onScreen = false;
        }
        else
        {
            onScreen = true;
        }
    }
}
