﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Selectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool mouseOverObject;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1)) // feld wird deselektiert
        {
            GameObject.Find("selectHandler").GetComponent<selectedObject>().setTile(null);
            GameObject.Find("feldSelect").GetComponent<SpriteRenderer>().enabled = false;
        }

        if (mouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject.Find("selectHandler").GetComponent<selectedObject>().setTile(this.gameObject);

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
