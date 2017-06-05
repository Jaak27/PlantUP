using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Height : MonoBehaviour, isUpgrade {

    int cost = 10;
    int current = 0;
    int max = 3;

    public int GetCost()
    {
        return cost;
    }

    public int GetCurrent()
    {
        return current;
    }

    public int GetMax()
    {
        return max;
    }

    public bool Inkrement() {
        if (current < max)
        {
            current++;
            return true;
        }
        else {
            return false;
        }
    }

    public string getInfo() {
        return "Höhe Stufe " + current;
    }
    
}
