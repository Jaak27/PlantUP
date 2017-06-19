using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class changeBlueprint : MonoBehaviour, IPointerClickHandler
{
    public selectedBP selected;
    public GameObject addUpgrades;
    public GameObject createBP;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(selected.getBlueprint() != null)
        {

            addUpgrades.SetActive(true);
            createBP.SetActive(false);

        }
    }
}
