using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class changeButton : MonoBehaviour, IPointerClickHandler
{
    public Blueprint newBp;
    public GameObject newGroup;

    //Menus
    public GameObject changeBP;
    public GameObject createBP;
    public selectedBP selected;
    public selectedBP selectedChange;

    public GameObject test;

    public void OnPointerClick(PointerEventData eventData)
    {

        selected.getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup.SetActive(false);
        selectedChange.setBlueprint(selected.getBlueprint());

        newBp = Instantiate(selected.getBlueprint());
        newGroup = Instantiate(selected.getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup, changeBP.transform);

        newBp.GetComponent<myAddedSlotGroup>().myGroup = newGroup;

        selected.setBlueprint(newBp);
        selected.getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup.SetActive(true);
        if (selected.getBlueprint() != null)
        {
            changeBP.SetActive(true);
            createBP.SetActive(false);
        }

    }
}
