using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowPlant : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        IsTile tile = this.gameObject.GetComponent<IsTile>();
        PlayerPrototype player = GameObject.Find("Player1").GetComponent<PlayerPrototype>();
        if (tile != null) { 
        tile.GrowPlant(player);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
