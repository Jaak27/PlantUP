using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowPlant : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GroundTile groundTile = this.gameObject.GetComponent<GroundTile>();
        PlayerPrototype player = GameObject.Find("Player1").GetComponent<PlayerPrototype>();
        if (groundTile != null) { 
        groundTile.GrowPlant(player);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
