using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class createBlueprint : MonoBehaviour, IPointerClickHandler{

    //Blueprint
    public Blueprint bp;
    public addedSlotGroup slotGroup;

    Blueprint newBp;
    addedSlotGroup newAddedSlotGroup;


    //Menus
    public GameObject addUpgrades;
    public GameObject createBP;
    public PlayerPrototype player;

   



    public void OnPointerClick(PointerEventData eventData)
    {

        if(player.blueprints.Count < 6)
        {
            newBp = Instantiate(bp);
            newAddedSlotGroup = Instantiate(slotGroup, addUpgrades.transform);

            newBp.gameObject.GetComponent<myAddedSlotGroup>().myGroup = newAddedSlotGroup.gameObject;
            GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().setBlueprint(newBp);

       
            newAddedSlotGroup.setMyBp(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint());

            addUpgrades.SetActive(true);
            createBP.SetActive(false);


        }

    }

}
