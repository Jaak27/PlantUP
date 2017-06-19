using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class destroyButton : MonoBehaviour, IPointerClickHandler
{

    public GameObject bp;
    public Transform parent;
    public GameObject addUpgrades;
    public GameObject createBP;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint());

    }
}
