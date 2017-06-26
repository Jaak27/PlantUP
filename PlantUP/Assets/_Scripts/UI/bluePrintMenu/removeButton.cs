using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class removeButton : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        int i = gameObject.GetComponent<addedSlot>().getListpos();
        addedSlotGroup group = GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup.GetComponent<addedSlotGroup>();
        group.elements.RemoveAt(i);
        GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().hasChanged = true;


        for (int j = 0; j < group.elements.Count; j++)
        {
            if(group.elements[j].getListpos() > i)
            {
                group.elements[j].setListpos((group.elements[j].getListpos() -1));
            }
        }
        
        GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().GetTypeSequence().RemoveAt(i);
        GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().SetSequence();
        Destroy(gameObject);
    }
}
