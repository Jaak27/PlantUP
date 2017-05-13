using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    /// <summary>
    /// Upgrade zum Erhöhen der FoV und des Alters.
    /// </summary>
    int height = 0;
    int maxHeight = 6;
    /// <summary>
    /// Upgrade zum Erhöhen der hps, erhöht aber auch eps.
    /// </summary>
    int regeneration = 0;
    int maxRegeneration = 6;
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
    int largePetals = 0;
    int maxLargePetals = 5;
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
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public int getHeight() { return height; }
    public int getRegeneration() { return regeneration; }
    public int getDeepRoots() { return deepRoots; }
    public int getBigLeaves() { return bigLeaves; }
    public int getLargePetals() { return largePetals; }
    public int getPorousRoots() { return porousRoots; }
    public int getSpreadRoots() { return spreadRoots; }
    public int getThickStalk() { return thickStalk; }
    public int getEfficiency() { return efficiency; }
    public int getInsects() { return insects; }

    public void setHeight(int i) {
        if (i <= maxHeight || i >= 0) {
            height = i;
        }
    }
    public void setRegeneration(int i)
    {
        if (i <= maxRegeneration || i >= 0)
        {
            regeneration = i;
        }
    }
    public void setDeepRoots(int i)
    {
        if (i <= maxDeepRoots || i >= 0)
        {
            deepRoots = i;
        }
    }
    public void setBigLeaves(int i)
    {
        if (i <= maxBigLeaves || i >= 0)
        {
            bigLeaves = i;
        }
    }
    public void setLargePetals(int i)
    {
        if (i <= maxLargePetals || i >= 0)
        {
            largePetals = i;
        }
    }
    public void setPorousRoots(int i)
    {
        if (i <= maxPorousRoots || i >= 0)
        {
            porousRoots = i;
        }
    }
    public void setSpreadRoots(int i)
    {
        if (i <= maxSpreadRoots || i >= 0)
        {
            spreadRoots = i;
        }
    }
    public void setThickStalk(int i)
    {
        if (i <= maxThickStalk || i >= 0)
        {
            thickStalk = i;
        }
    }
    public void setEfficiency(int i)
    {
        if (i <= maxEfficieny || i >= 0)
        {
            efficiency = i;
        }
    }
    public void setInsects(int i)
    {
        if (i <= maxInsects || i >= 0)
        {
            insects = i;
        }
    }
}
