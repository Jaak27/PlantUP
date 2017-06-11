using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    IsTile myTile;

    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    Blueprint myBlueprint;
    int[] currUpdates;

    /// <summary>
    /// Das Alter der Pflanze.
    /// </summary>
    int age = 0;
    int maxAge;

    /// <summary>
    /// Die Sichtweite der Pflanze in Tiles.
    /// </summary>
    int baseFov = 2;
    int fieldOfView;

    /// <summary>
    /// Die Lebenskraft der Pflanze.
    /// </summary>
    int maxHealth;
    int health;
    int baseHealth = 100;

    /// <summary>
    /// Health Per Second. Wieviel Leben regeneriert die Pflanze.
    /// </summary>
    float hps;

    /// <summary>
    /// Die allgemeine nährstoffabsorbier Fähigkeit der Pflanze. 
    /// </summary>
    float nutrientAbsorb;
    float baseNutrientAbsorb = 1f;

    /// <summary>
    /// Die Fähigkeit Energie aus dSonnenkraft zu nehmen.
    /// </summary>
    float sunAbsorb;
    float baseSunAbsorb = 1f;

    /// <summary>
    /// Die Fähigkeit Energie aus Windkraftzu nehmen.
    /// </summary>
    float windAbsorb;
    float baseWindAbsorb = 1f;

    /// <summary>
    /// Die Fähigkeit Energie aus Wasserkraft zu nehmen.
    /// </summary>
    float waterAbsorb;
    float baseWaterAbsorb = 1f;

    /// <summary>
    /// Die Grösse des Energiespeichers der Pflanze.
    /// </summary>
    int bankCapacity;
    int baseBankCapacity = 1000;

    /// <summary>
    /// Der Inhalt des Energiespeichers der Pflanze.
    /// </summary>
    int bankLevel;

    /// <summary>
    /// Wieviel Schaden die Pflanze nimmt in Prozent.
    /// </summary>
    float damageTaken;
    float baseDamageTaken = 1f;

    /// <summary>
    /// Energy Per Second. Wieviel Energie die Pflanze verbraucht.
    /// </summary>
    float eps;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    // Getters for stats
    public int getHealth() { return health; }
    public int getAge() { return age; }
    public int getMaxAge() { return maxAge; }
    public int getFoV() { return fieldOfView; }
    public float getHps() { return hps; }
    public float getNutrientAbsorb() { return nutrientAbsorb; }
    public float getSunAbsorb() { return sunAbsorb; }
    public float getWindAbsorb() { return windAbsorb; }
    public float getWaterAbsorb() { return waterAbsorb; }
    public int getBankCapacity() { return bankCapacity; }
    public int getBankLevel() { return bankLevel; }
    public float getDamageTaken() { return damageTaken;  }
    public float getEps() { return eps; }


    public void setTile(IsTile myTile)
    {
        this.myTile = myTile;
    }

    public IsTile getTile()
    {
        return myTile;
    }

    public void setBlueprint(Blueprint myBlueprint)
    {
        this.myBlueprint = myBlueprint;
        getCurrUpdates();
    }

    private void getCurrUpdates() {
        currUpdates[0] = myBlueprint.getHeight();
        currUpdates[1] = myBlueprint.getRegeneration();
        currUpdates[2] = myBlueprint.getDeepRoots();
        currUpdates[3] = myBlueprint.getBigLeaves();
        currUpdates[4] = myBlueprint.getLargePetals();
        currUpdates[5] = myBlueprint.getPorousRoots();
        currUpdates[6] = myBlueprint.getSpreadRoots();
        currUpdates[7] = myBlueprint.getThickStalk();
        currUpdates[8] = myBlueprint.getEfficiency();
        currUpdates[9] = myBlueprint.getInsects();
    }

    public Blueprint getBlueprint()
    {
        return myBlueprint;
    }

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
        int height = currUpdates[0];
        maxHealth = (int)System.Math.Pow(2, height) * baseHealth;
    }
    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void pushFoV() {
        int height = currUpdates[0];
        fieldOfView = baseFov + height;
    }
    //TODO: Define params
    private void pushMaxAge() {
        // example
        foreach (int n in currUpdates) {
            maxAge += n;
        }
    }
    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%.
    /// </summary>
    private void pushDamageTaken() {
        int efficiency = currUpdates[8];
        int thickStalk = currUpdates[7];

        damageTaken = baseDamageTaken + (thickStalk * 0.1f) - (efficiency * 0.1f);
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void pushHps() {
        int regen = currUpdates[1];
        int insects = currUpdates[9];

        hps = (maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * damageTaken);
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%.
    /// </summary>
    private void pushNutrientAbsorb() {
        int thickStalk = currUpdates[7];
        int porousRoots = currUpdates[5];

        nutrientAbsorb = baseNutrientAbsorb + (porousRoots * 0.1f) - (thickStalk * 0.1f);
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void pushBankCapacity() {
        int thickStalk = currUpdates[7];
        int height = currUpdates[0];

        bankCapacity = baseBankCapacity + (height * 100) + (thickStalk * 25);
    }
    /// <summary>
    /// Energieausgabe abhängig von height, regen, deepRoots, bigLeaves, largePetals und efficiency.
    /// </summary>
    private void pushEps() {
        int height = currUpdates[0];
        int regen = currUpdates[1];
        int efficiency = currUpdates[8];
        int deepRoots = currUpdates[2];
        int bigLeaves = currUpdates[3];
        int largePetals = currUpdates[4];

        eps = bankCapacity * (height + regen + deepRoots + bigLeaves + largePetals - 2 * efficiency) * 0.01f;
    }
    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void pushSunAbsorb() {
        int largePetals = currUpdates[4];

        sunAbsorb = nutrientAbsorb + largePetals * 0.1f;
    }
    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void pushWindAbsorb()
    {
        int bigLeaves = currUpdates[3];

        sunAbsorb = nutrientAbsorb + bigLeaves * 0.1f;
    }
    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void pushWaterAbsorb()
    {
        int deepRoots = currUpdates[2];

        sunAbsorb = nutrientAbsorb + deepRoots * 0.1f;
    }
}
