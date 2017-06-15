using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour {
    
    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    private List<IsTile> waterNeighbours;
    public IsTile myTile;
    
    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    public Blueprint myBlueprint;

    /// <summary>
    /// Eine Referenz auf den Besitzer der Pflanze.
    /// </summary>
    public PlayerPrototype player;
    
    /// <summary>
    /// Liste der Upgrades
    /// </summary>
    private List<IsUpgrade> upgrades;

    /// <summary>
    /// Liste der Stats
    /// </summary>
    private List<IsStat> stats;

    /// <summary>
    /// Count -> wieviele Stufen hat der Blueprint
    /// Index -> An welcher Stelle des Blueprint wir sind
    /// Ticks -> Zeitintervall in dem AjustStats() aufgerufen wird
    /// BoughtUpgrades -> Liste der bereits gekauften Upgrades, für wenn wir den BP ändern
    /// </summary>
    private int blueprintCount;
    private int index;
    private float blueprintCost;
    private static int plantCount;
    private int myNum;
    private readonly float ticks = 1f;
    private List<int> boughtUpgrades;

    //Temporär um zu sehen ob Upgrades funktionieren
    public int height;
    public int leaves;
    public int thickStalk;
    public int petals;
    public int regen;
    public int insects;
    public int deepRoots;
    public int porousRoots;
    public int spreadRoots;
    public int efficiency;

    //Temporär um zu sehen ob Stats funktionieren
    public float energie;
    public float leben;
    public float alter;
    public float fov;
    public float hps;
    public float nAbsorb;
    public float sAbsorb;
    public float wiAbsorb;
    public float waAbsorb;
    public float eps;
    public float schadenErlitten;

    public void SetMyTile(IsTile myTile)
    {
        this.myTile = myTile;
    }

    public IsTile GetMyTile()
    {
        return myTile;
    }

    public void SetBlueprint(Blueprint myBlueprint)
    {
        this.myBlueprint = myBlueprint;
    }
    
    public Blueprint GetBlueprint()
    {
        return myBlueprint;
    }


    public void SetPlayer(PlayerPrototype player)
    {
        this.player = player;
    }

    public PlayerPrototype GetPlayer()
    {
        return player;
    }

    public List<IsStat> GetStats()
    {
        return stats;
    }

    public static int getPlantCount()
    {
        return plantCount;
    }

    private void Awake()
    {
        myNum = ++plantCount;

        blueprintCount = myBlueprint.GetSequence().Count;
        
        
        SetWaterNeighbours();
        boughtUpgrades = new List<int>();
        blueprintCost = myBlueprint.GetCost();
        InitPlant();
        InvokeRepeating("AdjustStats", 0, ticks);
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
        alter = stats[0].GetCurrent();
        fov = stats[1].GetCurrent();
        leben = stats[2].GetCurrent();
        hps = stats[3].GetCurrent();
        nAbsorb = stats[4].GetCurrent();
        sAbsorb = stats[5].GetCurrent();
        wiAbsorb = stats[6].GetCurrent();
        waAbsorb = stats[7].GetCurrent();
        energie = stats[8].GetCurrent();
        schadenErlitten = stats[9].GetCurrent();
        eps = stats[10].GetCurrent();

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

        upgrades = new List<IsUpgrade> { u1, u2, u3, u4, u5, u6, u7, u8, u9, u10 };
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

        stats = new List<IsStat> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11 };
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
            //CheckForNextUpgrade();
            
        }
    }


    /// <summary>
    /// Wenn Kosten niedriger als Energievorhaben, inkrementiere Upgrade
    /// </summary>
    /// <param name="i">Nächst abzuarbeitendes Upgrade in der Sequenz</param>
    private void CheckForNextUpgrade()
    {
        int upgradeID = myBlueprint.GetSequence()[index];
        int cost = upgrades[upgradeID].GetCost() * (index + 1);
        
        float energy = stats[8].GetCurrent();

        if (cost <= energy)
        {
            if (upgrades[upgradeID].Inkrement())
            {
                stats[8].SetCurrent(energy - cost);
                print("Spieler"+player.GetPlayerNum()+"'s Pflanze Nr." + myNum + " hat Upgrade " + upgrades[upgradeID].getInfo() + " für " + cost + " gekauft.");
                boughtUpgrades.Add(upgradeID);

            }
            index++;
            PushChanges();
        }

    }

    void AdjustStats()
    {

        //Erstmal nur Nutrient Absorb
        if (myTile != null)
        {
            CalcNutriValue();
            CalcWaterValue();
            CalcSunValue();
            CalcWindValue();
            CalcEnergy();
            CalcHealth();

        }


        //Zum Testen der Upgrades
        height = upgrades[0].GetCurrent();
        leaves = upgrades[1].GetCurrent();
        thickStalk = upgrades[2].GetCurrent(); ;
        petals = upgrades[3].GetCurrent();
        regen = upgrades[4].GetCurrent();
        insects = upgrades[5].GetCurrent();
        deepRoots = upgrades[6].GetCurrent();
        porousRoots = upgrades[7].GetCurrent();
        spreadRoots = upgrades[8].GetCurrent();
        efficiency = upgrades[9].GetCurrent();
    }

    private void CalcNutriValue()
    {
        int nutriValue = myTile.getNutrientValue();
        float nups = stats[4].GetCurrent();
        float energy = stats[8].GetCurrent();

        if (nutriValue >= nups)
        {
            stats[8].SetCurrent(energy + nups);
            player.AddPoints(nups);
            //TODO anpassen für asche und sumpf feder!
            myTile.setNutrientValue(nutriValue - (int)nups);
        }
        else if(nutriValue > 0)
        {
            stats[8].SetCurrent(energy + nutriValue);
            player.AddPoints(nutriValue);
            //TODO anpassen für asche und sumpf feder!
            myTile.setNutrientValue(0);
            print("Groundtile empty");
        }
    }

    private void CalcWaterValue()
    {

        if (waterNeighbours.Count > 0)
        {
            float waps = stats[7].GetCurrent();
            foreach (IsTile wTile in waterNeighbours)
            {
                
                float energy = stats[8].GetCurrent();
                int waterValue = wTile.getWaterStrength();

                if (waterValue >= waps)
                {
                    stats[8].SetCurrent(energy + waps);
                    player.AddPoints(waps);
                    wTile.setWaterStrength(waterValue - (int)waps);
                }
                else if (waterValue > 0)
                {
                    stats[8].SetCurrent(energy + waterValue);
                    player.AddPoints(waterValue);
                    wTile.setWaterStrength(0);
                    print("Watertile empty");
                }

            }
        }

    }

    private void CalcWindValue()
    {
        int windValue = myTile.getWindStrength();
        float wips = stats[6].GetCurrent();
        float energy = stats[8].GetCurrent();
        
        if (windValue >= wips)
        {
            stats[8].SetCurrent(energy + wips);
            player.AddPoints(wips);
        }
        else
        {
            stats[8].SetCurrent(energy + windValue);
            player.AddPoints(windValue);
        }
    }

    private void CalcSunValue()
    {
        int sunValue = myTile.getLightValue();
        float sups = stats[5].GetCurrent();
        float energy = stats[8].GetCurrent();
        
        if (sunValue >= sups)
        {
            stats[8].SetCurrent(energy + sups);
            player.AddPoints(sups);
        }
        else
        {
            stats[8].SetCurrent(energy + sunValue);
            player.AddPoints(sunValue);
        }
    }

    private void CalcEnergy()
    {
        float eps = stats[10].GetCurrent();
        
        float energy = stats[8].GetCurrent();
        float health = stats[2].GetCurrent();
        float result = energy + eps;

        if (result >= 0)
        {
            stats[8].SetCurrent(result);
        }
        else
        {
            stats[2].SetCurrent(health+result);
        }
        
    }

    private void CalcHealth()
    {
        float hps = stats[3].GetCurrent();
        float dps = stats[9].GetCurrent();
        float health = stats[2].GetCurrent();
        float newHealth = health + hps - dps;

        if (newHealth <= stats[2].GetMax())
        {
            if (newHealth > 0)
            {
                stats[2].SetCurrent(newHealth);
            }
            else
            {
                print("Pflanze gestorben");
                Destroy(gameObject);
            }
        }


        stats[2].SetCurrent(health + hps - dps);
    }

    //TODO: Was passiert wenn Events Felder Wechseln?
    private void SetWaterNeighbours() {

        waterNeighbours = new List<IsTile>();
        IsTile[] neighbours = null;

        if (myTile != null)
        {
            neighbours = myTile.getNeighbours();
        }
        
        if (neighbours.Length > 0)
        {
            for (int i =0; i< 6; i++)
            {
                IsTile tile = neighbours[i];
                if (tile != null && (tile.getTileType() == tileType.WATER || tile.getTileType() == tileType.SWAMP))
                {
                    waterNeighbours.Add(tile);
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
        int bought = boughtUpgrades.Count;
        if (bought > 0)
        {
            print("Pflanze Nr."+myNum+" Verkauft " + boughtUpgrades.Count + " Upgrades!");
        }
        foreach (int i in boughtUpgrades)
        {
            if (upgrades[i].Dekrement())
            {
                int cost = upgrades[i].GetCost() * help;
                stats[8].SetCurrent(stats[8].GetCurrent() + cost);
                print("Pflanze Nr." + myNum + " hat ein Upgrade " + (i+1) + " für " + cost + " verkauft.");
                help++;
            }
        }
        boughtUpgrades = new List<int>();
        index = 0;
        blueprintCount = myBlueprint.GetSequence().Count;
    }

    private bool HasNext()
    {
        if (index < blueprintCount)
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
        /*height = upgrades[0].GetCurrent();
        leaves = upgrades[1].GetCurrent();
        thickStalk = upgrades[2].GetCurrent(); ;
        petals = upgrades[3].GetCurrent();
        regen = upgrades[4].GetCurrent();
        insects = upgrades[5].GetCurrent();
        deepRoots = upgrades[6].GetCurrent();
        porousRoots = upgrades[7].GetCurrent();
        spreadRoots = upgrades[8].GetCurrent();
        efficiency = upgrades[9].GetCurrent();
        */

        PushMaxHealth();
        PushFoV();
        PushMaxAge();
        PushDamageTaken();
        PushHps();
        PushNutrientAbsorb();
        PushBankCapacity();
        PushEps();
    }

    /// <summary>
    /// MaxHealth abhängig von height.
    /// </summary>
    private void PushMaxHealth() {
        stats[2].SetMax( (int)System.Math.Pow(2, height+1) * stats[2].GetBase());
    }
    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void PushFoV() {
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
        stats[9].SetCurrent((efficiency - thickStalk) * 0.1f);
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void PushHps() {
        float maxHealth = stats[2].GetMax();

        stats[3].SetCurrent((maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * stats[9].GetCurrent()));
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%.
    /// </summary>
    private void PushNutrientAbsorb() {

        stats[4].SetCurrent(stats[4].GetBase() + (porousRoots) - (thickStalk));
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void PushBankCapacity() {

        stats[8].SetMax(stats[8].GetBase() + ((height+1) * 100) + (thickStalk * 25));
    }
    /// <summary>
    /// Energieausgabe abhängig von height, regen, deepRoots, bigLeaves, largePetals und efficiency.
    /// </summary>
    private void PushEps() {

        float eps = (efficiency-regen-height);

        stats[10].SetCurrent(eps);
    }
    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void PushSunAbsorb() {

        stats[5].SetCurrent(stats[4].GetCurrent() + petals * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void PushWindAbsorb()
    {

        stats[6].SetCurrent(stats[4].GetCurrent() + leaves * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void PushWaterAbsorb()
    {

        stats[7].SetCurrent(stats[4].GetCurrent() + deepRoots * 0.1f);
    }

    
}
