using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class createBlueprint : MonoBehaviour, IPointerClickHandler{

    //Blueprint
    public Blueprint bp;
    Blueprint newBp;

    //Blueprint Slot Create

    public GameObject bpSlotCreate;
    GameObject newBpSlotCreate;


    // BLueprint Slot Plant

    GameObject newBpSlotPlant;



    //Parent Object
    public Transform parentCreate;
    public Transform parentPlant;

    //Menus
    public GameObject addUpgrades;
    public GameObject createBP;

    public void OnPointerClick(PointerEventData eventData)
    {
        newBp = Instantiate(bp);
        newBpSlotCreate = Instantiate(bpSlotCreate, parentCreate);

        newBpSlotPlant= Instantiate(bpSlotCreate, parentPlant);
        newBpSlotPlant.AddComponent<createPlant>();

        newBpSlotCreate.GetComponent<knowBlueprint>().setBlueprint(newBp);
        newBpSlotPlant.GetComponent<knowBlueprint>().setBlueprint(newBp);

        GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().setBlueprint(newBpSlotCreate);
        addUpgrades.SetActive(true);
        createBP.SetActive(false);
    }

}
