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
    int age;

    /// <summary>
    /// Das maximale Alter der Pflanze.
    /// </summary>
    int maxAge;

    /// <summary>
    /// Die Sichtweite der Pflanze in Tiles.
    /// </summary>
    int fieldOfView;

    /// <summary>
    /// Die Lebenskraft der Pflanze.
    /// </summary>
    int health;

    /// <summary>
    /// Health Per Second. Wieviel Leben regeneriert die Pflanze.
    /// </summary>
    float hps;

    /// <summary>
    /// Die Nährstoffabsorbier Fähigkeit der Pflanze. 
    /// </summary>
    float nutrientAbsorb;

    /// <summary>
    /// Die Grösse des Energiespeichers der Pflanze.
    /// </summary>
    int bankCapacity;

    /// <summary>
    /// Der Inhalt des Energiespeichers der Pflanze.
    /// </summary>
    int bankLevel;

    /// <summary>
    /// Wieviel Schaden die Pflanze nimmt in Prozent.
    /// </summary>
    float damageTaken;

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
    public float getRegen() { return hps; }
    public float getNutrientAbsorb() { return nutrientAbsorb; }
    public int getBankCapacity() { return bankCapacity; }
    public int getBankLevel() { return bankLevel; }
    public float getDamageTaken() { return damageTaken;  }
    public float getEnergyOut() { return eps; }


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
}
