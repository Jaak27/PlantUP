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



    // Use this for initialization
    void Start()
    {

        text_Feld = GameObject.Find("txt_FeldInfo").GetComponent<Text>();
        text_Pflanze = GameObject.Find("txt_PflanzenInfo").GetComponent<Text>();
        fenster_FeldInfo = GameObject.Find("fenster_FeldInfo").GetComponent<feldInfoUI>();
        fenster_PflanzenInfo = GameObject.Find("fenster_PflanzenInfo").GetComponent<feldInfoUI>();
        fenster_UpgradeInfo = GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>();

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
                    {
                        fenster_PflanzenInfo.setUp(true);
                        text_Pflanze.text = "-Planzendata-" +
                                         "\n" + "Health........"+
                                         "\n" + "Energy......." +
                                         "\n" + "Age......................" +
                                         "\n" + "N..........................." +
                                         "\n" + "Wa........................." +
                                         "\n" + "Wi.........................." +
                                         "\n" + "S............................" +
                                         "\n" + "Usage..................";
                    }
                }
                else
                {
                    fenster_PflanzenInfo.setUp(false);
                }

                /*
                //getTileType gibt ein Enum zurück, das durch die Cases gejagt wird.
                //Das macht es viel einfacher herauszufinden welche Daten angezeigt werden sollen
                switch (this.gameObject.GetComponent<IsTile>().getTileType())
                {
                    //Nährstoffe + Wind und Licht + Pflanze
                    case (tileType.ASH):
                    case (tileType.GROUND):
                        text_Feld.text = "-Feldata-" +
                                "\nNaehrstoffe.........." + this.gameObject.GetComponent<IsTile>().getNutrientValue() +
                                "\nWindstaerke.........." + this.gameObject.GetComponent<IsTile>().getWindStrength() +
                                "\nLichtintensitaet....." + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();

                        text_Pflanze.text = "-Planzendata-" +
                                     "\n" + "Health........100/100" +
                                     "\n" + "Energy.......100/100" +
                                     "\n" + "Age......................100" +
                                     "\n" + "N...........................100" +
                                     "\n" + "Wa.........................100" +
                                     "\n" + "Wi..........................100" +
                                     "\n" + "S............................100" +
                                     "\n" + "Usage..................100";
                        break;

                    //Wasserstärke + Wind und Licht 
                    case (tileType.WATER):

                        fenster_FeldInfo.setUp(true);
                        fenster_PflanzenInfo.setUp(false);
                        fenster_UpgradeInfo.setUp(false);
                        text_Feld.text = "-Feldata-" +
                                        "\nWasserwert..........." + this.gameObject.GetComponent<IsTile>().getWaterStrength() +
                                        "\nWindstaerke.........." + this.gameObject.GetComponent<IsTile>().getWindStrength() +
                                        "\nLichtintensitaet....." + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();

                        break;

                    //Wind und Licht
                    case (tileType.MOUNTAIN):
                    case (tileType.VOLCANO):
                    default:

                        fenster_FeldInfo.setUp(true);
                        fenster_PflanzenInfo.setUp(false);
                        fenster_UpgradeInfo.setUp(false);

                        text_Feld.text = "-Feldata-" +
                                "\nWindstaerke.........." + this.gameObject.GetComponent<IsTile>().getWindStrength() +
                                "\nLichtintensitaet....." + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
                        break;
                }
                */




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
