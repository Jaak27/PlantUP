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


        if (mouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!mainControl.getUseSkills())
                {
                    GameObject.Find("selectHandler").GetComponent<selectedObject>().setTile(this.gameObject.GetComponent<IsTile>());
                }
                else
                {
                    this.GetComponent<IsTile>().getPlayingField().replaceTile(this.GetComponent<IsTile>(), tileType.WATER);
                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!mainControl.getUseSkills())
                {
                    GameObject.Find("selectHandlerP2").GetComponent<selectedObject>().setTile(this.gameObject.GetComponent<IsTile>());
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
