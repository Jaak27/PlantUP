using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour, isTile
{
    PlayingFieldLogic playingField;
    isTile[] neighbours;

    int windstrength;
    bool windUpdate = true;
    

    /// <summary>
    /// Um wieviel % ein Wasserfeld den Wind wiederauffrischt.
    /// Erhöht den Windwert um % der originalen Windstärke.
    /// </summary>
    static public float waterWindRefreshFactor = 0.10f;
    


    /// <summary>
    /// Die Energiemenge die das Wasser hergibt.
    /// </summary>
    int waterStrength;

    public static int minimumWaterStrength = 50;
    public static int maximumWaterStrength = 150;
    

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

    public string getTileType()
    {
        return "water";
    }

    
    public int getWindSpread()
    {
        int windSpread = getWindStrength() + Mathf.CeilToInt(getPlayingField().getWindStrength() * waterWindRefreshFactor);
        if (windSpread > getPlayingField().getWindStrength())
            windSpread = getPlayingField().getWindStrength();
        return windSpread;
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


    public void setNeighbours(isTile[] neighbours)
    {
        this.neighbours = neighbours;
    }

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void setWaterStrength(int waterStrength)
    {
        this.waterStrength = waterStrength;
    }

    public int getWaterStrength()
    {
        return waterStrength;
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
