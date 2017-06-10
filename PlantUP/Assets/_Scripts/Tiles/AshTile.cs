﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshTile : MonoBehaviour, isTile
{
    /// <summary>
    /// Eine Referenz auf das Spielfeld.
    /// </summary>
    PlayingFieldLogic playingField;



    /// <summary>
    /// Enthält die 6 nächsten Nachbarn.
    /// Von links oben, in Uhrzeigerrichtung
    /// </summary>
    isTile[] neighbours;

    /// <summary>
    /// Die Nährstoffe die noch auf diesem Feld lagern.
    /// </summary>
    int nutrientValue;

    public static int minimumNutrientValue = 900;
    public static int maximumNutrientValue = 1500;

    /// <summary>
    /// Die Windstärke die auf diesem Feld herrscht.
    /// </summary>
    int windstrength;


    /// <summary>
    /// Ist windUpdate = true, muss die Windstärke neu berechnet werden.
    /// </summary>
    bool windUpdate = true;


    /// <summary>
    /// Die Pflanze die auf diesem Feld wächst.
    /// Muss noch implementiert werden.
    /// </summary>
    int plant;




    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public tileType getTileType()
    {
        return tileType.ASH;
    }

    public isTile[] getNeighbours()
    {
        return neighbours;
    }

    public int getNutrientValue()
    {
        return nutrientValue;
    }

    public int getWaterStrength()
    {
        return 0;
    }

    public void addNutrientValue(int newValue)
    {
        nutrientValue += newValue;
        if (nutrientValue < 0)
            nutrientValue = 0;
    }


    /// <summary>
    /// Gibt die maximale Windenergie auf diesem Feld zurück.
    /// </summary>
    /// <returns>Windenergie</returns>
    public int getWindStrength()
    {
        if (windUpdate)
            updateWindStrength();
        return windstrength;
    }

    /// <summary>
    /// Gibt die Windstärke an, die von diesem Feld zurück geht.
    /// Ist kleiner bei großen Pflanzen auf Groundfeldern oder generell bei Bergfeldern.
    /// </summary>
    /// <returns>Weitergegebene Windstärke</returns>
    public int getWindSpread()
    {

        //Der Wind frischt ein bischen auf, bis auf den Maximalwert.
        int windSpread = getWindStrength() + Mathf.CeilToInt(getPlayingField().getWindStrength() * GroundTile.groundWindRefreshFactor);
        if (windSpread > getPlayingField().getWindStrength())
            windSpread = getPlayingField().getWindStrength();



        return windSpread;

    }

    public void setWindStrength(int newValue)
    {
        windstrength = newValue;
    }

    /// <summary>
    /// Erneuert die Windstärke auf diesem Feld nachdem sich der Wind verändert hat.
    /// Wird aufgerufen wenn die Windstärke abgefragt wird, und fragt den vorherigen Feld in Richtung des Windes nach 
    /// dessen Windstärke bzw. dessen weitergegebe Windstärke.
    /// 
    /// </summary>
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

    public void forceWindUpdate()
    {
        windUpdate = true;
    }

    public void setNutrientValue(int nutrientValue)
    {
        this.nutrientValue = nutrientValue;
    }


    public int getLightValue()
    {
        return playingField.getLightStrength();
    }

    public void setNeighbours(isTile[] neighbours)
    {
        this.neighbours = neighbours;
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