using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class createPlant : MonoBehaviour, IPointerClickHandler
{
    private Blueprint blueprint;
    private PlayerPrototype player;
    public Plant plant;

    public void OnPointerClick(PointerEventData eventData)
    {
        IsTile tile = GameObject.Find("selectHandler").GetComponent<selectedObject>().getTile().GetComponent<IsTile>();

        GameObject.Find("bpSelectHandlerPlant").GetComponent<selectedBP>().setBlueprintSelect(gameObject.GetComponent<knowBlueprint>().getBlueprint());


        //player = this.GetComponent<IsTile>().getPlayingField().players[0];
        player = GameObject.Find("Player1").GetComponent<PlayerPrototype>();
        blueprint = this.gameObject.GetComponent<knowBlueprint>().getBlueprint();
        float cost = blueprint.GetCost();
        if (tile != null && tile.canSustainPlant && !tile.getPlant())
        {
            if (cost >= 0 && player.GetPoints() >= cost)
            {
                tile.GrowPlant(player, plant, blueprint);
                player.AddPoints(-cost);
            }
            else
            {
                //print("Nicht genug Energie! Spieler" + player.myNum+ " hat "+ player.GetPoints()+ " Punkte, BP kostet "+ cost);
            }
        }


    }
}
