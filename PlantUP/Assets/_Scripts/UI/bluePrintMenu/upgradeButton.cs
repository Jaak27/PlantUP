using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class upgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool overObject;
    public //int upgrade;
    UpgradeType upgrade;
    public Text upgradeInfo;
    public GameObject added;
    public Text addedSlotText;
    

    public Transform parentAdded;



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
                if(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint() == null)
                {
                    print("AAA");
                }
                GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().typeSequence.Add(upgrade);
                GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().hasChanged = true;


                addedSlotGroup group = GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup.GetComponent<addedSlotGroup>();

                GameObject addedSlot = Instantiate(added, GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup.transform);
                Text txt = Instantiate(addedSlotText, addedSlot.transform);
                //txt.GetComponent<RectTransform>().position = new Vector3(70, -4, 1);


                group.elements.Add(addedSlot.GetComponent<addedSlot>());
                addedSlot.GetComponent<addedSlot>().setListpos(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().GetTypeSequence().Count-1);
                addedSlot.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
                
                print("TEST " + upgrade);
            }
            upgradeInfo.text = "" + upgrade;
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
