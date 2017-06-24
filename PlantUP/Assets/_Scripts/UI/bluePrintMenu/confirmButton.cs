using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class confirmButton : MonoBehaviour, IPointerClickHandler
{
    Blueprint test;

    //Blueprint Slot Create

    public GameObject bpSlotCreate;
    GameObject newBpSlotCreate;


    // BLueprint Slot Plant

    public GameObject bpSlotPlant;
    GameObject newBpSlotPlant;



    //Parent Object
    public Transform parentCreate;
    public Transform parentPlant;
    public Transform parentChange;

    //Menus
    public GameObject addUpgrades;
    public GameObject createBP;

    public void OnPointerClick(PointerEventData eventData)
    {
        test = Instantiate(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint());


        newBpSlotCreate = Instantiate(bpSlotCreate, parentCreate);
        newBpSlotPlant = Instantiate(bpSlotPlant, parentPlant);

        test.GetComponent<myAddedSlotGroup>().myGroup.transform.parent = parentChange;
        test.GetComponent<myAddedSlotGroup>().myGroup.GetComponent<addedSlotGroup>().mybp = test;

        newBpSlotCreate.GetComponent<knowBlueprint>().setBlueprint(test);
        newBpSlotPlant.GetComponent<knowBlueprint>().setBlueprint(test);

        Destroy(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject);


    }
}
