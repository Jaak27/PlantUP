using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour {

    /// <summary>
    /// Upgrade zum Erhöhen der FoV und des Alters.
    /// </summary>
    static readonly int maxHeight = 6;
    BaseUpgrade height = new BaseUpgrade(maxHeight, "height");
    /// <summary>
    /// Upgrade zum Erhöhen der hps, erhöht aber auch eps.
    /// </summary>
    static readonly int maxRegeneration = 6;
    BaseUpgrade regeneration = new BaseUpgrade(maxHeight, "regeneration");

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über benachbarte WaterTiles.
    /// </summary>
    static readonly int maxDeepRoots = 10;
    BaseUpgrade deepRoots = new BaseUpgrade(maxHeight, "deepRoots");

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über Windstärke.
    /// </summary>
    static readonly int maxBigLeaves = 10;
    BaseUpgrade bigLeaves = new BaseUpgrade(maxHeight, "bigLeaves");

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über Sonne.
    /// </summary>
    static readonly int maxLargePetals = 5;
    BaseUpgrade largePetals = new BaseUpgrade(maxHeight, "largePetals");

    /// <summary>
    /// Upgrade zur Aufnahme von Energie in der GroundTile.
    /// </summary>
    static readonly int maxPorousRoots = 10;
    BaseUpgrade porousRoots = new BaseUpgrade(maxHeight, "porousRoots");

    /// <summary>
    /// Upgrade zur Aufnahme von Energie über benachbarte GroundTiles.
    /// </summary>
    static readonly int maxSpreadRoots = 5;
    BaseUpgrade spreadRoots = new BaseUpgrade(maxHeight, "spreadRoots");

    /// <summary>
    /// Upgrade zum Schutz vor Angriffen, hemmt Energieaufnahme.
    /// </summary>
    static readonly int maxThickStalk = 5;
    BaseUpgrade thickStalk = new BaseUpgrade(maxHeight, "thickStalk");

    /// <summary>
    /// Upgrade zur effizienteren Energieausgabe.
    /// </summary>
    static readonly int maxEfficieny = 5;
    BaseUpgrade efficiency = new BaseUpgrade(maxHeight, "efficiency");

    /// <summary>
    /// Upgrade zur Chance auf periodische Samenverteilung.
    /// </summary>
    static readonly int maxInsects = 3;
    BaseUpgrade insects = new BaseUpgrade(maxHeight, "insects");

    /// <summary>
    /// Sequenz in welcher die Upgrades abgearbeitet werden sollen
    /// </summary>
    List<int> blueprintSequence;
    List<BaseUpgrade> upgradeList;
    int numOfUpgrades = 10;

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

    public int[] getAllUpgrades()
    {
        int[] upgrades = { getHeight(),getRegeneration(),getDeepRoots(),getBigLeaves(),getLargePetals(),
                         getPorousRoots(),getSpreadRoots(),getThickStalk(),getEfficiency(),getInsects()};
        return upgrades;
    }

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
    /// Inkrementiere bestimmtes Upgrade.
    /// </summary>
    /// <param name="u">UpgradeID des Upgrades das inkrementiert werden soll</param>
    public void incrementUpgrade(int i) {
        upgradeList[i].incrementLevel();
    }

    private void setUpUpgradeList() {
        upgradeList = new List<BaseUpgrade> { height, regeneration, deepRoots, bigLeaves, largePetals,
                                            porousRoots, spreadRoots, thickStalk, efficiency, insects};
    }
    public List<BaseUpgrade> getUpgradeList()
    {
        return upgradeList;
    }
    public int getNumOfUpgrades()
    {
        return numOfUpgrades;
    }
    /// <summary>
    /// Füge abzuarbeitendes Upgrade der Sequenz hinzu
    /// </summary>
    /// <param name="upgradeNum">Einzelnes Upgrade als integer</param>
    public void addToSequence(int upgradeNum) {
        if (upgradeNum >= 0 && upgradeNum <= numOfUpgrades)
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
    public void addWholeSequence(int[] seq)
    {
        foreach (int i in seq)
        {
            if (i >= 0 && i <= numOfUpgrades)
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
}
