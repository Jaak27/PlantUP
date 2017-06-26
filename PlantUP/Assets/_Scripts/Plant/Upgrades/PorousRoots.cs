﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorousRoots : _IsUpgrade
{

    static readonly int cost = 1000;
    int current = 0;
    static readonly int max = 2;
    static readonly UpgradeType upgradeType = UpgradeType.POROUSROOTS;

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

    public bool Inkrement()
    {
        if (current < max)
        {
            current++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Dekrement()
    {
        if (current > 0)
        {
            current--;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetUpgrade()
    {
        current = 0;
    }

    public UpgradeType GetUpgradeType()
    {
        return upgradeType;
    }

    public string GetInfo()
    {
        return "Poröse Wurzeln Stufe " + current;
    }
}
