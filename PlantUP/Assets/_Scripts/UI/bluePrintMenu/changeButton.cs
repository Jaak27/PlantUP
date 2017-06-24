using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class changeButton : MonoBehaviour, IPointerClickHandler
{
    public Blueprint newBp;
    //Menus
    public GameObject changeBP;
    public GameObject createBP;
    public selectedBP selected;
    public selectedBP selectedChange;

    public GameObject test;

    public void OnPointerClick(PointerEventData eventData)
    {

        selected.getBlueprint().gameObject.GetComponent<myAddedSlotGroup>().myGroup.SetActive(true);
        selectedChange.setBlueprint(selected.getBlueprint());

        newBp = Instantiate(selected.getBlueprint());

        selected.setBlueprint(newBp);
        if(selected.getBlueprint() != null)
        {
            changeBP.SetActive(true);
            createBP.SetActive(false);
        }

    }
}
