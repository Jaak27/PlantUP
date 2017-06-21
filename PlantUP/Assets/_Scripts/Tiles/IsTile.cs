using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTile : MonoBehaviour
{
    /// <summary>
    /// Eine Referenz auf das Spielfeld.
    /// </summary>
    PlayingFieldLogic playingField;


    /// <summary>
    /// Welcher Typ in diesem Feld dargestellt ist.
    /// </summary>

    public tileType type;


    /// <summary>
    /// Enthält die 6 nächsten Nachbarn.
    /// Von links oben, in Uhrzeigerrichtung
    /// </summary>
    public IsTile[] neighbours;

    /// <summary>
    /// Die Nährstoffe die noch auf diesem Feld lagern.
    /// Die Stärke des Wassers, sowie die Windstärke.
    /// </summary>
    int nutrientValue;
    int waterStrength;
    int windstrength;
    

    /// <summary>
    /// Ist windUpdate = true, muss die Windstärke neu berechnet werden.
    /// windloss = wieviel Prozent der Wind verliert oder gewinnt wenn er über dieses Feld fliegt.
    /// </summary>
    bool windUpdate = true;
    public float windloss;


    /// <summary>
    /// CanSustainPlant = ob eine Pflanze auf diesem Feld wachsen kann
    /// hasGroundValue = ob Nährstoffe aus dem Boden extrahiert werden können
    /// hasWaterValue = ob Energie aus Wasser extrahiert werden kann
    /// </summary>

    public bool canSustainPlant;
    public bool hasGroundValue;
    public bool hasWaterValue;


    Plant plant = null;


    bool getMoving = false;
    Vector3 target;
    float distanceLeft;
    Vector3 movementDirection;


    // Update is called once per frame
    void Update()
    {
        if (getMoving)
        {
            if (movementDirection == null || movementDirection == Vector3.zero)
            {
                movementDirection = target - this.transform.position;
                movementDirection.Normalize();
                movementDirection = movementDirection * playingField.getSpeed();
            }

            this.transform.Translate(movementDirection);

            distanceLeft = Vector3.Distance(target, this.transform.position);
            if (Math.Abs(distanceLeft) < playingField.getSpeed() / 2) 
            {
                this.transform.position = target;
                getMoving = false;
            }

        }


    }

    // Use this for initialization
    void Start()
    {
        

    }

    public Plant getPlant()
    {
        return plant;
    }

    public void setPlant( Plant newPlant)
    {
        plant = newPlant;
        plant.setTile(this); 
    }


    

    public Transform getTransform()
    {
        return transform;
    }


    public tileType getTileType()
    {
        return type;
    }

    public IsTile[] getNeighbours()
    {
        return neighbours;
    }

    public int getNutrientValue()
    {
        return getNutrientValue(false);
    }

    public int getNutrientValue(bool ignoreHasNutrientValue)
    {
        if (hasGroundValue || ignoreHasNutrientValue)

            return nutrientValue;
        else
            return 0;
    }

    public int getWaterStrength()
    {
        return getWaterStrength(false);
    }
    

    public int getWaterStrength(bool ignoreHasWaterValue)
    {
        if (hasWaterValue || ignoreHasWaterValue)

            return waterStrength;
        else
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

        //Ist windloss < 0 wird die Windstärke direkt multipliziert, für schneller Abfall
        int windSpread;
        if (windloss < 1)
            windSpread = Mathf.CeilToInt(getWindStrength() * windloss);
        else
            windSpread = getWindStrength() + Mathf.CeilToInt(getPlayingField().getWindStrength() * windloss);
        if (windSpread > getPlayingField().getWindStrength())
            windSpread = getPlayingField().getWindStrength();



        return windSpread;
        
    }

    public void setWaterStrength(int waterStrength)
    {
        this.waterStrength = waterStrength;
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
            IsTile tile = neighbours[playingField.getWindDirection()];
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
        if (this.nutrientValue < 0)
            this.nutrientValue = 0;
    }
    

    public int getLightValue()
    {
        return playingField.getLightStrength();
    }

    public void setNeighbours(IsTile[] neighbours)
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

    public void replaceNeighbour(IsTile oldTile, IsTile newTile)
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

    public bool getCanSustainPlant()
    {
        return canSustainPlant;
    }

    public bool getHasGroundValue()
    {
        return hasGroundValue;
    }
    public bool getHasWaterValue()
    {
        return hasWaterValue;
    }


    public bool hasReachedTarget()
    {
        return !getMoving;
    }
    public void setTarget(Vector3 target)
    {
        this.target = target;
    }
    
    public void setMoving()
    {
        if (target != null)
        getMoving = true;
    }
}
