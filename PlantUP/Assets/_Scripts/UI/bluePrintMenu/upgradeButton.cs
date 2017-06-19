using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class upgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool overObject;
    public int upgrade;

    void Start()
    {
        overObject = false;
    }

    void Update()
    {

        if(overObject)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().GetComponent<knowBlueprint>().getBlueprint().GetSequence().Add(upgrade);
                print("TEST " + upgrade);
            }
        }
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        overObject = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overObject = false;
    }
}
