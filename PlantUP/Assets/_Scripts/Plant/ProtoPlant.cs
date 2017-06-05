using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface isUpgrade
{
    int GetCost();
    int GetCurrent();
    int GetMax();

    bool Inkrement();

    string getInfo();

}

public class ProtoPlant : MonoBehaviour {

    public GroundTile myGroundTile;
    public ProtoBlueprint myBlueprint;
    private int count;

    private int index = 0;

    public int height = 1;
    private int maxHeight = 3;

    public int leaves = 1;
    private int maxLeaves = 1;

    public float energy = 0;
    public int hoehe;
    public int blaetter;

    private int energyPerSecond = 10;

    private List<isUpgrade> upgrades;
    

    private void Awake()
    {
        
        if (GameObject.Find("BluePrint").GetComponent<ProtoBlueprint>() == null)
        {
            print("Blueprint konnte nicht gefunden und zur Pflanze hinzugefügt werden!");
        }
        else
        {
            myBlueprint = GameObject.Find("BluePrint").GetComponent<ProtoBlueprint>();
            count = myBlueprint.getSequence().Count;
        }
        InitUpgrades();
        InvokeRepeating("AdjustStats", 0, 0.1f);
    }
    

	// Update is called once per frame
	void Update () {
        if (index < count) {
            CheckForNextUpgrade(index);
        }
	}
    
    /// <summary>
    /// Wenn Kosten niedriger als Energievorhaben, inkrementiere Upgrade
    /// </summary>
    /// <param name="i">Nächst abzuarbeitendes Upgrade in der Sequenz</param>
    private void CheckForNextUpgrade(int i)
    {
        int upgradeID = myBlueprint.getSequence()[i] - 1;
        isUpgrade upgrade = upgrades[upgradeID];
        int cost = upgrade.GetCost() * (i + 1);

        if (cost < energy)
        {
            if (upgrade.Inkrement())
            {
                energy -= cost;
                print("Upgrade " + upgrade.getInfo() + " für " + cost + " gekauft.");
                AdjustStats();
                index++;
            }
        }
    }

    void AdjustStats() {

        if (myGroundTile != null)
        {
            int value = myGroundTile.getNutrientValue();
            if (value >= energyPerSecond)
            {
                energy += energyPerSecond;
                myGroundTile.setNutrientValue(value - energyPerSecond);
            }
            else
            {
                energy += value;
                myGroundTile.setNutrientValue(0);
                CancelInvoke();
                print("Groundtile empty");
            }
        }
        hoehe = upgrades[0].GetCurrent();
        blaetter = upgrades[1].GetCurrent();


    }

    public void SetGroundTile(GroundTile groundTile) {
        if (groundTile != null)
        {
            myGroundTile = groundTile;
        }
    }

    private void InitUpgrades() {
        Height h = new Height();
        Leaves l = new Leaves();
        upgrades = new List<isUpgrade> { h, l };
    }
}
