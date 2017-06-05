using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainTile : MonoBehaviour, isTile
{
    
    /// <summary>
    /// Auf wieviel reduziert der Berg die Windstärke.
    /// Für die Balance.
    /// </summary>
    static public double mountainWindFactor = 0.5;

   

    PlayingFieldLogic playingField;

    isTile[] neighbours;

    int windstrength;
    bool windUpdate = true;
    

    public void forceWindUpdate()
    {
        windUpdate = true;
    }

    public int getLightValue()
    {
        return playingField.getLightStrength();
    }

    public isTile[] getNeighbours()
    {
        return neighbours;
    }

    public void setNeighbours(isTile[] neighbours)
    {
        this.neighbours = neighbours;
    }


    public string getTileType()
    {
        return "mountain";
    }


    public int getWindSpread()
    {
        //BergFelder verringern die Windstärke
        
        return Mathf.RoundToInt((float) (getWindStrength() * mountainWindFactor));
    }


    public int getWindStrength()
    {
        if (windUpdate)
            updateWindStrength();
        return windstrength;
    }


    public void updateWindStrength()
    {
        if (windUpdate)
        {
            //Findet das Feld aus dem der Wind auf dieses Feld weht.
            isTile tile = neighbours[playingField.getWindDirection()];
            if (tile != null)
            {
                windstrength = tile.getWindSpread();
            }
            else
            {
                windstrength = playingField.getWindStrength();
            }
            //Die Windstärke wurde geupdated.
            windUpdate = false;
        }
    }




    // Use this for initialization
    void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPlayingField(PlayingFieldLogic playingField)
    {
        this.playingField = playingField;
    }

    public PlayingFieldLogic getPlayingField()
    {
        return playingField;
    }
    
}
