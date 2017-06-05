using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour {
    
    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    public GroundTile myTile;

    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    public Blueprint myBlueprint;
    int currentStage = 0;

    List<int> builtSeq;
    List<int> wholeSeq;

    bool seqHasChanged = false;

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
    

    // Use this for initialization
    void Start () {
        myBlueprint = new Blueprint();
        wholeSeq = myBlueprint.getSequence();
    }
	
	// Update is called once per frame
	void Update () {
        checkSeqChange();
	}
    
    // Getters for stats
    public float getHealth() { return health.getCurrentValue(); }
    public float getAge() { return age.getCurrentValue(); }
    public float getMaxAge() { return maxAge; }
    public float getFoV() { return fieldOfView.getCurrentValue(); }
    public float getHealthPerSecond() { return healthPerSecond.getCurrentValue(); }
    public float getNutrientAbsorb() { return nutrientAbsorb.getCurrentValue(); }
    public float getSunAbsorb() { return sunAbsorb.getCurrentValue(); }
    public float getWindAbsorb() { return windAbsorb.getCurrentValue(); }
    public float getWaterAbsorb() { return waterAbsorb.getCurrentValue(); }
    public float getBankCapacity() { return bankCapacity.getCurrentValue(); }
    public float getDamageTaken() { return damageTaken.getCurrentValue();  }
    public float getEnergyPerSecond() { return energyPerSecond.getCurrentValue(); }


    public void setTile(GroundTile myTile)
    {
        this.myTile = myTile;
    }

    public GroundTile getTile()
    {
        return myTile;
    }

    public void setBlueprint(Blueprint myBlueprint)
    {
        this.myBlueprint = myBlueprint;
    }
    
    public Blueprint getBlueprint()
    {
        return myBlueprint;
    }

    /// <summary>
    /// Gibt den momentanen Wert für ein bestimmtes Upgrade an.
    /// </summary>
    /// <param name="i">UpgradeID</param>
    /// <returns>Momentaner Wert des Upgrades</returns>
    private float getCurrentValueForUpgrade(int i) {
        return myBlueprint.getUpgradeList()[i].getCurrentValue();
    }

    /// <summary>
    /// Passt die Stats neu an.
    /// </summary>
    public void pushChanges() {
        pushMaxHealth();
        pushFoV();
        pushMaxAge();
        pushDamageTaken();
        pushHps();
        pushNutrientAbsorb();
        pushBankCapacity();
        pushEps();
    }
    /// <summary>
    /// MaxHealth abhängig von height.
    /// </summary>
    private void pushMaxHealth() {
        float height = getCurrentValueForUpgrade(0);
        health.setMaxValue( (int)System.Math.Pow(2, height) * baseHealth);
    }
    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void pushFoV() {
        float height = getCurrentValueForUpgrade(0);
        fieldOfView.setCurrentValue(fieldOfView.getBaseValue() + height);
    }
    //TODO: Define params
    private void pushMaxAge() {
        // example
        for(int n = 0; n > myBlueprint.getUpgradeList().Count(); n++) {
            age.setMaxValue(age.getMaxValue() + getCurrentValueForUpgrade(n));
        }
    }
    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%.
    /// </summary>
    private void pushDamageTaken() {
        float efficiency = getCurrentValueForUpgrade(8);
        float thickStalk = getCurrentValueForUpgrade(7);

        damageTaken.setCurrentValue(damageTaken.getBaseValue() + (thickStalk * 0.1f) - (efficiency * 0.1f));
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void pushHps() {
        float regen = getCurrentValueForUpgrade(1);
        float insects = getCurrentValueForUpgrade(9);

        healthPerSecond.setCurrentValue((maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * damageTaken.getCurrentValue()));
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%.
    /// </summary>
    private void pushNutrientAbsorb() {
        float thickStalk = getCurrentValueForUpgrade(7);
        float porousRoots = getCurrentValueForUpgrade(5);

        nutrientAbsorb.setCurrentValue(nutrientAbsorb.getBaseValue() + (porousRoots * 0.1f) - (thickStalk * 0.1f));
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void pushBankCapacity() {
        float thickStalk = getCurrentValueForUpgrade(7);
        float height = getCurrentValueForUpgrade(0);

        bankCapacity.setMaxValue(bankCapacity.getBaseValue() + (height * 100) + (thickStalk * 25));
    }
    /// <summary>
    /// Energieausgabe abhängig von height, regen, deepRoots, bigLeaves, largePetals und efficiency.
    /// </summary>
    private void pushEps() {
        float height = getCurrentValueForUpgrade(0);
        float regen = getCurrentValueForUpgrade(1);
        float efficiency = getCurrentValueForUpgrade(8);
        float deepRoots = getCurrentValueForUpgrade(2);
        float bigLeaves = getCurrentValueForUpgrade(3);
        float largePetals = getCurrentValueForUpgrade(4);

        energyPerSecond.setCurrentValue(bankCapacity.getCurrentValue() * (height + regen + deepRoots + bigLeaves + largePetals - 2 * efficiency) * 0.01f);
    }
    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void pushSunAbsorb() {
        float largePetals = getCurrentValueForUpgrade(4);

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + largePetals * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void pushWindAbsorb()
    {
        float bigLeaves = getCurrentValueForUpgrade(3);

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + bigLeaves * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void pushWaterAbsorb()
    {
        float deepRoots = getCurrentValueForUpgrade(2);

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + deepRoots * 0.1f);
    }

    /// <summary>
    /// Arbeitet Upgrade an abzuarbeitender Stelle ab und passt dann Stats an
    /// </summary>
    private void workOnSequence() {

        //Sicherstellen ob noch ein Upgrade vorhanden ist
        if (currentStage < myBlueprint.getUpgradeList().Count())
        {
            int upgradeID = myBlueprint.getSequence()[currentStage];

            //Upgrade validation, sicherstellen dass es sich um ein vorhandenes Upgrade handelt, es gibt nur 0 bis numOfUpgrades Upgrades.
            if (upgradeID <= myBlueprint.getNumOfUpgrades() && upgradeID >= 0)
            {
                myBlueprint.incrementUpgrade(upgradeID);
                pushChanges();
                currentStage++;
            }
        }

    }

    /// <summary>
    /// Wird aufgerufen wenn sich der Blueprint ändert
    /// </summary>
    public void notifySeqChange() {
        seqHasChanged = true;
    }

    /// <summary>
    /// Wurde die Sequenz geändert?
    /// </summary>
    private void checkSeqChange() {
        if (!seqHasChanged)
        {
            checkSeqEnd();
        }
        else {
            resetUpgrades();
            checkUpgrades();
        }
    }

    /// <summary>
    /// Ist die Sequenz durchgearbeitet?
    /// </summary>
    private void checkSeqEnd() {
        if (currentStage < wholeSeq.Count() - 1) {
            checkUpgrades();
        }
    }


    /// <summary>
    /// Ist genug Energie für das nächste Upgrade vorhanden?
    /// </summary>
    private void checkUpgrades() {
        float cost = getUpgradeCost();
        if (cost <= bankCapacity.getCurrentValue()) {
            bankCapacity.setCurrentValue(getBankCapacity() - cost);
            upgradeNext();
        }
    }

    /// <summary>
    /// Gibt benötigte Energiekosten für nächstes Upgrade wieder
    /// </summary>
    /// <returns>Kosten werden pro Upgrade teurer</returns>
    private float getUpgradeCost() {
        return (currentStage + 1) * myBlueprint.getCost(wholeSeq[currentStage]);
    }

    /// <summary>
    /// Gibt dem Blueprint Anweisung Upgrade zu erhöhen
    /// </summary>
    private void upgradeNext() {
        myBlueprint.incrementUpgrade(currentStage);
        currentStage++;
        checkUpgrades();
    }

    /// <summary>
    /// Macht alle Upgrades rückgängig und gibt Energie zurück
    /// </summary>
    private void resetUpgrades() {
        int help = 1;
        foreach (int i in builtSeq) {
            BaseUpgrade u = myBlueprint.getUpgradeList()[i];
            float cost = u.getBaseCost() * help;
            help++;
            bankCapacity.setCurrentValue(getBankCapacity() + cost);
            u.decrementLevel();
        }
    }
}
