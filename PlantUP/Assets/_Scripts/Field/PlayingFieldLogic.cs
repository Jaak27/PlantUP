using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Markiert Felder auf dem Spielfeld.
/// </summary>
public interface isTile 
{
    tileType getTileType();
    isTile[] getNeighbours();
    void setNeighbours(isTile[] neighbours);

    void setPlayingField(PlayingFieldLogic playingField);
    PlayingFieldLogic getPlayingField();
    
    int getLightValue();
    int getWindStrength();
    int getWindSpread();

    int getNutrientValue();
    int getWaterStrength();

    void forceWindUpdate();
    void updateWindStrength();

    void removeObject();
    void replaceNeighbour(isTile oldTile, isTile newTile);
    Transform getTransform();
    
}

public enum tileType
{
    GROUND, WATER, MOUNTAIN, VOLCANO, ASH
}

public class PlayingFieldLogic : MonoBehaviour {

    /// <summary>
    /// Eine Struktur um Events zu speichern.
    /// Events ändern die Windstärke, Richtung oder die Lichtstärke.
    /// </summary>
    private class RandomEvent
    {
        //"Licht" 
        //"Windstärke"
        //"Richtung"
        public string type; 
        //Eine Zahl die auf die Licht oder Windstärke addiert wird, negativ für schwächen.
        //Für Windrichtung: positiv dreht den Wind im Uhrzeigersinn, eine Negative gegen den Uhrzeiger.
        public int change;
        //Wieviel Zeit zwischen diesem und dem vorherigen Event vergehen soll, in Ticks.
        public int time;
    }




    /// <summary>
    /// Speichert alle ausgewürfelten Events.
    /// </summary>
    List<RandomEvent> eventListe;


    public GroundTile groundTilePrefab;
    public AshTile ashTilePrefab;
    public WaterTile waterTilePrefab;
    public MountainTile mountainTilePrefab;
    public VolcanoTile volcanoTilePrefab;



    /// <summary>
    /// Die maximale Stärke des Windes der grad herrscht.
    /// </summary>
    int windStrength;

    public static readonly int minimumWindStrength = 20;
    public static readonly int maximumWindStrength = 150;
    /// <summary>
    /// Die Richtung aus der der Wind weht.
    /// 0 = links oben
    /// 1 = rechts oben
    /// 5 = links unten
    /// </summary>
    int windDirection;

    /// <summary>
    /// Die Menge an Energie die an Licht auf jedes Feld fällt.
    /// </summary>
    int lightStrength;

    public static readonly int  minimumLightStrength = 10;
    public  static readonly int maximumLightStrength = 50;


    /// <summary>
    /// Die Größe des Spielfeldes, in Feldern.
    /// Für Viereckige Spielfelder, Hexagonale nehmen nur xSize.
    /// </summary>
    public int xSize, ySize;

    //Diese beiden Arrays werden benötigt um allen Feldern die richtigen Nachbarn zugeben.
    static readonly int[,] unevenNeighbourCoords = { { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 0 } };
    static readonly int[,] evenNeighbourCoords = { { -1, -1 }, { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 1 }, { -1, 0 } };


    /// <summary>
    /// Speichert alle Felder dieses Spielfeldes.
    /// Wollen wir ein Sechseckiges Spielfeld, oder ein viereckiges?
    /// </summary>
    isTile[] felder;

    /// <summary>
    /// Der Seed für den Zufallszahlengenerator.
    /// </summary>
    public int seed;

	// Use this for initialization
	void Start ()
    {
        GenerateRectangleMap();
        GenerateEvents(18000);
	}


    public void GenerateRectangleMap()
    {
        Random.InitState(seed);

        //Speichert den Typen jedes Feld bevor es finaliesiert wird.
        tileType[,] tempField = new tileType[xSize, ySize];


        //Generiere ein Starttyp für jedes Feld.
       

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                //Generiere eine zufällige Zahl zwischen 0 und 10.
                float randomChance = Random.Range(0f, 10f);

                //80% Chance das es ein Groundfeld wird.
                if (randomChance < 8f)
                {
                    tempField[x, y] = tileType.GROUND;
                }
                //10% Chance für ein Wasserfeld
                else if (randomChance < 9f)
                {
                    tempField[x, y] = tileType.WATER;
                }
                //10% chance für ein Gebirgsfeld
                else
                {
                    tempField[x, y] = tileType.MOUNTAIN;
                }
            }
        }

        //Im Augenblick sind Gebirge und Wasser nur seltsam sporadisch verteilt, 
        //hier wird das Resultat noch ein bischen verändert.
        for (int i = 0; i < 3; i++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    int countMountain = 0;
                    int countWater = 0;
                    int countVolcano = 0;
                    int countAsh = 0;

                    int[,] neighbourCoords;

                    //Das Nachzählen der Feldertypen um dieses Feld, wenn ihr einen besseren Weg als diesen kennt, nennt ihn mir bitte!
                    //Jede Reihe braucht andere Koordianten für ihre Nachbarn (weil 6eckig statt viereckig)
                    if (y % 2 == 1)
                        neighbourCoords = unevenNeighbourCoords;
                    else
                        neighbourCoords = evenNeighbourCoords;

                    for (int j = 0; j < 6; j++)
                    {
                        if (x + neighbourCoords[j, 0] >= 0 && x + neighbourCoords[j, 0] < xSize
                            && y + neighbourCoords[j, 1] >= 0 && y + neighbourCoords[j, 1] < ySize)
                        { 

                            if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.MOUNTAIN)
                                countMountain++;
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.WATER)
                                countWater++;
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.VOLCANO)
                                countVolcano++;
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.ASH)
                                countAsh++;
                        }
                    }

                    //Ein GroundFeld das neben 3 oder mehr Wasserfeldern ist, kann sich selbst in ein Wassfeld verwandeln.
                    //Ist es neben 1 oder 2 Gebirgsfeldern, kann es sich auch in ein Gebirgsfeld verwandeln
                    //Ist es neben einem Vulkan oder einem Ashefeld, kann es sich in ein Ashfeld wandeln.
                    if (tempField[x, y] == tileType.GROUND)
                    {
                        //Chance sich in ein Wasserfeld zu verwandeln.
                        if (countWater >= 2)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.1 + 0.05 * countWater)
                                tempField[x, y] = tileType.WATER;
                        }
                        //Chance sich in ein Gebirgsfeld zu verwandeln.
                        if (countMountain == 2 || countMountain == 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.15 * countMountain)
                                tempField[x, y] = tileType.MOUNTAIN;
                        }
                        //Chance sich in ein Aschefeld zu verwandeln.
                        if (countVolcano >= 1 || countAsh >= 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.5 * countVolcano + 0.1 * countAsh)
                                tempField[x, y] = tileType.ASH;
                        }
                    }
                    //Wasserfelder neben Vulkanen > Boden
                    else if (tempField[x, y] == tileType.WATER)
                    {
                        //Chance sich in ein Groundfeld zu verwandeln.
                        if (countVolcano >= 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.3 + 0.2 * countVolcano)
                                tempField[x, y] = tileType.GROUND;
                        }

                    }
                    //Gebirgsfelder können sich in Groundfelder verwandeln, wenn sie 2 oder mehr Wasserfelder berühren.
                    //Gebirgsfelder können sich in Vulkane verwandeln.
                    else if (tempField[x, y] == tileType.MOUNTAIN)
                    {
                        //Chance sich in ein Groundfeld zu verwandeln.
                        if (countWater >= 2)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.2 + 0.075 * countWater)
                                tempField[x, y] = tileType.GROUND;
                        }
                        //Chance sich in ein Vulkan zu verwandeln
                        //0.05%
                        if (Random.Range(0f, 1f) <= 0.01)
                        {
                            tempField[x, y] = tileType.VOLCANO;
                        }

                    }

                }
            }
        }


        //Die ausgewürfelten Feldertypen werden jetzt in richtige Felder verwandelt.
        felder = new isTile[xSize * ySize];
        
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                

                Vector2 size = new Vector2(0.875f, 0.75f);
                Vector2 position2d;
                position2d = new Vector2(x - xSize / 2, y - ySize / 2);
                if (y % 2 == 1)
                    position2d.x += 0.5f;

                Vector3 position = new Vector3(this.transform.position.x + position2d.x * size.x, this.transform.position.y + position2d.y * size.y, 1);

                switch (tempField[x, y])
                {
                    case tileType.WATER:
                        WaterTile tempWater = Instantiate(waterTilePrefab, position, Quaternion.identity);
                        
                        felder[y * xSize + x] = tempWater;
                        tempWater.setWaterStrength((int)Random.Range(WaterTile.minimumWaterStrength, WaterTile.maximumWaterStrength));
                        tempWater.setPlayingField(this);
                        break;
                    case tileType.MOUNTAIN:
                        MountainTile tempMountain = Instantiate(mountainTilePrefab, position, Quaternion.identity);
                        tempMountain.setPlayingField(this);
                        felder[y * xSize + x] = tempMountain;
                        break;
                    case tileType.VOLCANO:
                        VolcanoTile tempVolcano = Instantiate(volcanoTilePrefab, position, Quaternion.identity);
                        tempVolcano.setPlayingField(this);
                        felder[y * xSize + x] = tempVolcano;
                        break;
                    case tileType.ASH:
                        AshTile tempAsh = Instantiate(ashTilePrefab, position, Quaternion.identity);
                        tempAsh.setPlayingField(this);
                        tempAsh.setNutrientValue((int)Random.Range(AshTile.minimumNutrientValue, AshTile.maximumNutrientValue));

                        felder[y * xSize + x] = tempAsh;
                        break;
                    case tileType.GROUND:
                    default:
                        GroundTile tempGround = Instantiate(groundTilePrefab, position, Quaternion.identity);
                        tempGround.setPlayingField(this);
                        tempGround.setNutrientValue((int)Random.Range(GroundTile.minimumNutrientValue, GroundTile.maximumNutrientValue));
                        felder[y * xSize + x] = tempGround;
                        break;
                }

                
            }
        }

        //Jetzt sind alle Felder erstellt, und allen Feldern können jetzt ihre Nachbarn gegeben werden.

        for (int i = 0; i < felder.Length; i++)
        {
            //
            int tempX = i % xSize;
            int tempY = (i - i % xSize) / xSize;

            isTile[] neighbours = new isTile[6];

            int[,] neighbourCoords;
            
            if (tempY % 2 == 1)
                neighbourCoords = unevenNeighbourCoords;
            else
                neighbourCoords = evenNeighbourCoords;

            for (int j = 0; j < 6; j++)
            {
                int tempX2 = tempX + neighbourCoords[j, 0];
                int tempY2 = tempY + neighbourCoords[j, 1];

                int tempListPosition = tempY2 * xSize + tempX2;

                if (tempListPosition >= 0 && tempListPosition < felder.Length)
                    neighbours[j] = felder[tempListPosition];
                else
                    neighbours[j] = null;
            }
            felder[i].setNeighbours(neighbours);
        }




        //Die Erstellung von allen Feldern ist jetzt abgeschlossen.
        //Nun folgt das setzen der weiteren Weltbedingten Resourcen wie Licht.
        windStrength = (int)Random.Range(minimumWindStrength, maximumWindStrength);
        lightStrength = (int)Random.Range(minimumLightStrength, maximumLightStrength);
        
        windDirection = Random.Range(0, 6);
    }
	
    public void GenerateEvents(int maximaleTickAnzahl)
    {
        eventListe = new List<RandomEvent>();
        int ticks = 0;
        while (ticks < maximaleTickAnzahl)
        {
            RandomEvent neu = new RandomEvent();
            //Würfel Typ aus
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    neu.type = "Licht";
                    //Würfel positiven oder negativenEffekt aus
                    if (Random.Range(0,2) == 0)
                    {
                        //Positiv
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie 1 oder +15, nicht so häufig auftreten.
                        //Durchschnitt wäre 7 oder 8.
                        neu.change = Random.Range(0, 8) + Random.Range(1, 9);
                    }
                    else
                    {
                        //Negativ
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie -1 oder -15, nicht so häufig auftreten.
                        //Durchschnitt wäre -7 oder -8.
                        neu.change = Random.Range(0, -8) + Random.Range(-1, -9);
                    }
                    break;
                case 1:
                    neu.type = "Windstärke";
                    //Würfel positiven oder negativenEffekt aus
                    if (Random.Range(0, 2) == 0)
                    {
                        //Positiv
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie 5 oder +35, nicht so häufig auftreten.
                        neu.change = Random.Range(2, 18) + Random.Range(3, 19);
                    }
                    else
                    {
                        //Negativ
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie -5 oder -35, nicht so häufig auftreten.
                        neu.change = Random.Range(-2, -18) + Random.Range(-3, -19);
                    }
                    break;
                case 2:
                    neu.type = "Richtung";
                    //Würfel Änderung nach links oder rechts aus
                    if (Random.Range(0, 2) == 0)
                    {
                        //Rechts
                        neu.change = 1;
                    }
                    else
                    {
                        //Links
                        neu.change = -1;
                    }
                    break;
                default:
                    neu.type = "Error";
                    //Ein Fehler wird ignoriert, und sollte sowieso nicht auftreten.
                    break;
            }
            //Wirf eine Zeit aus
            //Jeden Tick wird die Time des obersten Events um 1 verringert, erreicht sie 0 wird das Event ausgehandelt, und aus der Liste genommen.
            //Minimalwert = 0, Maximalwert = 240 Ticks, Durschnitt ist ~120?
            //Muss noch poliert werden.
            neu.time = Random.Range(0, 120) + Random.Range(0, 120) + Random.Range(0, 120);

            ticks += neu.time;
            eventListe.Add(neu);
        }
    }


	// Update is called once per frame
	void Update () 
    {
        //Starte die Events
        if (eventListe.Count > 0)
        {
            RandomEvent current = eventListe[0];
            current.time--;
            if (current.time <= 0)
            {
                eventListe.RemoveAt(0);
                handleEvent(current);
            }
        }
	}


    private void handleEvent(RandomEvent currentEvent)
    {
        switch (currentEvent.type)
        {
            case "Licht":
                lightStrength += currentEvent.change;
                if (lightStrength < minimumLightStrength)
                    lightStrength = minimumLightStrength;
                else if (lightStrength > maximumLightStrength)
                    lightStrength = maximumLightStrength;
                break;
            case "Windstärke":
                windStrength += currentEvent.change;
                if (windStrength < minimumWindStrength)
                    windStrength = minimumWindStrength;
                else if (windStrength > maximumWindStrength)
                    windStrength = maximumWindStrength;

                foreach (isTile tile in felder)
                {
                    tile.forceWindUpdate();
                }
                break;
            case "Richtung":
                windDirection += currentEvent.change;
                windDirection = windDirection % 6;
                foreach (isTile tile in felder)
                {
                    tile.forceWindUpdate();
                }
                break;
            default:
                break;
        }
    }




    /// <summary>
    /// Ersetzt ein Feld durch ein neugeneriertes Feld eines anderen (oder dem selben) Typs.
    /// Tötet jede Pflanze die sich auf dem Feld befindet, automatisch.
    /// </summary>
    public void replaceTile (isTile tile, tileType newType)
    {
        int index = findPositionOfTile(tile);
        switch (newType)
        {
            case tileType.GROUND:
                GroundTile tempGround = Instantiate(groundTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                tempGround.setPlayingField(this);
                tempGround.setNutrientValue((int)Random.Range(GroundTile.minimumNutrientValue, GroundTile.maximumNutrientValue));
                tempGround.setNeighbours(felder[index].getNeighbours());
                for (int i = 0; i < 6; i++)
                {
                    tempGround.getNeighbours()[i].replaceNeighbour(felder[index], tempGround);
                }
                felder[index].removeObject();
                felder[index] = tempGround;
                break;
            case tileType.ASH:
                AshTile tempAsh = Instantiate(ashTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                tempAsh.setPlayingField(this);
                tempAsh.setNutrientValue((int)Random.Range(AshTile.minimumNutrientValue, AshTile.maximumNutrientValue));
                tempAsh.setNeighbours(felder[index].getNeighbours());
                for (int i = 0; i < 6; i++)
                {
                    tempAsh.getNeighbours()[i].replaceNeighbour(felder[index], tempAsh);
                }
                felder[index].removeObject();
                felder[index] = tempAsh;
                break;
            case tileType.WATER:
                WaterTile tempWater = Instantiate(waterTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                tempWater.setPlayingField(this);
                tempWater.setWaterStrength((int)Random.Range(WaterTile.minimumWaterStrength, WaterTile.maximumWaterStrength));
                tempWater.setNeighbours(felder[index].getNeighbours());
                for (int i = 0; i < 6; i++)
                {
                    tempWater.getNeighbours()[i].replaceNeighbour(felder[index], tempWater);
                }
                felder[index].removeObject();
                felder[index] = tempWater;
                break;
            case tileType.MOUNTAIN:
                MountainTile tempMountain = Instantiate(mountainTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                tempMountain.setPlayingField(this);
                tempMountain.setNeighbours(felder[index].getNeighbours());
                for (int i = 0; i < 6; i++)
                {
                    tempMountain.getNeighbours()[i].replaceNeighbour(felder[index], tempMountain);
                }
                felder[index].removeObject();
                felder[index] = tempMountain;
                break;
            case tileType.VOLCANO:
                VolcanoTile tempVolcano = Instantiate(volcanoTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                tempVolcano.setPlayingField(this);
                tempVolcano.setNeighbours(felder[index].getNeighbours());
                for (int i = 0; i < 6; i++)
                {
                    tempVolcano.getNeighbours()[i].replaceNeighbour(felder[index], tempVolcano);
                }
                felder[index].removeObject();
                felder[index] = tempVolcano;
                break;
            default:
                break;
        }
    }


    int findPositionOfTile(isTile tile)
    {
        for (int i = 0; i < felder.Length;i++)
        {
            if (felder[0] == tile)
                return i;
        }
        return -1;
    }


    public int getLightStrength()
    {
        return lightStrength;
    }

    public int getWindDirection()
    {
        return windDirection;
    }

    public int getWindStrength()
    {
        return windStrength;
    }
}
