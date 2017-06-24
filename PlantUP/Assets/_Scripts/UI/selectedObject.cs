using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class selectedObject : MonoBehaviour
{

    GameObject tileSelect;

    Text text_Feld; // Text welcher die Feldinfos darstellt
    Text text_Pflanze;
    feldInfoUI fenster_FeldInfo;
    feldInfoUI fenster_PflanzenInfo;
    feldInfoUI fenster_UpgradeInfo;

    public GameObject plantText;
    public GameObject createPlantPanel;



    // Use this for initialization
    void Start()
    {

        text_Feld = GameObject.Find("txt_FeldInfo").GetComponent<Text>();
        text_Pflanze = GameObject.Find("txt_PflanzenInfo").GetComponent<Text>();
        fenster_FeldInfo = GameObject.Find("fenster_FeldInfo").GetComponent<feldInfoUI>();
        fenster_PflanzenInfo = GameObject.Find("fenster_PflanzenInfo").GetComponent<feldInfoUI>();
        //fenster_UpgradeInfo = GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (tileSelect != null)
        {
            GameObject.Find("feldSelect").transform.position = new Vector3(tileSelect.transform.position.x, tileSelect.transform.position.y, 1);
            GameObject.Find("feldSelect").GetComponent<SpriteRenderer>().enabled = true;

            fenster_FeldInfo.setUp(true);
            // Welche Infos angezeigt werden sollen abhängig von den Tiles

            if (tileSelect.GetComponent<IsTile>() != null)
            {
                IsTile tile = tileSelect.GetComponent<IsTile>();



                //Jedes Feld hat 3 booleans die Aussagen ob es 1. Naerstoffe hat, 2. Wasserkraft hat, und 3. Eine Pflanze haben kann
                text_Feld.text = "-Feldata-";
                if (tile.getHasGroundValue())
                    text_Feld.text += "\nNaehrstoffe........" + tile.getNutrientValue();
                if (tile.getHasWaterValue())
                    text_Feld.text += "\nWasserwert........." + tile.getWaterStrength();
                //Jedes Feld hat Licht und Wind
                text_Feld.text += "\nWindstaerke........" + tile.getWindStrength() +
                                      "\nLichtintensitaet..." + tile.getLightValue();

                if (tile.getCanSustainPlant())
                {
                    fenster_PflanzenInfo.setUp(true);

                    if(tile.getPlant() != null)
                    {

                        createPlantPanel.SetActive(false);
                        plantText.SetActive(true);
                        text_Pflanze.text = "-Planzendata-" +
                                         "\n" + "Health........" + tile.getPlant().GetStats()[2].GetCurrent() + 
                                         "\n" + "Energy......." + tile.getPlant().GetStats()[8].GetCurrent() +
                                         "\n" + "Age......................" + tile.getPlant().GetStats()[7].GetCurrent() +
                                         "\n" + "N..........................." + tile.getPlant().GetStats()[4].GetCurrent() +
                                         "\n" + "Wa........................." + tile.getPlant().GetStats()[7].GetCurrent() +
                                         "\n" + "Wi.........................." + tile.getPlant().GetStats()[6].GetCurrent() +
                                         "\n" + "S............................" + tile.getPlant().GetStats()[5].GetCurrent() +
                                         "\n" + "Usage.................." +  tile.getPlant().GetStats()[10].GetCurrent();
         
                    }
                    else
                    {
                        createPlantPanel.SetActive(true);
                        plantText.SetActive(false);
                    }

                }
                else
                {
                    fenster_PflanzenInfo.setUp(false);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
 
                }
            }
            else
            {
                fenster_FeldInfo.setUp(false);
                fenster_PflanzenInfo.setUp(false);
            }
        }
    }

    public GameObject getTile()
    {
        return tileSelect;
    }

    public void setTile(GameObject test)
    {
        tileSelect = test;
    }
}