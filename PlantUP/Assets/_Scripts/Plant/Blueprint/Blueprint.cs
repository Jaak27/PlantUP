using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    public PlayerPrototype player;
    public List<int> upgradeSequence;
    public List<UpgradeType> typeSequence;
    public bool hasChanged = true;
    public int playerPlants = 0;
    public int plantsNoticed = 0;
    public float cost = 100;

    public int index;

    public Sprite s;

    private void Awake()
    {
        SetSequence();
    }



    public float GetCost()
    {
        if (upgradeSequence.Count >= 0)
        {
            cost = typeSequence.Count * 100;
        }

        return cost;
    }

    public void updateCost()
    {
        cost = typeSequence.Count * 200;
    }

    public float GetCostTypSequence()
    {
        int costn = 0;
        if (typeSequence.Count > 0)
        {
            costn = typeSequence.Count * 100;
        }

        return costn;
    }

    public List<int> GetSequence() {
        return upgradeSequence;
    }

    public List<UpgradeType> GetTypeSequence()
    {
        return typeSequence;
    }

    public void setTypeSequence(List<UpgradeType> t)
    {
        typeSequence = t;
    }

    public void SetSequence()
    {
        print("set Sequence");
        upgradeSequence = new List<int>();
        foreach (UpgradeType type in typeSequence)
        {
            print("TT");
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

    public void setSequence(List<int> newSeq)
    {
        upgradeSequence = newSeq;
    }

    public void setHasChanged(bool b)
    {
        print("setHasChanged");
        hasChanged = b;
    }

    public bool HasChanged() {
        //playerPlants = GameObject.Find("Player1").GetComponent<PlayerPrototype>().GetPlantCount();
        return hasChanged;
    }

    public void ChangeNoticed()
    {
        print("changed Noticed");
        plantsNoticed++;
        SetSequence();
        if (plantsNoticed == playerPlants)
        {
            hasChanged = false;
            plantsNoticed = 0;
        }
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

    public int getPlants()
    {
        return playerPlants;
    }

    public void dekrementPlants()
    {
        playerPlants--;
    }

    public void inkrementPlants()
    {
        playerPlants++;
    }

    public int getUpgradeCount()
    {
        int num = 0;
        foreach(UpgradeType type in typeSequence)
        {
            num++;
        }
        return num;
    }
}
