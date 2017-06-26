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
        selectedChange.getBlueprint().setTypeSequence(selected.getBlueprint().GetTypeSequence());

        selectedChange.getBlueprint().setHasChanged(true);

        Destroy(selected.getBlueprint().gameObject);

        changeBP.SetActive(false);
        createBP.SetActive(true);
    }
}
