using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface isStat
{
    float GetCurrent();
    float GetBase();
    float GetMax();

    void AddToCurrent(float value);
    void SetCurrent(float value);
    void SetMax(float value);
}

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

public class Plant : MonoBehaviour {
    
    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    public GroundTile myGroundTile;
    private List<WaterTile> waterNeighbours;
    
    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    public Blueprint myBlueprint;
    
    /// <summary>
    /// Liste der Upgrades
    /// </summary>
    private List<isUpgrade> upgrades;

    /// <summary>
    /// Liste der Stats
    /// </summary>
    private List<isStat> stats;

    /// <summary>
    /// Count -> wieviele Stufen hat der Blueprint
    /// Index -> An welcher Stelle des Blueprint wir sind
    /// seqHasChanged -> Informiert uns wenn der Blueprint geändert wurde
    /// </summary>
    private int count;
    private int index;
    private List<int> boughtUpgrades;

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

    //Temporär um zu sehen ob Stats funktionieren
    public int energie;
    public int leben;
    public int alter;
    public int fov;
    public int hps;
    public int nAbsorb;
    public int sAbsorb;
    public int wiAbsorb;
    public int waAbsorb;
    public int eps;
    public int schadenErlitten;

    public void SetGroundTile(GroundTile myTile)
    {
        this.myGroundTile = myTile;
    }

    public GroundTile GetTile()
    {
        return myGroundTile;
    }

    public void SetBlueprint(Blueprint myBlueprint)
    {
        this.myBlueprint = myBlueprint;
    }
    
    public Blueprint GetBlueprint()
    {
        return myBlueprint;
    }

    private void Awake()
    {

        if (GameObject.Find("BluePrint").GetComponent<Blueprint>() == null)
        {
            print("Blueprint konnte nicht gefunden und zur Pflanze hinzugefügt werden!");
        }
        else
        {
            myBlueprint = GameObject.Find("BluePrint").GetComponent<Blueprint>();
            count = myBlueprint.GetSequence().Count;
        }
        InitPlant();
        SetWaterNeighbours();
        boughtUpgrades = new List<int>();
        InvokeRepeating("AdjustStats", 0, 0.1f);
    }

    private void InitPlant()
    {
        index = 0;

        InitUpgrades();
        InitStats();
    }

    private void Update()
    {
        // TODO: Nur zum Testen der Stats
        alter = (int)stats[0].GetCurrent();
        fov = (int)stats[1].GetCurrent();
        leben = (int)stats[2].GetCurrent();
        hps = (int)stats[3].GetCurrent();
        nAbsorb = (int)stats[4].GetCurrent();
        sAbsorb = (int)stats[5].GetCurrent();
        wiAbsorb = (int)stats[6].GetCurrent();
        waAbsorb = (int)stats[7].GetCurrent();
        energie = (int)stats[8].GetCurrent();
        schadenErlitten = (int)stats[9].GetCurrent();
        eps = (int)stats[10].GetCurrent();

        CheckSeqChange();
    }

    private void InitUpgrades()
    {

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

    private void InitStats()
    {
        Age s1 = new Age();
        FieldOfView s2 = new FieldOfView();
        Health s3 = new Health();
        HealthPerSecond s4 = new HealthPerSecond();
        NutrientAbsorb s5 = new NutrientAbsorb();
        SunAbsorb s6 = new SunAbsorb();
        WindAbsorb s7 = new WindAbsorb();
        WaterAbsorb s8 = new WaterAbsorb();
        Bank s9 = new Bank();
        DamageTaken s10 = new DamageTaken();
        EnergyPerSecond s11 = new EnergyPerSecond();

        stats = new List<isStat> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11 };
    }

    /// <summary>
    /// Wurde die Sequenz geändert?
    /// Wenn nicht sind wir am Ende?
    /// </summary>
    private void CheckSeqChange()
    {
        if (!myBlueprint.HasChanged())
        {
            if (HasNext())
            {
                CheckForNextUpgrade();
            }
        }
        else
        {
            myBlueprint.ChangeNoticed();
            ResetUpgrades();
            CheckForNextUpgrade();
        }
    }


    /// <summary>
    /// Wenn Kosten niedriger als Energievorhaben, inkrementiere Upgrade
    /// </summary>
    /// <param name="i">Nächst abzuarbeitendes Upgrade in der Sequenz</param>
    private void CheckForNextUpgrade()
    {
        int upgradeID = myBlueprint.GetSequence()[index] - 1;
        int cost = upgrades[upgradeID].GetCost() * (index + 1);
        float energy =
        schadenErlitten = (int)stats[8].GetCurrent();

        if (cost <= energy)
        {
            if (upgrades[upgradeID].Inkrement())
            {
                stats[8].SetCurrent(energy - cost);
                print("Upgrade " + upgrades[upgradeID].getInfo() + " für " + cost + " gekauft.");
                AdjustStats();
                boughtUpgrades.Add(upgradeID);

            }
            index++;
            PushChanges();
        }
        
    }

    void AdjustStats()
    {

        //Erstmal nur Nutrient Absorb
        if (myGroundTile != null)
        {
            CalcNutriValue();
            CalcWaterValue();
            CalcSunValue();
            CalcWindValue();
            //int sunValue = myGroundTile.getLightValue();
            //int windValue = myGroundTile.getWindStrength();
            
        }


        //Zum Testen der Upgrades
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

    private void CalcNutriValue()
    {
        int nutriValue = myGroundTile.getNutrientValue();
        float nps = stats[4].GetCurrent();
        float energy = stats[8].GetCurrent();

        if (nutriValue >= nps)
        {
            stats[8].SetCurrent(energy + nps);
            myGroundTile.setNutrientValue(nutriValue - (int)nps);
        }
        else
        {
            stats[8].SetCurrent(energy + nutriValue);
            myGroundTile.setNutrientValue(0);
            print("Groundtile empty");
        }
    }

    private void CalcWaterValue()
    {
        float wps = stats[7].GetCurrent();

        if (myGroundTile.getNeighbours() != null)
        {
            foreach (WaterTile wTile in waterNeighbours)
            {

                float energy = stats[8].GetCurrent();
                int waterValue = wTile.getWaterStrength();

                if (waterValue >= wps)
                {
                    stats[8].SetCurrent(energy + wps);
                    wTile.setWaterStrength(waterValue - (int)wps);
                }
                else if (waterValue > 0)
                {
                    stats[8].SetCurrent(energy + waterValue);
                    wTile.setWaterStrength(0);
                    print("Watertile empty");
                }

            }
        }
        else
        {
            print("Nachbarfelder nicht gesetzt!");
        }

    }

    private void CalcWindValue()
    {
        //TODO
    }

    private void CalcSunValue()
    {
        int sunValue = myGroundTile.getLightValue();
        float lps = stats[5].GetCurrent();
        float energy = stats[8].GetCurrent();

        if (sunValue == 0) { print("No SUN T_T"); }
        if (sunValue >= lps)
        {
            stats[8].SetCurrent(energy + lps);
        }
        else
        {
            stats[8].SetCurrent(energy + sunValue);
            //myGroundTile.setNutrientValue(0);
        }
    }


    private void SetWaterNeighbours() {
        if (myGroundTile.getNeighbours() != null)
        {
            foreach (isTile tile in myGroundTile.getNeighbours())
            {
                if (tile != null && tile.getTileType() == tileType.GROUND)
                {
                    waterNeighbours.Add((WaterTile)tile);
                }
            }
        }
        else
        {
            print("Nachbarfelder nicht gesetzt!");
        }

    }


    /// <summary>
    /// Macht alle Upgrades rückgängig und gibt Energie zurück
    /// TODO: NEU SCHREIBEN!
    /// </summary>
    private void ResetUpgrades()
    {
        int help = 1;
        print("Verkaufe " + boughtUpgrades.Count + " Upgrades!");
        foreach (int i in boughtUpgrades)
        {
            if (upgrades[i].Dekrement())
            {
                int cost = upgrades[i].GetCost() * help;
                stats[8].SetCurrent(stats[8].GetCurrent() + cost);
                print("Upgrade " + i + " für " + cost + " verkauft.");
                help++;
            }
        }
        index = 0;
        count = myBlueprint.GetSequence().Count;
    }

    private bool HasNext()
    {
        if (index < count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gibt den momentanen Wert für ein bestimmtes Upgrade an.
    /// </summary>
    /// <param name="i">UpgradeID</param>
    /// <returns>Momentaner Wert des Upgrades</returns>
    private int GetCurrentValueForUpgrade(int i) {
        return upgrades[i].GetCurrent();
    }

    /// <summary>
    /// Passt die Stats neu an.
    /// </summary>
    public void PushChanges() {
        PushMaxHealth();
        PushFoV();
        PushMaxAge();
        PushDamageTaken();
        PushHps();
        PushNutrientAbsorb();
        PushBankCapacity();
        PushEps();
        AdjustStats();
    }
    /// <summary>
    /// MaxHealth abhängig von height.
    /// </summary>
    private void PushMaxHealth() {
        float height = GetCurrentValueForUpgrade(0);
        stats[2].SetMax( (int)System.Math.Pow(2, height+1) * stats[2].GetBase());
    }
    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void PushFoV() {
        float height = GetCurrentValueForUpgrade(0);
        stats[1].SetCurrent(stats[1].GetBase() + height);
    }
    //TODO: Define params
    private void PushMaxAge() {
        // example
        for(int n = 0; n > myBlueprint.GetSequence().Count; n++) {
            stats[0].SetMax(stats[0].GetMax() + GetCurrentValueForUpgrade(n)+1);
        }
    }
    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%.
    /// </summary>
    private void PushDamageTaken() {
        float efficiency = GetCurrentValueForUpgrade(9);
        float thickStalk = GetCurrentValueForUpgrade(2);

        stats[9].SetCurrent(stats[9].GetBase() + (thickStalk * 0.1f) - (efficiency * 0.1f));
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void PushHps() {
        float regen = GetCurrentValueForUpgrade(4);
        float insects = GetCurrentValueForUpgrade(5);
        float maxHealth = stats[2].GetMax();

        stats[3].SetCurrent((maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * stats[9].GetCurrent()));
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%.
    /// </summary>
    private void PushNutrientAbsorb() {
        float thickStalk = GetCurrentValueForUpgrade(2);
        float porousRoots = GetCurrentValueForUpgrade(7);

        stats[4].SetCurrent(stats[4].GetBase() + (porousRoots) - (thickStalk));
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void PushBankCapacity() {
        float thickStalk = GetCurrentValueForUpgrade(2);
        float height = GetCurrentValueForUpgrade(0);

        stats[8].SetMax(stats[8].GetBase() + ((height+1) * 100) + (thickStalk * 25));
    }
    /// <summary>
    /// Energieausgabe abhängig von height, regen, deepRoots, bigLeaves, largePetals und efficiency.
    /// </summary>
    private void PushEps() {
        float height = GetCurrentValueForUpgrade(0);
        float regen = GetCurrentValueForUpgrade(4);
        float efficiency = GetCurrentValueForUpgrade(9);
        float deepRoots = GetCurrentValueForUpgrade(6);
        float bigLeaves = GetCurrentValueForUpgrade(1);
        float largePetals = GetCurrentValueForUpgrade(3);

        stats[10].SetCurrent(
            stats[8].GetCurrent()
            * (height + regen + deepRoots + bigLeaves + largePetals - 2 * efficiency)
            * 0.01f
            );
    }
    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void PushSunAbsorb() {
        float largePetals = GetCurrentValueForUpgrade(3);

        stats[5].SetCurrent(stats[4].GetCurrent() + largePetals * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void PushWindAbsorb()
    {
        float bigLeaves = GetCurrentValueForUpgrade(1);

        stats[6].SetCurrent(stats[4].GetCurrent() + bigLeaves * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void PushWaterAbsorb()
    {
        float deepRoots = GetCurrentValueForUpgrade(6);

        stats[7].SetCurrent(stats[4].GetCurrent() + deepRoots * 0.1f);
    }

    
}
