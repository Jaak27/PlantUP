using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    public PlayerPrototype player;
    private List<int> upgradeSequence;
    public List<UpgradeType> typeSequence;
    public bool hasChanged = true;
    public int playerPlants = 0;
    public int plantsNoticed = 0;
    private float cost = 100;

    private void Awake()
    {
        SetSequence();
    }

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

    private void SetSequence()
    {

        upgradeSequence = new List<int>();
        foreach (UpgradeType type in typeSequence)
        {
            switch (type)
            {
                case UpgradeType.HEIGHT:
                    upgradeSequence.Add(0);
                    break;
                case UpgradeType.LEAVES:
                    upgradeSequence.Add(1);
                    break;
                case UpgradeType.STALK:
                    upgradeSequence.Add(2);
                    break;
                case UpgradeType.PETAL:
                    upgradeSequence.Add(3);
                    break;
                case UpgradeType.REGENERATION:
                    upgradeSequence.Add(4);
                    break;
                case UpgradeType.INSECTS:
                    upgradeSequence.Add(5);
                    break;
                case UpgradeType.DEEPROOTS:
                    upgradeSequence.Add(6);
                    break;
                case UpgradeType.POROUSROOTS:
                    upgradeSequence.Add(7);
                    break;
                case UpgradeType.SPREADROOTS:
                    upgradeSequence.Add(8);
                    break;
                case UpgradeType.EFFICIENCY:
                    upgradeSequence.Add(9);
                    break;
            }
        }
    }

    public bool HasChanged() {
        playerPlants = player.GetPlantCount();
        return hasChanged;
    }

    public void ChangeNoticed() {
        plantsNoticed++;
        SetSequence();
        if (plantsNoticed == playerPlants)
        {
            hasChanged = false;
            plantsNoticed = 0;
        }
    }
}
