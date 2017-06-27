using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class confirmButton : MonoBehaviour, IPointerClickHandler
{
    Blueprint test;

    //Blueprint Slot Create

    public GameObject bpSlotCreate;
    GameObject newBpSlotCreate;


    // BLueprint Slot Plant

    public GameObject bpSlotPlant;
    GameObject newBpSlotPlant;

    public PlayerPrototype player;



    //Parent Object
    public Transform parentCreate;
    public Transform parentPlant;
    public Transform parentChange;
    public Transform parentMain;

    //Menus
    public GameObject addUpgrades;
    public GameObject createBP;
    public GameObject main;

    public void OnPointerClick(PointerEventData eventData)
    {

        if(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().GetTypeSequence().Count > 0)
        {
            test = Instantiate(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint());


            newBpSlotCreate = Instantiate(bpSlotCreate, parentCreate);
            newBpSlotPlant = Instantiate(bpSlotPlant, parentPlant);

            //addedSlotGroup a = Instantiate(test.GetComponent<myAddedSlotGroup>().myGroup.GetComponent<addedSlotGroup>());

            //a.transform.parent = parentMain;

            test.GetComponent<myAddedSlotGroup>().myGroup.transform.parent = createBP.transform;
            test.GetComponent<myAddedSlotGroup>().myGroup.GetComponent<addedSlotGroup>().mybp = test;

            newBpSlotCreate.GetComponent<knowBlueprint>().setBlueprint(test);
            newBpSlotPlant.GetComponent<knowBlueprint>().setBlueprint(test);


            player.blueprints.Add(test);
            test.index = player.blueprints.Count - 1;

            BlueprintSprites spritesPlant = GameObject.Find("bluePrintPlantSprites").GetComponent<BlueprintSprites>();

            for (int i = 0; i < spritesPlant.spirtes.Count; i++)
            {
                if (spritesPlant.used[i] == false)
                {
                    test.s = spritesPlant.spirtes[i];
                    spritesPlant.used[i] = true;
                    break;
                }
            }

            BlueprintSprites spritesBP = GameObject.Find("bluePrintSprites").GetComponent<BlueprintSprites>();

            for (int i = 0; i < spritesBP.spirtes.Count; i++)
            {
                if (spritesBP.used[i] == false)
                {
                    newBpSlotCreate.GetComponent<Image>().sprite = spritesBP.spirtes[i];
                    newBpSlotPlant.GetComponent<Image>().sprite = spritesBP.spirtes[i];
                    spritesBP.used[i] = true;
                    break;
                }
            }

            Destroy(GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint().gameObject);

            addUpgrades.SetActive(false);
            main.SetActive(true);

        }



    }
}

internal class addUpgrades
{
}