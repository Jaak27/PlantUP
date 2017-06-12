using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowPlant : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerPrototype player = GameObject.Find("Player1").GetComponent<PlayerPrototype>();
        IsTile tile = this.gameObject.GetComponent<IsTile>();
        float cost = GameObject.Find("BluePrint").GetComponent<Blueprint>().GetCost();
        if (tile != null && tile.canSustainPlant && !tile.getPlant())
        {
            if (cost >= 0 && player.GetPoints() >= cost)
            {
                tile.GrowPlant(player);
                player.AddPoints(-cost);
            }
            else
            {
                print("Nicht genug Energie! SPieler hat "+ player.GetPoints()+ " Punkte, BP kostet "+ cost);
            }
        }
    }
    
}
