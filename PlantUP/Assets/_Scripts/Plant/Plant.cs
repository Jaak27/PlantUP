using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour
{
    float timestamp;
    public bool upgrading;

    /// <summary>
    /// Eine Referenz auf die GroundTile.
    /// </summary>
    public IsTile myTile;

    public void setTile(IsTile tile)
    {
        myTile = tile;
    }

    /// <summary>
    /// Eine Referenz auf den Blueprint.
    /// </summary>
    public Blueprint myBlueprint;

    /// <summary>
    /// Eine Referenz auf den Besitzer der Pflanze.
    /// </summary>
    public PlayerPrototype player;

    /// <summary>
    /// Liste der Upgrades
    /// </summary>
    private List<_IsUpgrade> upgrades;

    /// <summary>
    /// Liste der Stats
    /// </summary>
    private List<_IsStat> stats;

    /// <summary>
    /// Sammlung der Tiles in Reichweite der Upgrades
    /// </summary>
    private Dictionary<IsTile, int> inFoVReach;
    private Dictionary<IsTile, int> inSpreadRootsReach;
    private Dictionary<IsTile, int> inPorousRootsReach;
    private Dictionary<IsTile, int> inSunReach;

    /// <summary>
    /// SeqCount -> Stufen des Blueprints
    /// Index -> An welcher Stelle des Blueprint wir sind
    /// BlueprintCost -> Kosten des Blueprints
    /// plantCount -> Anzahl aller Pflanzen
    /// MyNum -> ID der Pflanze
    /// AbsorbRate -> Regler für Absorbierfähigkeit
    /// Ticks -> Zeitintervall in dem AjustStats() aufgerufen wird
    /// BoughtUpgrades -> Liste der bereits gekauften Upgrades, für wenn wir den BP ändern
    /// </summary>
    private int seqCount;
    private int index;
    private float blueprintCost;
    public static int plantCount = 0;
    public int myNum;
    public float absorbRate = 1f;
    private readonly float ticks = 5f;
    private List<int> boughtUpgrades;
    private String output = "";

    //Temporär public um zu sehen ob Upgrades funktionieren
    /// <summary>
    /// Upgrades der Pflanze
    /// </summary>
    //public int height;
    public int leaves;
    public int stalk;
    public int petals;
    //public int regen;
    //public int insects;
    public int deepRoots;
   // public int porousRoots;
    //public int spreadRoots;
    //public int efficiency;

    //Temporär um zu sehen ob Stats funktionieren
    public float energie;
    public float leben;
   // public float alter;
   // public float fov;
   // public float hps;
    public float nutriAbsorb;
    public float sunAbsorb;
    public float windAbsorb;
    public float waterAbsorb;
    // public float eps;
    // public float schadenErlitten;

    public float maxEnergie = 3000;


    public bool nutTilesinformed;

    public void SetMyTile(IsTile myTile)
    {
        this.myTile = myTile;
    }

    public IsTile GetMyTile()
    {
        return myTile;
    }

    public void SetBlueprint(Blueprint myBlueprint)
    {
        this.myBlueprint = myBlueprint;
    }

    public Blueprint GetBlueprint()
    {
        return myBlueprint;
    }

    public void SetPlayer(PlayerPrototype player)
    {
        this.player = player;
    }

    public PlayerPrototype GetPlayer()
    {
        return player;
    }

    public List<_IsStat> GetStats()
    {
        return stats;
    }

    public List<_IsUpgrade> GetUpgrades()
    {
        return upgrades;
    }

    public static int GetPlantCount()
    {
        return plantCount;
    }

    public int GetMyNum()
    {
        return myNum;
    }

    /// <summary>
    /// Initialisiere Pflanze, rufe AdjustStats()-Loop auf
    /// </summary>
    private void Start()
    {
        myNum = ++plantCount;


        myBlueprint.inkrementPlants();
        seqCount = myBlueprint.GetSequence().Count;

        boughtUpgrades = new List<int>();
        blueprintCost = myBlueprint.GetCost();
        InitPlant();
        PushChanges();
        InvokeRepeating("AdjustStats", 0, ticks);
        popUpTextController.Initialize();
    }

    /// <summary>
    /// Initialisiere Pflanze
    /// Index -> Index für Ablaufen des Blueprints wird auf 0 initialisiert
    /// InFoVReach, InSpreadRootsReach und InPorousRootsReach werden initialisiert
    /// </summary>
    private void InitPlant()
    {
        index = 0;
        inFoVReach = new Dictionary<IsTile, int>();
        inSpreadRootsReach = new Dictionary<IsTile, int>();
        inPorousRootsReach = new Dictionary<IsTile, int>();
        inSunReach = new Dictionary<IsTile, int>();
        InitUpgrades();
        InitStats();
    }

    /// <summary>
    /// Initialisieren der Upgrades
    /// </summary>
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

        upgrades = new List<_IsUpgrade> { u1, u2, u3, u4, u5, u6, u7, u8, u9, u10 };
    }

    /// <summary>
    /// Initialisieren der Stats
    /// </summary>
    private void InitStats()
    {
        Age s1 = new Age();
        FieldOfView s2 = new FieldOfView();
        Health s3 = new Health();
        HealthPerSecond s4 = new HealthPerSecond();
        NutrientAbsorb s5 = new NutrientAbsorb();
        SunAbsorb s6 = new SunAbsorb();
        WindAbsorb s7 = new WindAbsorb();
        WaterAbsorb s8 = new WaterAbsorb();
        Bank s9 = new Bank();
        DamageTaken s10 = new DamageTaken();
        EnergyPerSecond s11 = new EnergyPerSecond();

        stats = new List<_IsStat> { s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11 };
    }

    /// <summary>
    /// Jedes Update wird geschaut ob sich der Blueprint geändert hat
    /// TODO?: Blueprint sagt Spieler bescheid, Spieler sagt seinen Pflanzen bescheid
    /// </summary>
    private void Update()
    {

        maxEnergie = stats[8].GetMax();

        if (stats[8].GetCurrent() >= stats[8].GetMax())
        {
            stats[8].SetCurrent(stats[8].GetMax());
        }




        // TODO: Nur zum Testen der Stats
        //alter = stats[0].GetCurrent();
       //fov = stats[1].GetCurrent();
        leben = stats[2].GetCurrent();
       // hps = stats[3].GetCurrent();
        nutriAbsorb = stats[4].GetCurrent();
        sunAbsorb = stats[5].GetCurrent();
        windAbsorb = stats[6].GetCurrent();
        waterAbsorb = stats[7].GetCurrent();
        energie = stats[8].GetCurrent();
        //schadenErlitten = stats[9].GetCurrent();
        //eps = stats[10].GetCurrent();


        CheckSeqChange();

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
            else
            {
                //ReachOut();
                PushChanges();
            }
        }
        else
        {
            myBlueprint.ChangeNoticed();
            ResetUpgrades();
            print("CHANGED");
            CheckForNextUpgrade();

        }
    }

    /// <summary>
    /// Schaut ob die Sequenz im Blueprint noch nicht ans Ende gelangt ist
    /// </summary>
    /// <returns>true für es gibt ein weiteres Upgrade; false für wir sind am Ende angelangt</returns>
    private bool HasNext()
    {
        if (index < seqCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Wenn Kosten niedriger als Energievorhaben, inkrementiere Upgrade
    /// </summary>
    /// <param name="i">Nächst abzuarbeitendes Upgrade in der Sequenz</param>
    private void CheckForNextUpgrade()
    {
        int upgradeID = myBlueprint.GetSequence()[index];
        //int cost = upgrades[upgradeID].GetCost() * (index + 1);
        //print(upgrades[upgradeID].GetCost() * (index + 1));

        //int cost = upgrades[upgradeID].GetCost();

        int cost = 250;

        float energy = stats[8].GetCurrent();

        if (!upgrading )
        {
                //player.AddPoints(-cost);
                upgrading = true;
                print("TESST");
                timestamp = Time.time + 1;
                print(Time.time);
                print(timestamp);
                /*
                stats[8].SetCurrent(energy - cost);
                //print("Spieler" + player.GetPlayerNum() + "'s Pflanze Nr." + myNum + " hat Upgrade " + upgrades[upgradeID].getInfo() + " für " + cost + " gekauft.");

                boughtUpgrades.Add(upgradeID);
                */
            ;
        }

        if(upgrading)
        {
            if(timestamp < Time.time )
            {
                //popUpTextController.CreatePopUpText("NEW UP", gameObject.transform);
                
                //print("Spieler" + player.GetPlayerNum() + "'s Pflanze Nr." + myNum + " hat Upgrade " + upgrades[upgradeID].getInfo() + " für " + cost + " gekauft.");
                upgrades[upgradeID].Inkrement();
                boughtUpgrades.Add(upgradeID);
                upgrading = false;
                index++;
            }
        }

        PushChanges();

    }

    /// <summary>
    /// Passt die Stats neu an.
    /// </summary>
    public void PushChanges()
    {

        //Zum Testen der Upgrades
      //  height = upgrades[0].GetCurrent();
        leaves = upgrades[1].GetCurrent();
        stalk = upgrades[2].GetCurrent(); ;
        petals = upgrades[3].GetCurrent();
       // regen = upgrades[4].GetCurrent();
       // insects = upgrades[5].GetCurrent();
        deepRoots = upgrades[6].GetCurrent();
      //  porousRoots = upgrades[7].GetCurrent();
      //  spreadRoots = upgrades[8].GetCurrent();
       // efficiency = upgrades[9].GetCurrent();

        PushMaxHealth();
        //PushFoV();
        //PushMaxAge();
        //PushDamageTaken();
        //PushHps();
        PushNutrientAbsorb();
        PushBankCapacity();
        //PushEps();
        PushSunAbsorb();
        PushWaterAbsorb();
        PushWindAbsorb();

        ReachOut();
    }

    /// <summary>
    /// MaxHealth abhängig von height.
    /// </summary>
    private void PushMaxHealth()
    {
        //stats[2].SetMax((int)System.Math.Pow(2, height + 1) * stats[2].GetBase());
        stats[2].SetMax(1000 + stalk * 100 + petals * 100);
    }

    /// <summary>
    /// Field of View abhängig von height.
    /// </summary>
    private void PushFoV()
    {
        //stats[1].SetCurrent(height);
    }

    //TODO: Define params
    private void PushMaxAge()
    {
        // example
        for (int n = 0; n > myBlueprint.GetSequence().Count; n++)
        {
            stats[0].SetMax(stats[0].GetMax() + GetCurrentValueForUpgrade(n) + 1);
        }
    }

    /// <summary>
    /// DamageTaken abhängig von efficiency und thickStalk, range 50% - 150%.
    /// </summary>
    private void PushDamageTaken()
    {
        //stats[9].SetCurrent((efficiency - stalk) * 0.1f);
    }

    /// <summary>
    /// HPS von regeneration und insect abhängig,
    /// bei negativ spielt damageTaken eine Rolle.
    /// </summary>
    private void PushHps()
    {
        float maxHealth = stats[2].GetMax();

        //stats[3].SetCurrent((maxHealth * regen * 0.02f) - (maxHealth * insects * 0.01f * stats[9].GetCurrent()));
    }

    /// <summary>
    /// Basic Nutrient Absorb Value abhängig von thickStalk und deepRoots, range 50% - 150%.
    /// </summary>
    private void PushNutrientAbsorb()
    {
        //print("NUT UP");
        //stats[4].SetCurrent((stats[4].GetBase() + deepRoots - stalk) * absorbRate);
        stats[4].SetCurrent((stats[4].GetBase() + deepRoots));
    }

    /// <summary>
    /// Maximale Bank Kapazität abhängig von height und thickStalk.
    /// </summary>
    private void PushBankCapacity()
    {

        stats[8].SetMax(2000 + deepRoots * deepRoots * 1000 + leaves * leaves * 1000);
    }

    /// <summary>
    /// Energieausgabe abhängig von height, regen und efficiency.
    /// </summary>
    private void PushEps()
    {

        //float eps = (efficiency - regen - height) * absorbRate;

       // stats[10].SetCurrent(eps);
    }

    /// <summary>
    /// Pflanze gewinnt Sonnenenergie abhängig vom allgemeinen NutrientAbsorb und dem largePetals Upgrade.
    /// </summary>
    private void PushSunAbsorb()
    {

        //stats[5].SetCurrent((stats[5].GetBase() + petals) * absorbRate);
        stats[5].SetCurrent((stats[5].GetBase() + petals));
    }

    /// <summary>
    /// Pflanze gewinnt Windergie abhängig vom allgemeinen NutrientAbsorb und dem bigLeaves Upgrade.
    /// </summary>
    private void PushWindAbsorb()
    {

        //print("WIND UP");
        //stats[6].SetCurrent((stats[6].GetBase() + leaves - stalk) * absorbRate);
        stats[6].SetCurrent((stats[6].GetBase() + leaves));
    }

    /// <summary>
    /// Pflanze gewinnt Wasserergie abhängig vom allgemeinen NutrientAbsorb und dem deepRoots Upgrade.
    /// </summary>
    private void PushWaterAbsorb()
    {

        //stats[7].SetCurrent((stats[7].GetBase() + deepRoots - stalk) * absorbRate);
        stats[7].SetCurrent((stats[7].GetBase() + stalk));
    }

    /// <summary>
    /// Setze Tiles in Reichweite der Pflanze fest
    /// </summary>
    public void ReachOut()
    {
        //inFoVReach = SetInReach(height);
        //inSpreadRootsReach = SetInReach(spreadRoots);
    }

    /// <summary>
    /// Gibt den momentanen Wert für ein bestimmtes Upgrade an.
    /// </summary>
    /// <param name="i">UpgradeID</param>
    /// <returns>Momentaner Wert des Upgrades</returns>
    private int GetCurrentValueForUpgrade(int i)
    {
        return upgrades[i].GetCurrent();
    }

    /// <summary>
    /// Lege fest wie viele Tiles in der Reichweite eines Upgrades liegen
    /// 0: Nur die eigene Tile
    /// x: x-1 + seine Nachbarn
    /// </summary>
    /// <param name="level">Wieviele Stufen soll die Reichweite gehen</param>
    /// <returns></returns>
    public Dictionary<IsTile, int> SetInReach(int level)
    {
        Dictionary<IsTile, int> helpSet = new Dictionary<IsTile, int>();
        Dictionary<IsTile, int> inReach = new Dictionary<IsTile, int>
        {
            {myTile, 0}
        };
        int reach = 1;
        while (level > 0)
        {
            foreach (var tile in inReach)
            {
                foreach (IsTile nTile in tile.Key.getNeighbours())
                {
                    if (nTile != null)
                    {
                        if (!inReach.ContainsKey(nTile) && !helpSet.ContainsKey(nTile))
                        {
                            helpSet.Add(nTile, reach);
                        }
                    }
                }

            }
            foreach (KeyValuePair<IsTile, int> tilePair in helpSet)
            {
                if (!inReach.Contains(tilePair))
                {
                    inReach.Add(tilePair.Key, tilePair.Value);
                }
            }
            helpSet.Clear();
            --level;
            ++reach;
        }

        return inReach;

    }

    /// <summary>
    /// Macht alle Upgrades rückgängig und gibt Energie zurück
    /// </summary>
    private void ResetUpgrades()
    {
        print("RESET");
        int help = 1;
        int bought = boughtUpgrades.Count;
        if (bought > 0)
        {
            print("Pflanze Nr."+myNum+" Verkauft " + boughtUpgrades.Count + " Upgrades!");
        }
        foreach (int i in boughtUpgrades)
        {
            if (upgrades[i].Dekrement())
            {
                print("DEKREMENT");
                //int cost = upgrades[i].GetCost() * help;
               // player.AddPoints(500);
                //print("Pflanze Nr." + myNum + " hat ein Upgrade " + (i+1) + " für " + cost + " verkauft.");
                help++;
            }
        }
        boughtUpgrades = new List<int>();
        index = 0;
        seqCount = myBlueprint.GetSequence().Count;
    }

    /// <summary>
    /// Passe die Stats den entsprechenden Upgrades an
    /// </summary>
    private void AdjustStats()
    {
        if (myTile != null)
        {

            CalcNutriValue();
            CalcWaterValue();
            CalcSunValue();
            CalcWindValue();

            popUpTextController.CreatePopUpText(gameObject.GetComponent<Plant>().GetStats()[8].GetCurrent().ToString(), gameObject.transform);
            //CalcEnergyLoss();
 

            if (myNum == 1)
            {
                Debug.Log("<color=red>" + output + "</color>");
            }
            if (myNum == 2)
            {
                Debug.Log("<color=green>" + output + "</color>");
            }
            output = "";
        }

    }

    /// <summary>
    /// Berechne wieviel Nährstoff-Energie die Pflanze aus den Tiles Entzieht
    /// </summary>
    private void CalcNutriValue()
    {

        //IsTile thisTile;
        //int thisLevel;
        //float reach = 0;
        
        
        float nutriValue = myTile.getNutrientValue();
        float energy = stats[8].GetCurrent();
        float nups;
        nups = nutriValue * stats[4].GetCurrent();
        //popUpTextController.CreatePopUpText(nups.ToString(), gameObject.transform);
        stats[8].SetCurrent(energy + nups);
        //myTile.setNutrientValue(nutriValue - nups);


        /*
        for (int i = 0; i < 6; i++)
        {
            if (myTile.getNeighbours()[i].getTileType() == tileType.GROUND || myTile.getNeighbours()[i].getTileType() == tileType.SWAMP)
            {
                nutriValue = myTile.getNeighbours()[i].getNutrientValue();
                if( nutriValue > 0)
                {
                    energy = stats[8].GetCurrent();
                    //popUpTextController.CreatePopUpText(wups.ToString(), gameObject.transform);
                    nups = nutriValue * stats[4].GetCurrent();
                    stats[8].SetCurrent(energy + nups);
                }

            }
        }
        */

            //Für jede Tile in der "Verbreitete Wurzeln" Reichweite der Pflanze
            /*
            foreach (KeyValuePair<IsTile, int> pair in inSpreadRootsReach)
            {
                thisTile = pair.Key;
                thisLevel = pair.Value;

                switch (thisLevel)
                {
                    case 0:
                        reach = 1.0f;
                        break;
                    case 1:
                        reach = 0.14f;
                        break;
                    case 2:
                        reach = 0.05f;
                        break;
                    case 3:
                        reach = 0.001f;
                        break;
                }

                float nutriValue = myTile.getNutrientValue();

                //Wenn die Tile Nährstoffe enthält
                if (nutriValue > 0)
                {

                    int avgGroundValue = (PlayingFieldLogic.minimumGroundValueStart + PlayingFieldLogic.maximumGroundValueStart) / 2;

                    //float nups = stats[4].GetCurrent() * reach * avgGroundValue * 0.01f * absorbRate;
                    float nups = 50.0f;
                    float energy = stats[8].GetCurrent();
                   // popUpTextController.CreatePopUpText("ZEST", gameObject.transform);


                    //Wenn die Tile mehr Nährstoffe enthält als die Pflanze gebrauchen kann
                    if (nutriValue >= nups)
                    {
                        stats[8].SetCurrent(energy + nups);                 //Berechne gewonnene Energie dazu
                        //player.AddPoints(nups);                             //Berechne Spielerpunkte dazu
                        myTile.setNutrientValue(nutriValue - nups);  //Entziehe der Tile die Nährstoffe

                        output += ("Nutri: " + nups + ", ");
                    }
                    //Enthält die Tile weniger Nährstoffe als benötigt ziehen wir den Rest ab
                    else
                    {
                        stats[8].SetCurrent(energy + nutriValue);
                        //player.AddPoints(nutriValue);
                        myTile.setNutrientValue(0);                       //Jetzt ist die Tile komplett leer
                        output += ("Nutri: " + nups + ", ");
                    }
                }
            //}*/

        }

        /// <summary>
        /// Berechne wieviel Wasser-Energie die Pflanze aus den Tiles Entzieht
        /// </summary>
        /// 

        private void CalcWaterValue()
    {

        for(int i = 0; i < 6; i++)
        {
            if(myTile.getNeighbours()[i] != null)
            {
                if(myTile.getNeighbours()[i].getTileType() == tileType.WATER)
                {
                    float waterValue = myTile.getNeighbours()[i].getWaterStrength();
                    float wups = stats[7].GetCurrent() * waterValue;
                    float energy = stats[8].GetCurrent();
                    //popUpTextController.CreatePopUpText(wups.ToString(), gameObject.transform);
                    stats[8].SetCurrent(energy + wups);
                }
            }



        }
        /*
        int reach = porousRoots;

        if (reach == 0)
        {
            reach = 1;
        }

        inPorousRootsReach = SetInReach(reach);

        IsTile thisTile;
        int thisLevel;
        float reachMultiplier = 0;

        //Für jede Tile in der "Poröse Wurzeln" Reichweite der Pflanze
        foreach (KeyValuePair<IsTile, int> pair in inPorousRootsReach)
        {
            thisTile = pair.Key;
            thisLevel = pair.Value;

            switch (thisLevel)
            {
                case 0:
                    reachMultiplier = 1.0f;
                    break;
                case 1:
                    reachMultiplier = 0.5f;
                    break;
                case 2:
                    reachMultiplier = 0.05f;
                    break;
            }

            float waterValue = thisTile.getWaterStrength();

            //Wenn die Tile Wasser enthält
            if (waterValue > 0)
            {
                float waps = stats[7].GetCurrent() * reachMultiplier * waterValue * absorbRate;
                float energy = stats[8].GetCurrent();

                //Wenn die Tile mehr Wasser enthält als die Pflanze gebrauchen kann
                if (waterValue >= waps)
                {
                    stats[8].SetCurrent(energy + waps);                     //Berechne gewonnene Energie dazu
                    player.AddPoints(waps);                                 //Berechne Spielerpunkte 

                    output += ("Water: " + waps + ", ");
                    //thisTile.setWaterStrength(waterValue - waps); //Entziehe der Tile die Nährstoffe
                }
                //Enthält die Tile weniger Wasser als benötigt ziehen wir den Rest ab
                else
                {
                    stats[8].SetCurrent(energy + waterValue);
                    player.AddPoints(waterValue);

                    output += ("Water: " + waps + ", ");
                    //thisTile.setWaterStrength(0);                           //Jetzt ist die Tile komplett leer
                }
            }

        }
        */
    }
    
    /// <summary>
    /// Berechne wieviel Wind-Energie die Pflanze aus der Tile Entzieht
    /// </summary>
    private void CalcWindValue()
    {
        float windValue = myTile.getWindStrength();
        float wips = stats[6].GetCurrent() * windValue;
        float energy = stats[8].GetCurrent();
        //popUpTextController.CreatePopUpText(wips.ToString(), gameObject.transform);

        stats[8].SetCurrent(energy + wips);

        /*
        //Weht der Wind auf der Tile der Pflanze stark genug gibt es die vollen Werte dafür
        if (windValue >= wips)
        {
            stats[8].SetCurrent(energy + wips);
            //player.AddPoints(wips);

            output += ("Wind: " + wips + ", ");
        }
        //Wenn nicht gibt es nur den maximalen Wert der das Feld hergibt
        else
        {
            stats[8].SetCurrent(energy + windValue);
            //player.AddPoints(windValue);

            output += ("Wind: " + wips + ", ");
        }
        */
    }


    
    /// <summary>
    /// Berechne wieviel Sonnen-Energie die Pflanze aus der Map Entzieht
    /// </summary>
    private void CalcSunValue()
    {


        float sunValue = myTile.getLightValue();
        float energy = stats[8].GetCurrent();
        float sups;
        sups = sunValue * stats[5].GetCurrent();
        //popUpTextController.CreatePopUpText(sups.ToString(), gameObject.transform);
        stats[8].SetCurrent(energy + (int) sups);
        //myTile.setNutrientValue(nutriValue - nups);


        /*
        for (int i = 0; i < 6; i++)
        {
            if (myTile.getNeighbours()[i].getTileType() == tileType.DESERT)
            {
                sunValue = myTile.getNeighbours()[i].getLightValue();
                if(sunValue > 0)
                {
                    energy = stats[8].GetCurrent();
                    //popUpTextController.CreatePopUpText(wups.ToString(), gameObject.transform);
                    sups = sunValue * stats[5].GetCurrent();
                    stats[8].SetCurrent(energy + sups);
                }

            }
        }
        */
        /*
        //=================================================Schatten Berechnen======================================================================
        float shadowPenalty = 0.8f;

        int maxReach = upgrades[0].GetMax();
        int myReach = height;
        int otherFoV = 0;
        int awayFromMe = 0;

        Plant otherPlant;
        List<int> shadowCasters = new List<int>();

        inSunReach = SetInReach(maxReach);

        foreach (KeyValuePair<IsTile, int> pair in inSunReach)
        {

            IsTile tile = pair.Key;

            awayFromMe = pair.Value;

            if (awayFromMe != 0)
            {
                //Wenn die Tile eine Pflanze beherbergen kann
                if (tile.canSustainPlant)
                {

                    //Und eine Pflanze beherbergt
                    if ((otherPlant = tile.getPlant()) != null)
                    {
                        otherFoV = otherPlant.height;
                        //Bin ich in der FoV dieser Pflanze und ist sie grösser als ich
                        if ((otherFoV - awayFromMe) >= 0 && otherFoV > myReach)
                        {
                            if (!shadowCasters.Contains(otherPlant.height))
                            {
                                shadowCasters.Add(otherPlant.height);
                            }
                        }

                    }

                }
            }
        }

        float sunValue = myTile.getLightValue();
        //Für jeden Layer von Schatten ziehen wir 20% Lichtstärke ab
        foreach (int i in shadowCasters)
        {
            sunValue *= shadowPenalty;
        }

        //=================================================Energie aus Licht ziehen======================================================================

        float sups = stats[5].GetCurrent() * sunValue * absorbRate;
        float energy = stats[8].GetCurrent();

        //Scheint die Sonne stark genug gibt es die vollen Werte dafür
        if (sunValue >= sups)
        {
            stats[8].SetCurrent(energy + sups);
            player.AddPoints(sups);
            output += ("Sun: " + sups + ", ");
        }
        //Wenn nicht gibt es nur den maximalen Wert der die Map hergibt
        else
        {
            stats[8].SetCurrent(energy + sunValue);
            player.AddPoints(sunValue);
            output += ("Sun: " + sups + ", ");
        }
        */
    }

    

    /// <summary>
    /// Berechne den Energieverlust der Pflanze
    /// </summary>
    private void CalcEnergyLoss()
    {
        float eps = stats[10].GetCurrent();

        float energy = stats[8].GetCurrent();
        float health = stats[2].GetCurrent();
        float result = energy + eps;

        //Sind noch Energiereserven vorhanden ziehe den Verlust davon ab
        if (result >= 0)
        {
            stats[8].SetCurrent(result);
        }
        //Ist keine Energie mehr vorhanden, ziehe der Pflanze Leben ab
        else
        {
            stats[2].SetCurrent(health + result);
        }

    }

    /// <summary>
    /// Berechne die Lebenspunkte der Pflanze
    /// </summary>
    private void CalcHealth()
    {
        float hps = stats[3].GetCurrent();
        float dps = stats[9].GetCurrent();
        float health = stats[2].GetCurrent();
        float newHealth = health + hps - dps;                                                           //Leben + regeneration - schaden erlitten

        //Sind die neuerechneten Lebenspunkte unter der Maximalgrenze
        if (newHealth <= stats[2].GetMax())
        {
            //Sind die Lebenspunkte über 0
            if (newHealth > 0)
            {
                stats[2].SetCurrent(newHealth);                                                         //Setze Lebenspunkte auf neuen Wert
            }
            //Sind die Lebenspunkte 0 oder weniger
            else
            {
                print("Pflanze gestorben");
                myTile.setNutrientValue((int)(myTile.getNutrientValue() + stats[8].GetCurrent()) / 10);   //Gebe der Tile einen Teil der angesammelten Nährstoffe zurück
                //myTile.setPlant(null);
                --plantCount;
                Destroy(gameObject);                                                                    //Die Pflanze ist tot
            }
        }
    }


}
