using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    
    /// <summary>
    /// Upgrade zum Erhöhen der FoV und des Alters.
    /// </summary>
    static readonly int maxHeight = 6;
    BaseUpgrade height = new BaseUpgrade(maxHeight, "height", 100);
    /// <summary>
    /// Upgrade zum Erhöhen der hps, erhöht aber auch eps.
    /// </summary>
    static readonly int maxRegeneration = 6;
    BaseUpgrade regeneration = new BaseUpgrade(maxHeight, "regeneration", 250);

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über benachbarte WaterTiles.
    /// </summary>
    static readonly int maxDeepRoots = 10;
    BaseUpgrade deepRoots = new BaseUpgrade(maxHeight, "deepRoots", 100);

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über Windstärke.
    /// </summary>
    static readonly int maxBigLeaves = 10;
    BaseUpgrade bigLeaves = new BaseUpgrade(maxHeight, "bigLeaves", 100);

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über Sonne.
    /// </summary>
    static readonly int maxLargePetals = 5;
    BaseUpgrade largePetals = new BaseUpgrade(maxHeight, "largePetals", 100);

    /// <summary>
    /// Upgrade zur Aufnahme von Energie in der GroundTile.
    /// </summary>
    static readonly int maxPorousRoots = 10;
    BaseUpgrade porousRoots = new BaseUpgrade(maxHeight, "porousRoots", 100);

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über benachbarte GroundTiles.
    /// </summary>
    static readonly int maxSpreadRoots = 5;
    BaseUpgrade spreadRoots = new BaseUpgrade(maxHeight, "spreadRoots", 100);

    /// <summary>
    /// Upgrade zum Schutz vor Angriffen, hemmt Energieaufnahme.
    /// </summary>
    static readonly int maxThickStalk = 5;
    BaseUpgrade thickStalk = new BaseUpgrade(maxHeight, "thickStalk", 100);

    /// <summary>
    /// Upgrade zur effizienteren Energieausgabe.
    /// </summary>
    static readonly int maxEfficieny = 5;
    BaseUpgrade efficiency = new BaseUpgrade(maxHeight, "efficiency", 250);

    /// <summary>
    /// Upgrade zur Chance auf periodische Samenverteilung.
    /// </summary>
    static readonly int maxInsects = 3;
    BaseUpgrade insects = new BaseUpgrade(maxHeight, "insects", 450);

    /// <summary>
    /// Sequenz in welcher die Upgrades abgearbeitet werden sollen
    /// </summary>
    List<int> blueprintSequence;

    /// <summary>
    /// Liste aller Upgrades
    /// </summary>
    List<BaseUpgrade> upgradeList;

    /// <summary>
    /// Konstruktor mit Sequenz für den Blueprint 
    /// </summary>
    /// <param name="seq">Sequenz</param>
    public Blueprint(List<int> seq) {

        setUpUpgradeList();
        addWholeSequence(seq);

    }

    /// <summary>
    /// Konstruktor für Leeren Blueprint
    /// </summary>
    public Blueprint() {
        setUpUpgradeList();
    }

    public int getHeight() { return height.getCurrentValue(); }
    public int getRegeneration() { return regeneration.getCurrentValue(); }
    public int getDeepRoots() { return deepRoots.getCurrentValue(); }
    public int getBigLeaves() { return bigLeaves.getCurrentValue(); }
    public int getLargePetals() { return largePetals.getCurrentValue(); }
    public int getPorousRoots() { return porousRoots.getCurrentValue(); }
    public int getSpreadRoots() { return spreadRoots.getCurrentValue(); }
    public int getThickStalk() { return thickStalk.getCurrentValue(); }
    public int getEfficiency() { return efficiency.getCurrentValue(); }
    public int getInsects() { return insects.getCurrentValue(); }
    
    /// <summary>
    /// Setze Upgrade auf bestimmtes Level.
    /// </summary>
    /// <param name="u">Upgrade</param>
    /// <param name="i">Level</param>
    public void setUpgradeLevel(BaseUpgrade u, int i)
    {
        u.setLevel(i);
    }

    /// <summary>
    /// Setup für neuen Blueprint
    /// </summary>
    private void setUpUpgradeList()
    {
        upgradeList = new List<BaseUpgrade> { height, regeneration, deepRoots, bigLeaves, largePetals,
                                            porousRoots, spreadRoots, thickStalk, efficiency, insects};
        blueprintSequence = new List<int>();
    }

    /// <summary>
    /// Inkrementiere bestimmtes Upgrade.
    /// </summary>
    /// <param name="u">UpgradeID des Upgrades das inkrementiert werden soll</param>
    public void incrementUpgrade(int i) {
        upgradeList[i].incrementLevel();
    }
    
    public int getNumOfUpgrades() {
        return upgradeList.Count;
    }
    public List<BaseUpgrade> getUpgradeList()
    {
        return upgradeList;
    }
    /// <summary>
    /// Füge abzuarbeitendes Upgrade der Sequenz hinzu
    /// </summary>
    /// <param name="upgradeNum">Einzelnes Upgrade als integer</param>
    public void addToSequence(int upgradeNum) {
        if (upgradeNum >= 0 && upgradeNum < upgradeList.Count)
        {
            blueprintSequence.Add(upgradeNum);
        }
        else
        {
            Debug.Log(upgradeNum + "ist keine UpgradeID, kann nicht zur Sequenz hinzugefügt werden.");
        }
    }

    /// <summary>
    /// Füge komplette Sequenz hinzu 
    /// </summary>
    /// <param name="seq">Integer array mit Upgradesequenz</param>
    public void addWholeSequence(List<int> seq)
    {
        foreach (int i in seq)
        {
            if (i >= 0 && i < upgradeList.Count)
            {
                blueprintSequence.Add(i);
            }
            else
            {
                Debug.Log(i + "ist keine UpgradeID, kann nicht zur Sequenz hinzugefügt werden.");
            }
        }
    }

    /// <summary>
    /// Leere vorhandene Sequenz
    /// </summary>
    public void resetSequence()
    {
        blueprintSequence.Clear();
    }

    public List<int> getSequence() {
        return blueprintSequence;
    }

    internal int getCost(int v)
    {
        BaseUpgrade u = upgradeList[v];
        int i = (u.getCurrentValue()+1) * u.getBaseCost();
        return i;
    }
}
