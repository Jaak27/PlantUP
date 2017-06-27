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
                //GameObject.Find("bluePrintSelect").transform.position = this.transform.position;

            }

            Blueprint test = this.GetComponent<knowBlueprint>().getBlueprint();


            test.GetComponent<myAddedSlotGroup>().myGroup.SetActive(true);

        }
        else
        {
            Blueprint test = this.GetComponent<knowBlueprint>().getBlueprint();
            if(test != null)
            {
                test.GetComponent<myAddedSlotGroup>().myGroup.SetActive(false);
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
