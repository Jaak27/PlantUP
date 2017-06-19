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

    public bool HasChanged() {
        return hasChanged;
    }

    public void ChangeNoticed() {
        hasChanged = false;
    }
}
