using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour {
    
    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    public GroundTile myGroundTile;
    
    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    public ProtoBlueprint myBlueprint;

    /// <summary>
    /// Count -> wieviele Stufen hat der Blueprint
    /// Index -> An welcher Stelle des Blueprint wir sind
    /// seqHasChanged -> Informiert uns wenn der Blueprint geändert wurde
    /// </summary>
    private int count;
    private int index = 0;
    private List<int> boughtUpgrades;
    private bool seqHasChanged = false;

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

    /// <summary>
    /// Liste der Upgrades
    /// </summary>
    private List<isUpgrade> upgrades;

    /// <summary>
    /// Das Alter der Pflanze.
    /// </summary>
    static readonly float baseAge = 0;
    static readonly float maxAge = 5;
    ContainerStat age = new ContainerStat(baseAge, baseAge, maxAge);

    /// <summary>
    /// Die Sichtweite der Pflanze in Tiles.
    /// </summary>
    static readonly float baseFieldOfView = 100;
    BaseStat fieldOfView = new BaseStat(2,2);

    /// <summary>
    /// Die Lebenskraft der Pflanze.
    /// </summary>
    static readonly float baseHealth = 100;
    static readonly float maxHealth = baseHealth;
    ContainerStat health = new ContainerStat(baseHealth, baseHealth, maxHealth);

    /// <summary>
    /// Health Per Second. Wieviel Leben regeneriert die Pflanze.
    /// </summary>
    static readonly float baseHealthPerSecond = 0;
    BaseStat healthPerSecond = new BaseStat(baseHealthPerSecond, baseHealthPerSecond);

    /// <summary>
    /// Die allgemeine nährstoffabsorbier Fähigkeit der Pflanze. 
    /// </summary>
    static readonly float baseNutrientAbsorb = 1;
    BaseStat nutrientAbsorb = new BaseStat(baseNutrientAbsorb, baseNutrientAbsorb);

    /// <summary>
    /// Die Fähigkeit Energie aus dSonnenkraft zu nehmen.
    /// </summary>
    static readonly float baseSunAbsorb = 1;
    BaseStat sunAbsorb = new BaseStat(baseSunAbsorb, baseSunAbsorb);

    /// <summary>
    /// Die Fähigkeit Energie aus Windkraftzu nehmen.
    /// </summary>
    static readonly float baseWindAbsorb = 1;
    BaseStat windAbsorb = new BaseStat(baseWindAbsorb, baseWindAbsorb);

    /// <summary>
    /// Die Fähigkeit Energie aus Wasserkraft zu nehmen.
    /// </summary>
    static readonly float baseWaterAbsorb = 1;
    BaseStat waterAbsorb = new BaseStat(baseWaterAbsorb, baseWaterAbsorb);

    /// <summary>
    /// Die Grösse des Energiespeichers der Pflanze.
    /// </summary>
    static readonly float baseBankCapacity = 0;
    static readonly float maxBankCapacity = 1000;
    ContainerStat bankCapacity = new ContainerStat(baseBankCapacity, baseBankCapacity, maxBankCapacity);

    /// <summary>
    /// Wieviel Schaden die Pflanze nimmt in Prozent.
    /// </summary>
    static readonly float baseDamageTaken = 1;
    BaseStat damageTaken = new BaseStat(baseDamageTaken, baseDamageTaken);

    /// <summary>
    /// Energy Per Second. Wieviel Energie die Pflanze verbraucht.
    /// </summary>
    static readonly float baseEnergyPerSecond = 0;
    BaseStat energyPerSecond = new BaseStat(baseEnergyPerSecond, baseEnergyPerSecond);
    

    // Getters for stats
    public float GetHealth() { return health.getCurrentValue(); }
    public float GetAge() { return age.getCurrentValue(); }
    public float GetMaxAge() { return maxAge; }
    public float GetFoV() { return fieldOfView.getCurrentValue(); }
    public float GetHealthPerSecond() { return healthPerSecond.getCurrentValue(); }
    public float GetNutrientAbsorb() { return nutrientAbsorb.getCurrentValue(); }
    public float GetSunAbsorb() { return sunAbsorb.getCurrentValue(); }
    public float GetWindAbsorb() { return windAbsorb.getCurrentValue(); }
    public float GetWaterAbsorb() { return waterAbsorb.getCurrentValue(); }
    public float GetBankCapacity() { return bankCapacity.getCurrentValue(); }
    public float GetDamageTaken() { return damageTaken.getCurrentValue();  }
    public float GetEnergyPerSecond() { return energyPerSecond.getCurrentValue(); }


    public void SetGroundTile(GroundTile myTile)
    {
        this.myGroundTile = myTile;
    }

    public GroundTile GetTile()
    {
        return myGroundTile;
    }

    public void SetBlueprint(ProtoBlueprint myBlueprint)
    {
        this.myBlueprint = myBlueprint;
    }
    
    public ProtoBlueprint GetBlueprint()
    {
        return myBlueprint;
    }

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
        boughtUpgrades = new List<int>();
        InvokeRepeating("AdjustStats", 0, 0.1f);
    }

    private void Update()
    {
        // TODO: Nur zum Testen der Stats
        energie = (int)bankCapacity.getCurrentValue();
        leben = (int)health.getCurrentValue();
        alter = (int)age.getCurrentValue();
        fov = (int)fieldOfView.getCurrentValue();
        hps = (int)healthPerSecond.getCurrentValue();
        nAbsorb = (int)nutrientAbsorb.getCurrentValue();
        sAbsorb = (int)sunAbsorb.getCurrentValue();
        wiAbsorb = (int)windAbsorb.getCurrentValue();
        waAbsorb = (int)waterAbsorb.getCurrentValue();
        eps = (int)energyPerSecond.getCurrentValue();
        schadenErlitten = (int)damageTaken.getCurrentValue();

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
        float energy = bankCapacity.getCurrentValue();

        if (cost <= energy)
        {
            if (upgrades[upgradeID].Inkrement())
            {
                bankCapacity.setCurrentValue(energy - cost);
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
            int value = myGroundTile.getNutrientValue();
            float nps = nutrientAbsorb.getCurrentValue();
            float energy = bankCapacity.getCurrentValue();

            if (value >= nps)
            {
                bankCapacity.setCurrentValue(energy + nps);
                myGroundTile.setNutrientValue(value - (int)nps);
            }
            else
            {
                bankCapacity.setCurrentValue(energy + value);
                myGroundTile.setNutrientValue(0);
                CancelInvoke();
                print("Groundtile empty");
            }
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
                bankCapacity.setCurrentValue(GetBankCapacity() + cost);
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
    }
    /// <summary>
    /// MaxHealth abhängig von height.
    /// </summary>
    private void PushMaxHealth() {
        float height = GetCurrentValueForUpgrade(0);
        health.setMaxValue( (int)System.Math.Pow(2, height+1) * baseHealth);
    }
    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void PushFoV() {
        float height = GetCurrentValueForUpgrade(0);
        fieldOfView.setCurrentValue(fieldOfView.getBaseValue() + height);
    }
    //TODO: Define params
    private void PushMaxAge() {
        // example
        for(int n = 0; n > myBlueprint.GetSequence().Count; n++) {
            age.setMaxValue(age.getMaxValue() + GetCurrentValueForUpgrade(n)+1);
        }
    }
    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%.
    /// </summary>
    private void PushDamageTaken() {
        float efficiency = GetCurrentValueForUpgrade(9);
        float thickStalk = GetCurrentValueForUpgrade(2);

        damageTaken.setCurrentValue(damageTaken.getBaseValue() + (thickStalk * 0.1f) - (efficiency * 0.1f));
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void PushHps() {
        float regen = GetCurrentValueForUpgrade(4);
        float insects = GetCurrentValueForUpgrade(5);

        healthPerSecond.setCurrentValue((maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * damageTaken.getCurrentValue()));
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%.
    /// </summary>
    private void PushNutrientAbsorb() {
        float thickStalk = GetCurrentValueForUpgrade(2);
        float porousRoots = GetCurrentValueForUpgrade(7);

        nutrientAbsorb.setCurrentValue(nutrientAbsorb.getBaseValue() + (porousRoots) - (thickStalk));
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void PushBankCapacity() {
        float thickStalk = GetCurrentValueForUpgrade(2);
        float height = GetCurrentValueForUpgrade(0);

        bankCapacity.setMaxValue(bankCapacity.getBaseValue() + ((height+1) * 100) + (thickStalk * 25));
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

        energyPerSecond.setCurrentValue(bankCapacity.getCurrentValue() * (height + regen + deepRoots + bigLeaves + largePetals - 2 * efficiency) * 0.01f);
    }
    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void PushSunAbsorb() {
        float largePetals = GetCurrentValueForUpgrade(3);

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + largePetals * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void PushWindAbsorb()
    {
        float bigLeaves = GetCurrentValueForUpgrade(1);

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + bigLeaves * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void PushWaterAbsorb()
    {
        float deepRoots = GetCurrentValueForUpgrade(6);

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + deepRoots * 0.1f);
    }

    
}
