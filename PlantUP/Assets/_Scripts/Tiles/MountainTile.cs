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


    public tileType getTileType()
    {
        return tileType.MOUNTAIN;
    }


    public int getWindSpread()
    {
        //BergFelder verringern die Windstärke

        return Mathf.RoundToInt((float)(getWindStrength() * mountainWindFactor));
    }


    public int getWindStrength()
    {
        if (windUpdate)
            updateWindStrength();
        return windstrength;
    }

    public int getNutrientValue()
    {
        return 0;
    }

    public int getWaterStrength()
    {
        return 0;
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
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPlayingField(PlayingFieldLogic playingField)
    {
        this.playingField = playingField;
    }

    public PlayingFieldLogic getPlayingField()
    {
        return playingField;
    }

    public Transform getTransform()
    {
        return transform;
    }

    public void replaceNeighbour(isTile oldTile, isTile newTile)
    {
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] == oldTile)
                neighbours[i] = newTile;
        }
    }

    public void removeObject()
    {
        Destroy(this.gameObject);
    }
}
