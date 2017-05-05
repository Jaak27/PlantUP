using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    /// <summary>
    /// Upgrade zum Erhöhen der FoV und des Alters.
    /// </summary>
    int height = 1;
    int maxHeight = 6;
    /// <summary>
    /// Upgrade zum Erhöhen der hps, erhöht aber auch eps.
    /// </summary>
    int regeneration = 0;
    int maxRegeneration = 10;
    /// <summary>
    /// Upgrade zur Aufnahme von Energie über benachbarte WaterTiles.
    /// </summary>
    int deepRoots = 0;
    int maxDeepRoots = 10;
    /// <summary>
    /// Upgrade zur Aufnahme von Energie über Windstärke.
    /// </summary>
    int bigLeaves = 0;
    int maxBigLeaves = 10;
    /// <summary>
    /// Upgrade zur Aufnahme von Energie über Sonne.
    /// </summary>
    int largPetals = 0;
    int maxLargePetals = 10;
    /// <summary>
    /// Upgrade zur Aufnahme von Energie in der GroundTile.
    /// </summary>
    int porousRoots = 0;
    int maxPorousRoots = 10;
    /// <summary>
    /// Upgrade zur Aufnahme von Energie über benachbarte GroundTiles.
    /// </summary>
    int spreadRoots = 0;
    int maxSpreadRoots = 5;
    /// <summary>
    /// Upgrade zum Schutz vor Angriffen, hemmt Energieaufnahme.
    /// </summary>
    int thickStalk = 0;
    int maxThickStalk = 5;
    /// <summary>
    /// Upgrade zur effizienteren Energieausgabe.
    /// </summary>
    int efficiency = 0;
    int maxEfficieny = 5;
    /// <summary>
    /// Upgrade zur Chance auf periodische Samenverteilung.
    /// </summary>
    int insects = 0;
    int maxInsects = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
