using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbsorb : _IsStat {

    private static readonly float baseValue = 1;
    private float currentValue = baseValue;


    public float GetBase()
    {
        return baseValue;
    }

    public float GetCurrent()
    {
        return currentValue;
    }

    public void AddToCurrent(float value)
    {
        float temp = currentValue + value;
        if (temp <= 0)
        {
            currentValue = 0;
        }
        else
        {
            currentValue = temp;
        }
    }

    public void SetCurrent(float value)
    {
        if (value >= 0)
        {
            currentValue = value;
        }
    }

    public float GetMax()
    {
        throw new NotImplementedException();
    }

    public void SetMax(float value)
    {
        throw new NotImplementedException();
    }
}
