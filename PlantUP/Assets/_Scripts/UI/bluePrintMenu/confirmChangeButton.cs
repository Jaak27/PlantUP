using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class confirmChangeButton : MonoBehaviour, IPointerClickHandler
{

    public GameObject changeBP;
    public GameObject createBP;
    public selectedBP selected;
    public selectedBP selectedChange;

    public void OnPointerClick(PointerEventData eventData)
    {

        Destroy(selectedChange.getBlueprint().GetComponent<myAddedSlotGroup>().myGroup);
        selectedChange.getBlueprint().GetComponent<myAddedSlotGroup>().myGroup = selected.getBlueprint().GetComponent<myAddedSlotGroup>().myGroup;
        selectedChange.getBlueprint().setTypeSequence(selected.getBlueprint().GetTypeSequence());
        selectedChange.getBlueprint().SetSequence();
        selectedChange.getBlueprint().setHasChanged(true);
        selectedChange.getBlueprint().updateCost();


        Destroy(selected.getBlueprint().gameObject);

        changeBP.SetActive(false);
        createBP.SetActive(true);
    }
}
