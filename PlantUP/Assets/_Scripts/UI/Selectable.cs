using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Selectable : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

    Text text_Feld; // Text welcher die Feldinfos darstellt
    Text text_Pflanze;

    bool selected = false;

    bool overObject;

    GameObject objectOver;

	// Use this for initialization
	void Start () {

        text_Feld = GameObject.Find("txt_FeldInfo").GetComponent<Text>();
        text_Pflanze = GameObject.Find("txt_PflanzenInfo").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        if (selected == true)
        {
            // Welche Infos angezeigt werden sollen abhängig von den Tiles
            if(this.gameObject.GetComponent<GroundTile>() != null)
            {
                text_Feld.GetComponentInParent<feldInfoUI>().setUp(true);
             
                text_Feld.text = "Felddaten " +
                            "\n" + "Bodenwert: " + this.gameObject.GetComponent<GroundTile>().getNutrientValue() +
                            //"/n Windwert: " + this.gameObject.GetComponent<GroundTile>().getWindStrength() +
                            "\n" + "Sonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();

                text_Pflanze.GetComponentInParent<feldInfoUI>().setUp(true);

                text_Pflanze.text = "Pflanzendaten: (Falls vorhanden)" +
                                    "\n" + "Produktion: 100" +
                                     "\n" + "Verbrauch: 100" +
                                     "\n" + "Alter: 100" +
                                     "\n" + "Size: 111" +
                                     "\n" + "Health: 100";

                GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>().setUp(true);


            }

            if (this.gameObject.GetComponent<WaterTile>() != null)
            {
                text_Feld.GetComponentInParent<feldInfoUI>().setUp(true);

                text_Feld.text = "Felddaten" +
                            "\nWasserwert: " + this.gameObject.GetComponent<WaterTile>().getWaterStrength() +
                            //"/n Windwert: " + this.gameObject.GetComponent<WaterTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();

                text_Pflanze.GetComponentInParent<feldInfoUI>().setUp(false);

                GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>().setUp(false);

            }

            if (this.gameObject.GetComponent<MountainTile>() != null)
            {
                text_Feld.GetComponentInParent<feldInfoUI>().setUp(true);

                text_Feld.text = "Felddaten" +
                            //"\nWindwert: " + this.gameObject.GetComponent<MountainTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();

                text_Pflanze.GetComponentInParent<feldInfoUI>().setUp(false);

                GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>().setUp(false);

            }


            
        }

        if(Input.GetMouseButton(1)) // feld wird deselektiert
        {
            selected = false;
        }

        if (overObject)
        {

            if (Input.GetMouseButtonDown(0))
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

    public void setText_Feld(GameObject test)
    {
        text_Feld = test.GetComponent<Text>();
    }

    public void setText_Pflanze(GameObject test)
    {
        text_Pflanze = test.GetComponent<Text>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
            //setSelected(true);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        overObject = true;
        objectOver = eventData.pointerEnter;
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        overObject = false;
        objectOver = null;
    }
}
