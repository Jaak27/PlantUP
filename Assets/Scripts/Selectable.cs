using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour {

    public Text text;

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
                text.text = "Felddaten" +
                            "\nBodenwert: " + this.gameObject.GetComponent<GroundTile>().getNutrientValue() +
                            //"/n Windwert: " + this.gameObject.GetComponent<GroundTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
            }

            if (this.gameObject.GetComponent<WaterTile>() != null)
            {
                text.text = "Felddaten" +
                            "\nWasserwert: " + this.gameObject.GetComponent<WaterTile>().getWaterStrength() +
                            //"/n Windwert: " + this.gameObject.GetComponent<WaterTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
            }

            if (this.gameObject.GetComponent<MountainTile>() != null)
            {
                text.text = "Felddaten" +
                            //"\nWindwert: " + this.gameObject.GetComponent<MountainTile>().getWindStrength() +
                            "\nSonnenwert: " + GameObject.Find("playingFieldTest").GetComponent<PlayingFieldLogic>().getLightStrength();
            }

        }

    }


    // Methoden um die jeweiligen Felder zu selektieren
    
    public void setSelected()
    {
        selected = true;
    }

    public void setUnselected()
    {
        selected = false;
    }
}
