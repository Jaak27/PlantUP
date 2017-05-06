using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    GroundTile myTile;

    /// <summary>
    /// Eine Referenz auf den BluePrint.
    /// </summary>
    Blueprint myBlueprint;

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
    /// Die Nährstoffabsorbier Fähigkeit der Pflanze. 
    /// </summary>
    float nutrientAbsorb;
    float baseNutrientAbsorb = 1f;

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
    
    public int getHealth() { return health; }
    public int getAge() { return age; }
    public int getMaxAge() { return maxAge; }
    public int getFoV() { return fieldOfView; }
    public float getHps() { return hps; }
    public float getNutrientAbsorb() { return nutrientAbsorb; }
    public int getBankCapacity() { return bankCapacity; }
    public int getBankLevel() { return bankLevel; }
    public float getDamageTaken() { return damageTaken;  }
    public float getEps() { return eps; }


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

    public void pushChanges() {

    }
    /// <summary>
    /// MaxHealth abhängig von height.
    /// </summary>
    private void pushMaxHealth() {
        int height = myBlueprint.getHeight();
        maxHealth = (int)System.Math.Pow(2, height) * baseHealth;
    }
    /// <summary>
    /// Field of View abhängig von height
    /// </summary>
    private void pushFoV() {
        int height = myBlueprint.getHeight();
        fieldOfView = baseFov + height;
    }
    //TODO: Define params
    private void pushMaxAge() {

    }
    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%
    /// </summary>
    private void pushDamageTaken() {
        int efficiency = myBlueprint.getEfficiency();
        int thickStalk = myBlueprint.getThickStalk();

        damageTaken = baseDamageTaken + (thickStalk * 0.1f) - (efficiency * 0.1f);
    }
    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle
    /// </summary>
    private void pushHps() {
        int regen = myBlueprint.getRegeneration();
        int insects = myBlueprint.getInsects();

        hps = (maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * damageTaken);
    }
    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und largePetals, range 50% - 150%
    /// </summary>
    private void pushNutrientAbsorb() {
        int thickStalk = myBlueprint.getThickStalk();
        int largePetals = myBlueprint.getLargePetals();

        nutrientAbsorb = baseNutrientAbsorb + (largePetals * 0.1f) - (thickStalk * 0.1f);
    }
    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk
    /// </summary>
    private void pushBankCapacity() {
        int thickStalk = myBlueprint.getThickStalk();
        int height = myBlueprint.getHeight();

        bankCapacity = baseBankCapacity + (height * 100) + (thickStalk * 25);
    }
}
