using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgrade : MonoBehaviour {


    private int value;
    private int maxValue;
    private int baseCost;
    private string nameID;

    /// <summary>
    /// Konstruktor des Stat.
    /// </summary>
    /// <param name="value">Level des Upgrades</param>
    /// <param name="maxValue">Maximaler Level des Upgrades</param>
    public BaseUpgrade(int maxValue, string name, int baseCost)
    {
        this.value = 0;
        this.maxValue = maxValue;
        this.baseCost = baseCost;
        this.nameID = name;
    }

    /// <summary>
    /// Getter für den momentanen Level des Upgrades.
    /// </summary>
    /// <returns>Momentaner Wert</returns>
    public int getCurrentValue()
    {
        return value;
    }

    /// <summary>
    /// Getter für den Maximallevel des Upgrades.
    /// </summary>
    /// <returns>Maximallevel</returns>
    public int getMaxValue()
    {
        return maxValue;
    }

    /// <summary>
    /// Getter für die BasisKosten des Upgrades.
    /// </summary>
    /// <returns>Maximallevel</returns>
    public int getBaseCost()
    {
        return baseCost;
    }

    /// <summary>
    /// Getter für die NamensID des Upgrades.
    /// </summary>
    /// <returns>NameID</returns>
    public string getName()
    {
        return nameID;
    }

    /// <summary>
    /// Operation zum anpassen des Levels des Upgrades.
    /// </summary>
    public void setLevel(int i)
    {
        if (i <= maxValue && i >= 0)
        {
            value = i;
        }
        else {
            Debug.Log("Limit von " + nameID + "nicht im gültigen Bereich: " + i + "nicht zwischen 0 und " + maxValue + ".");
        }
    }

    /// <summary>
    /// Operation zum erhöhen des Levels des Upgrades.
    /// </summary>
    public void incrementLevel()
    {
        if (value < maxValue)
        {
            value++;
        }
        else
        {
            Debug.Log("Inkrementierlimit von " + nameID +", Level" + value + ", erreicht.");
        }
    }

    /// <summary>
    /// Operation zum erhöhen des Levels des Upgrades.
    /// </summary>
    public void decrementLevel()
    {
        if (value > 0)
        {
            value--;
        }
        else
        {
            Debug.Log("Dekrementierlimit von " + nameID + ", Level" + value + ", erreicht.");
        }
    }
}
