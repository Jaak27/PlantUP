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
    bool Dekrement();
    void ResetUpgrade();

    string getInfo();

}

public class ProtoPlant : MonoBehaviour {

    public GroundTile myGroundTile;
    public ProtoBlueprint myBlueprint;
    private int count;

    private int index = 0;

    public float energy = 0;

    //Temporär um zu sehen ob Upgrades funktionieren
    public int hoehe;
    public int blaetter;
    public int staengel;
    public int bluete;
    public int regeneration;
    public int insekten;
    public int tiefeWurzeln;
    public int poroeseWuzeln;
    public int reichweiteWurzeln;
    public int effizienz;

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
            count = myBlueprint.GetSequence().Count;
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
        int upgradeID = myBlueprint.GetSequence()[i] - 1;
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
        staengel = upgrades[2].GetCurrent(); ;
        bluete = upgrades[3].GetCurrent();
        regeneration = upgrades[4].GetCurrent();
        insekten = upgrades[5].GetCurrent();
        tiefeWurzeln = upgrades[6].GetCurrent();
        poroeseWuzeln = upgrades[7].GetCurrent();
        reichweiteWurzeln = upgrades[8].GetCurrent();
        effizienz = upgrades[9].GetCurrent();


}

    public void SetGroundTile(GroundTile groundTile) {
        if (groundTile != null)
        {
            myGroundTile = groundTile;
        }
    }

    private void InitUpgrades() {

        Height u1 = new Height();
        Leaves u2 = new Leaves();
        Stalk u3 = new Stalk();
        Petals u4 = new Petals();
        Regenerate u5 = new Regenerate();
        Insects u6 = new Insects();
        DeepRoots u7 = new DeepRoots();
        PorousRoots u8 = new PorousRoots();
        SpreadRoots u9 = new SpreadRoots();
        Efficiency u10 = new Efficiency();

        upgrades = new List<isUpgrade> { u1, u2, u3, u4, u5, u6, u7, u8, u9, u10 };
    }
}
