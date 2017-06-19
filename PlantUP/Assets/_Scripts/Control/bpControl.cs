using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bpControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool mouseOverObject;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1)) // feld wird deselektiert
        {

        }

        if (mouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                    GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().setBlueprint(this.gameObject);

            }
        }


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
