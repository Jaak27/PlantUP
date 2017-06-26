﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPerSecond : _IsStat {

    private static readonly float baseValue = 0;
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
        
            currentValue = value;
        
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
