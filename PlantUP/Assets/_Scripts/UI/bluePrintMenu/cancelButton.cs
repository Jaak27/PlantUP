using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cancelButton : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().GetComponent<myAddedSlotGroup>().myGroup.gameObject);
        Destroy(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject);
    }

}
