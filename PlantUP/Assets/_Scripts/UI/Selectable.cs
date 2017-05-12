using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Selectable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    Text text_Feld; // Text welcher die Feldinfos darstellt
    Text text_Pflanze;
    feldInfoUI fenster_FeldInfo;
    feldInfoUI fenster_PflanzenInfo;
    feldInfoUI fenster_UpgradeInfo;

    bool mouseOverObject;


    bool selected = false;

	// Use this for initialization
	void Start () {

        text_Feld = GameObject.Find("txt_FeldInfo").GetComponent<Text>();
        text_Pflanze = GameObject.Find("txt_PflanzenInfo").GetComponent<Text>();
        fenster_FeldInfo = GameObject.Find("fenster_FeldInfo").GetComponent<feldInfoUI>();
        fenster_PflanzenInfo = GameObject.Find("fenster_PflanzenInfo").GetComponent<feldInfoUI>();
        fenster_UpgradeInfo = GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>();


    }
	
	// Update is called once per frame
	void Update () {

        if (selected == true)
        {

            fenster_FeldInfo.setUp(true);
            fenster_PflanzenInfo.setUp(true);
            fenster_UpgradeInfo.setUp(true);

            // Welche Infos angezeigt werden sollen abhängig von den Tiles
            if (this.gameObject.GetComponent<GroundTile>() != null)
            {

                text_Feld.text = "Felddaten " +
                            "\n" + gameObject.name +
                            "\n" + "Bodenwert: " + this.gameObject.GetComponent<GroundTile>().getNutrientValue() +
                            //"/n Windwert: " + this.gameObject.GetComponent<GroundTile>().getWindStrength() +
                            "\n" + "Sonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();

                text_Pflanze.text = "Pflanzendaten: (Falls vorhanden)" +
                                    "\n" + "Produktion: 100" +
                                     "\n" + "Verbrauch: 100" +
                                     "\n" + "Alter: 100" +
                                     "\n" + "Size: 111" +
                                     "\n" + "Health: 100";
          
                            
            }

            if (this.gameObject.GetComponent<WaterTile>() != null)
            {

                fenster_FeldInfo.setUp(true);
                fenster_PflanzenInfo.setUp(false);
                fenster_UpgradeInfo.setUp(false);

                text_Feld.text = "Felddaten" +
                            "\n" + gameObject.name +
                            "\nWasserwert: " + this.gameObject.GetComponent<WaterTile>().getWaterStrength() +
                            //"/n Windwert: " + this.gameObject.GetComponent<WaterTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
                            
            }

            if (this.gameObject.GetComponent<MountainTile>() != null)
            {

                fenster_FeldInfo.setUp(true);
                fenster_PflanzenInfo.setUp(false);
                fenster_UpgradeInfo.setUp(false);

                text_Feld.text = "Felddaten" +
                            "\n" + gameObject.name +
                            //"\nWindwert: " + this.gameObject.GetComponent<MountainTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
                            
            }


            
        }

        if(Input.GetMouseButton(1)) // feld wird deselektiert
        {
            selected = false;
        }

        if (mouseOverObject)
        {
            if(Input.GetMouseButtonDown(0))
            {
                selected = true;
            }
        }

    }


    // Methode um die jeweiligen Felder zu selektieren(deselektieren)
    
    public void setSelected(bool b)
    {
        selected = b;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.gameObject.GetComponent<GroundTile>() != null)
        {

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverObject = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverObject = false;
        selected = false;
    }
}
