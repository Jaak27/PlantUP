using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Selectable : MonoBehaviour, IPointerClickHandler {

    public Text text_Feld; // Text welcher die Feldinfos darstellt
    public Text text_Pflanze;
    public Image fenster_FeldInfo;
    public Image fenster_PflanzenInfo;

    bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (selected == true)
        {
            // Welche Infos angezeigt werden sollen abhängig von den Tiles
            if(this.gameObject.GetComponent<GroundTile>() != null)
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

                text_Feld.text = "Felddaten" +
                            "\n" + gameObject.name +
                            "\nWasserwert: " + this.gameObject.GetComponent<WaterTile>().getWaterStrength() +
                            //"/n Windwert: " + this.gameObject.GetComponent<WaterTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
                            
            }

            if (this.gameObject.GetComponent<MountainTile>() != null)
            {

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
}
