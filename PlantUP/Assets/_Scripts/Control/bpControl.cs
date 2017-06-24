using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bpControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool mouseOverObject;
    public Text blueprintInfo;


    // Update is called once per frame
    private void Start()
    {

    }

    void Update()
    {
        if (mouseOverObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                    GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().setBlueprint(this.gameObject.GetComponent<knowBlueprint>().getBlueprint());

            }

            Blueprint test = this.GetComponent<knowBlueprint>().getBlueprint();

            blueprintInfo = GameObject.Find("txt_BlueprintInfo").GetComponent<Text>();

            blueprintInfo.text = "" + test.ToString();







        }
        else
        {

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
