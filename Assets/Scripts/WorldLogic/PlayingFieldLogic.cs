using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Markiert Felder auf dem Spielfeld.
/// </summary>
public interface isTile
{
    
    string getTileType();
    isTile[] getNeighbours();
    void setNeighbours(isTile[] neighbours);

    void setPlayingField(PlayingFieldLogic playingField);
    PlayingFieldLogic getPlayingField();
    
    int getLightValue();
    int getWindStrength();
    int getWindSpread();

    void forceWindUpdate();
    void updateWindStrength();

    
}

public class PlayingFieldLogic : MonoBehaviour {




    /// <summary>
    /// Die maximale Stärke des Windes der grad herrscht.
    /// </summary>
    int windStrength;

    public static readonly int minimumWindStrength = 20;
    public static readonly int maximumWindStrength = 150;
    /// <summary>
    /// Die Richtung aus der der Wind weht.
    /// 0 = links oben
    /// 1 = recht soben
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

	}


    public void GenerateRectangleMap()
    {
        Random.InitState(seed);

        //Speichert den Typen jedes Feld bevor es finaliesiert wird.
        int[,] tempField = new int[xSize, ySize];


        //Generiere ein Starttyp für jedes Feld.
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                //Generiere eine zufällige Zahl zwischen 0 und 10.
                float randomChance = Random.Range(0f, 10f);

                //70% Chance das es ein Groundfeld wird.
                if (randomChance < 7f)
                {
                    tempField[x, y] = 0;
                }
                //15% Chance für ein Wasserfeld
                else if (randomChance < 8.5f)
                {
                    tempField[x, y] = 1;
                }
                //15% chance für ein Gebirgsfeld
                else
                {
                    tempField[x, y] = 2;
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

                    int[,] neighbourCoords;

                    //Das Nachzählen der Feldertypen um dieses Feld, wenn ihr einen besseren Weg als diesen kennt, nennt ihn mir bitte!
                    //Jede Reihe braucht andere Koordianten für ihre Nachbarn (weil 6eckig statt viereckig)
                    if (y % 2 == 1)
                        neighbourCoords = unevenNeighbourCoords;
                    else
                        neighbourCoords = evenNeighbourCoords;

                    for (int j = 0; j < 6; j++)
                    {
                        if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == 2)
                        {
                            countMountain++;
                        }
                        else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == 1)
                            countWater++;
                    }

                    //Ein GroundFeld das neben 3 oder mehr Wasserfeldern ist, kann sich selbst in ein Wassfeld verwandeln.
                    //Ist es neben 1 oder 2 Gebirgsfeldern, kann es sich auch in ein Gebirgsfeld verwandeln
                    if (tempField[x, y] == 0)
                    {
                        //Chance sich in ein Wasserfeld zu verwandeln.
                        if (countWater >= 3)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.1 + 0.05 * countWater)
                                tempField[x, y] = 1;
                        }
                        //Chance sich in ein Gebirgsfeld zu verwandeln.
                        if (countMountain == 2 || countMountain == 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.15 * countMountain)
                                tempField[x, y] = 2;
                        }
                    }
                    //Gebirgsfelder können sich in Groundfelder verwandeln, wenn sie 2 oder mehr Wasserfelder berühren.
                    else if (tempField[x, y] == 2)
                    {
                        //Chance sich in ein Groundfeld zu verwandeln.
                        if (countWater >= 2)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.2 + 0.075 * countWater)
                                tempField[x, y] = 0;
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
                GameObject newTile = new GameObject();


                switch (tempField[x, y])
                {
                    case 1:
                        WaterTile tempWater = newTile.AddComponent<WaterTile>();
                        felder[y * xSize + x] = tempWater;
                        tempWater.setWaterStrength((int)Random.Range(WaterTile.minimumWaterStrength, WaterTile.maximumWaterStrength));
                        tempWater.setPlayingField(this);
                        break;
                    case 2:
                        MountainTile tempMountain = newTile.AddComponent<MountainTile>();
                        tempMountain.setPlayingField(this);
                        felder[y * xSize + x] = tempMountain;
                        break;
                    case 0:
                    default:
                        GroundTile tempGround = newTile.AddComponent<GroundTile>();
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
        }

        //Die Erstellung von allen Feldern ist jetzt abgeschlossen.
        //Nun folgt das setzen der weiteren Weltbedingten Resourcen wie Licht.
        windStrength = (int)Random.Range(minimumWindStrength, maximumWindStrength);
        lightStrength = (int)Random.Range(minimumLightStrength, maximumLightStrength);
        
        windDirection = Mathf.FloorToInt(Random.Range(0f, 5.99f));
    }
	
	// Update is called once per frame
	void Update () 
      {
		
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
