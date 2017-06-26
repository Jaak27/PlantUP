using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : _IsStat{

    private static readonly float baseValue = 100;
    private float maxValue = baseValue;
    private float currentValue = baseValue;


    public float GetBase()
    {
        return baseValue;
    }

    public float GetCurrent()
    {
        return currentValue;
    }

    public float GetMax()
    {
        return maxValue;
    }

    public void AddToCurrent(float value)
    {
        float temp = currentValue + value;
        if (temp <= 0)
        {
            currentValue = 0;
        }
        else if (temp >= maxValue)
        {
            currentValue = maxValue;
        }
        else
        {
            currentValue = temp;
        }
    }

    public void SetCurrent(float value)
    {
        if (value > 0)
        {
            currentValue = value;
        }
    }

    public void SetMax(float value)
    {
        if (value >= 1)
        {
            maxValue = value;
        }
    }

}
