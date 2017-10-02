using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feldInfoUI : MonoBehaviour {

        bool onScreen; // legt fest ob sich das UI Element auf dem Bildschirm bewegen soll

    // Use this for initialization
    void Start()
        {

            onScreen = false; // zu beginnt nicht auf dem Bildschirm

    }

        // Update is called once per frame
        void Update()
        {

            if (onScreen == true) // falls true auf dem Bildschirm bewegen
             {
                //float newX = Mathf.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition.x, -120, 0.3f);
               // gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(newX,gameObject.GetComponent<Transform>().position.y , 1);
            }
            else // falls false offScreen bewegen
            {
                //float newX = Mathf.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition.x, 140, 0.3f);
               // gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(newX,gameObject.GetComponent<Transform>().position.y, 1);

            }

        if (Input.GetMouseButton(1)) // Feld wird beim klick auf die rechte Maustaste deselektiert, die Infofenster soll nicht länger angezeigt werden
        {
            onScreen = false;
        }
    }

        public void setUp(bool b)
        {
             onScreen = b;
           

        }

}
