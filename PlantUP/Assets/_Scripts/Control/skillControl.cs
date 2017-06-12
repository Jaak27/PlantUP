using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class skillControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Test Skript um die Skils auszuprobieren
    bool mouseOverObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(1)) // feld wird deselektiert
        {
            mainControl.setUseSkills(false);
        }

        if (mouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))

                mainControl.setUseSkills(true);
                //this.GetComponent<IsTile>().getPlayingField().replaceTile(this.GetComponent<IsTile>(), tileType.WATER);

            }
        }

    

    // test Methode um den Skill zu "benutzen"
    public void useSkill()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverObject = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverObject = false;
    }

}
