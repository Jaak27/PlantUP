using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class tileControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool mouseOverObject;


    // Update is called once per frame
    void Update()
    {

        if (mainControl.getUseSkills() | Input.GetMouseButton(1)) // feld wird deselektiert
        {
            GameObject.Find("selectHandler").GetComponent<selectedObject>().setTile(null);
            GameObject.Find("feldSelect").GetComponent<SpriteRenderer>().enabled = false;
        }

        if (mouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(!mainControl.getUseSkills())
                {
                    GameObject.Find("selectHandler").GetComponent<selectedObject>().setTile(this.gameObject);
                }
                else
                {
                    this.GetComponent<IsTile>().getPlayingField().replaceTile(this.GetComponent<IsTile>(), tileType.WATER);
                }
                
                

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
