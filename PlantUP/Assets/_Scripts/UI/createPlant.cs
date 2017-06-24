using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class createPlant : MonoBehaviour, IPointerClickHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        IsTile tile = GameObject.Find("selectHandler").GetComponent<selectedObject>().getTile().GetComponent<IsTile>();

        GameObject.Find("bpSelectHandlerPlant").GetComponent<selectedBP>().setBlueprint(gameObject.GetComponent<knowBlueprint>().getBlueprint());

        PlayerPrototype player = GameObject.Find("Player1").GetComponent<PlayerPrototype>();
        float cost = GameObject.Find("BluePrint").GetComponent<Blueprint>().GetCost();
        if (tile != null && tile.canSustainPlant && !tile.getPlant())
        {
            if (cost >= 0 && player.GetPoints() >= cost)
            {
                tile.GrowPlant(player, tile,this.gameObject.GetComponent<knowBlueprint>().getBlueprint());
                player.AddPoints(-cost);
            }
            else
            {
                print("Nicht genug Energie! SPieler hat " + player.GetPoints() + " Punkte, BP kostet " + cost);
            }
        }


    }
}
