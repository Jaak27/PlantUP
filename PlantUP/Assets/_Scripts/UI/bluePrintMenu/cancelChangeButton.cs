using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cancelChangeButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject createPanel;
    public GameObject changePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject);

        createPanel.SetActive(true);
        changePanel.SetActive(false);
    }
}
