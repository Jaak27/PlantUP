using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class skills : MonoBehaviour, IPointerEnterHandler
{
    // Test Skript um die Skils auszuprobieren

    public Text skillTest; // zeigt ob die Fähigkeit genutzt wird
    public Text skillInfo; // Text für das Infofenster für die Erklärungen zu den Fähigkeiten


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }


    public void useSkill()
    {
        skillTest.text = "" + gameObject.name + " benutzt :)";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Text für das Infofenster für die Erklärungen zu den Fähigkeiten
        skillInfo.text = "" + gameObject.name;
    }
}
