using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowPlant : MonoBehaviour, IPointerClickHandler
{
    private Blueprint blueprint ;
    private PlayerPrototype player;

    public void OnPointerClick(PointerEventData eventData)
    {
        player = this.GetComponent<IsTile>().getPlayingField().players[0];
        blueprint = player.blueprints[0];

        IsTile tile = this.gameObject.GetComponent<IsTile>();
        float cost = blueprint.GetCost();
        if (tile != null && tile.canSustainPlant && !tile.getPlant())
        {
            if (cost >= 0 && player.GetPoints() >= cost)
            {
                tile.GrowPlant(player);
                player.AddPoints(-cost);
            }
            else
            {
                print("Nicht genug Energie! Spieler" + player.GetPlayerNum()+ " hat "+ player.GetPoints()+ " Punkte, BP kostet "+ cost);
            }
        }
    }
    
}
