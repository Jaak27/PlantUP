using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerStat : BaseStat {

    float maxValue;

    public ContainerStat(float baseValue, float currentValue, float maxValue) : base(baseValue, currentValue)
    {
        this.maxValue = maxValue;
    }

    /// <summary>
    /// Setter für Maximalen Wert.
    /// </summary>
    /// <param name="i">Maximaler Wert</param>
    public void setMaxValue(float i)
    {
        if (i > 0)
        {
            maxValue = i;
        }
        else
        {
            Debug.Log("MaxValue darf nicht negativ sein.");
        }
        
    }

    /// <summary>
    /// Getter für maximalen Wert.
    /// </summary>
    /// <returns>Maximalen Wert</returns>
    public float getMaxValue()
    {
        return maxValue;
    }

}
