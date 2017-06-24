using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    public List<int> upgradeSequence;
    public bool hasChanged = true;
    private float cost = 100;

    public float GetCost()
    {
        if (upgradeSequence.Count > 0)
        {
            cost = upgradeSequence.Count * 100;
        }

        return cost;
    }

    public List<int> GetSequence() {
        return upgradeSequence;
    }

    public void setSequence(List<int> newSeq)
    {
        upgradeSequence = newSeq;
    }

    public void setHasChanged(bool b)
    {
        hasChanged = b;
    }

    public bool HasChanged() {
        return hasChanged;
    }

    public void ChangeNoticed() {
        hasChanged = false;
    }

    public override string ToString()
    {
        String test;
        test = "";
        for(int i = 0; i < upgradeSequence.Count; i++)
        {
            test = test + upgradeSequence[i];
        }
        return test;
    }
}
