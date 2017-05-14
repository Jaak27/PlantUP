using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour {




    //TODO: AGE HEALTH BANKCAPACITY ==> ContainerStat!









    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    GroundTile myTile;

    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    Blueprint myBlueprint;
    int [] upgrade;
    List<int> blueprintSequence = new List<int> { 1, 1, 3, 0 };
    int currentStage = 0;

    /// <summary>
    /// Das Alter der Pflanze.
    /// </summary>
    static readonly float baseAge = 0;
    float maxAge;
    BaseStat age = new BaseStat(baseAge, baseAge);

    /// <summary>
    /// Die Sichtweite der Pflanze in Tiles.
    /// </summary>
    static readonly float baseFieldOfView = 100;
    BaseStat fieldOfView = new BaseStat(2,2);

    /// <summary>
    /// Die Lebenskraft der Pflanze.
    /// </summary>
    static readonly float baseHealth = 100;
    float maxHealth = baseHealth;
    BaseStat health = new BaseStat(baseHealth, baseHealth);

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
    static readonly float baseBankCapacity = 1000;
    BaseStat bankCapacity = new BaseStat(baseBankCapacity, baseBankCapacity);

    /// <summary>
    /// Der Inhalt des Energiespeichers der Pflanze.
    /// </summary>
    static readonly float baseBankLevel = 0;
    BaseStat bankLevel = new BaseStat(baseBankLevel, baseBankLevel);

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

    public Plant(Blueprint bp) {
        if (bp != null)
        {
            myBlueprint = bp;
        }
        else {
            myBlueprint = new Blueprint();
        }
        //TODO: Nur zum testen hier, !!!!!!!!!!!!LÖSCHEN!!!!!!!!!!!!!!!!
        blueprintSequence = new List<int> { 1, 1, 3, 0 };
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
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
    public float getBankLevel() { return bankLevel.getCurrentValue(); }
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

    private void getCurrentUpgrades()
    {
        upgrade = myBlueprint.getAllUpgrades();
    }

    public void pushChanges() {
        getCurrentUpgrades();
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
        int height = upgrade[0];
        maxHealth = (int)System.Math.Pow(2, height) * baseHealth;
    }
    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void pushFoV() {
        int height = upgrade[0];
        fieldOfView.setCurrentValue(fieldOfView.getBaseValue() + height);
    }
    //TODO: Define params
    private void pushMaxAge() {
        // example
        foreach (int n in upgrade) {
            maxAge += n;
        }
    }
    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%.
    /// </summary>
    private void pushDamageTaken() {
        int efficiency = upgrade[8];
        int thickStalk = upgrade[7];

        damageTaken.setCurrentValue(damageTaken.getBaseValue() + (thickStalk * 0.1f) - (efficiency * 0.1f));
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void pushHps() {
        int regen = upgrade[1];
        int insects = upgrade[9];

        healthPerSecond.setCurrentValue((maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * damageTaken.getCurrentValue()));
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%.
    /// </summary>
    private void pushNutrientAbsorb() {
        int thickStalk = upgrade[7];
        int porousRoots = upgrade[5];

        nutrientAbsorb.setCurrentValue(nutrientAbsorb.getBaseValue() + (porousRoots * 0.1f) - (thickStalk * 0.1f));
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void pushBankCapacity() {
        int thickStalk = upgrade[7];
        int height = upgrade[0];

        bankCapacity.setCurrentValue(bankCapacity.getBaseValue() + (height * 100) + (thickStalk * 25));
    }
    /// <summary>
    /// Energieausgabe abhängig von height, regen, deepRoots, bigLeaves, largePetals und efficiency.
    /// </summary>
    private void pushEps() {
        int height = upgrade[0];
        int regen = upgrade[1];
        int efficiency = upgrade[8];
        int deepRoots = upgrade[2];
        int bigLeaves = upgrade[3];
        int largePetals = upgrade[4];

        energyPerSecond.setCurrentValue(bankCapacity.getCurrentValue() * (height + regen + deepRoots + bigLeaves + largePetals - 2 * efficiency) * 0.01f);
    }
    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void pushSunAbsorb() {
        int largePetals = upgrade[4];

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + largePetals * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void pushWindAbsorb()
    {
        int bigLeaves = upgrade[3];

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + bigLeaves * 0.1f);
    }
    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void pushWaterAbsorb()
    {
        int deepRoots = upgrade[2];

        sunAbsorb.setCurrentValue(nutrientAbsorb.getCurrentValue() + deepRoots * 0.1f);
    }
    private void getNextUpgrade() {
        int upgradeID = blueprintSequence.ElementAt(currentStage);

        //Upgrade validation step #1, making sure Sequence
        if (upgradeID <= myBlueprint.getNumOfUpgrades() && upgradeID >= 0) {
            myBlueprint.incrementUpgrade(upgradeID);
            pushChanges();
        }
    }
}
