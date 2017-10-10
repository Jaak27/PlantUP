using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum tileType
{
    GROUND = 0, WATER, MOUNTAIN, VOLCANO, ASH, ICE, FIRE, SWAMP, DESERT
}

public enum weatherType
{
    NORMAL, BLIZZARD, ERUPTION
}

public class PlayingFieldLogic : MonoBehaviour
{
    public ParticleSystem erupt;

    public List<IsTile> vulcanos;

    /// <summary>
    /// Eine Struktur um Events zu speichern.
    /// Events ändern die Windstärke, Richtung oder die Lichtstärke.
    /// </summary>
    public class RandomEvent
    {
        public eventTypes type;
        //Eine Zahl die auf die Licht oder Windstärke addiert wird, negativ für schwächen.
        //Für Windrichtung: positiv dreht den Wind im Uhrzeigersinn, eine Negative gegen den Uhrzeiger.
        public int change;
        //Wieviel Zeit zwischen diesem und dem vorherigen Event vergehen soll, in Ticks.
        public int time;
    }

    public enum eventTypes
    {
        LIGHT, WINDSTRENGTH, WINDDIRECTION, BLIZZARDSTART, ERUPTIONSTART, WEATHERSTOP, NULL
    }

    public  int minimumBlizzardDuration = 400;
    public int maximumBlizzardDuration = 1200;

    public  int minimumEruptionDuration = 100;
    public  int maximumEruptionDuration = 250;


    public int averageTimeBetweenEvents = 180;

    public int normalEventChance = 9;
    public int windStrengthChangeChance = 1;
    public int windDirectionChangeChance = 1;
    public int LightStrengthChangeChance = 1;
    public int catastropheEventChance = 2;
    public int BlizzardChance = 1;
    public int EruptionChance = 1;

    /// <summary>
    /// Speichert alle ausgewürfelten Events.
    /// </summary>
    List<RandomEvent> eventListe = null;


    public IsTile groundTilePrefab;
    public IsTile ashTilePrefab;
    public IsTile waterTilePrefab;
    public IsTile swampTilePrefab;
    public IsTile iceTilePrefab;
    public IsTile mountainTilePrefab;
    public IsTile volcanoTilePrefab;
    public IsTile fireTilePrefab;
    public IsTile desertTilePrefab;

    public List<IsTile> waterTiles;

    public GameObject testPrefab;

    public IsTile[] extraTiles;

    public Text timer;

    /// <summary>
    /// Die maximale Stärke des Windes der grad herrscht.
    /// </summary>
    int windStrength;


    /// <summary>
    /// Basiswerte für viele Starteigenschaften
    /// </summary>
    public  int minimumWindStrength = 20;
    public  int maximumWindStrength = 150;
    public int minimumLightStrength = 10;
    public  int maximumLightStrength = 50;

    public  int minimumWaterStrength = 30;
    public  int maximumWaterStrength = 70;
    public  int minimumSwampStrength = 15;
    public  int maximumSwampStrength = 90;

    public  int minimumSwampValueStart = 1500;
    public  int maximumSwampValueStart = 3500;
    public static int minimumGroundValueStart = 25;
    public static int maximumGroundValueStart = 175;
    public  int minimumAshValueStart = 2000;
    public  int maximumAshValueStart = 4000;

    /// <summary>
    /// Die Richtung aus der der Wind weht.
    /// </summary>
    int windDirection;

    /// <summary>
    /// Die Menge an Energie die an Licht auf jedes Feld fällt.
    /// </summary>
    float lightStrength;


    public weatherType currentWeather = weatherType.NORMAL;


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
    public IsTile[] felder;

    /// <summary>
    /// Der Seed für den Zufallszahlengenerator.
    /// </summary>
    //public int seed;

    float timeHappened = 0;
    int ticksHappened = 0;

    bool isPaused = true;
    bool isFinished = false;
    bool isSetupPhase = false;
    public movementTypes spawnPattern;
    int maxNumberOfPatterns = 2;
    float tileSpeed = 0.1f;


    public enum movementTypes
    {
        MIDDLE, SLIDEIN, RANDOM
    }
    public cameraShake cS;
    public ParticleSystem snow;

    // Use this for initialization
    void Start()
    {
        cS = GameObject.Find("Main Camera").GetComponent<cameraShake>();
        snow = GameObject.Find("snowParticle").GetComponent<ParticleSystem>();




    //Test
    /*
    eventListe.Clear();
    RandomEvent neu = new RandomEvent();
    neu.type = eventTypes.ERUPTIONSTART;
    neu.time = 5;
    neu.change = Random.Range(minimumEruptionDuration, maximumEruptionDuration) + Random.Range(minimumEruptionDuration, maximumEruptionDuration);
    eventListe.Add(neu);
    neu = new RandomEvent();
    neu.type = eventTypes.WEATHERSTOP;
    neu.time = 300;
    eventListe.Add(neu);
    */
}


    public void GenerateRectangleMap()
    {
        //Speichert den Typen jedes Feld bevor es finaliesiert wird.
        tileType[,] tempField = new tileType[xSize, ySize];


        //Generiere ein Starttyp für jedes Feld.


        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float mountainChance = 8f;
                float waterChance = 10f;
                float desertChance = 15f;
                float swampChance = 0f;

                //Generiere eine zufällige Zahl zwischen 0 und 10.
                float randomChance = Random.Range(0f, 100f);

                if (randomChance < mountainChance)
                {
                    tempField[x, y] = tileType.MOUNTAIN;
                }
                else if(randomChance < mountainChance + desertChance)
                {
                    tempField[x, y] = tileType.DESERT;
                }
                else if (randomChance < mountainChance + desertChance + waterChance)
                {
                    tempField[x, y] = tileType.WATER;
                }
                else if (randomChance < mountainChance + desertChance + waterChance + swampChance)
                {
                    tempField[x, y] = tileType.SWAMP;
                }
                else
                {
                    tempField[x, y] = tileType.GROUND;
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
                    int countSwamp = 0;
                    int countDesert = 0;

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
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.DESERT)
                                countDesert++;
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.VOLCANO)
                                countVolcano++;
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.ASH)
                                countAsh++;
                            else if (tempField[x + neighbourCoords[j, 0], y + neighbourCoords[j, 1]] == tileType.SWAMP)
                                countSwamp++;
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
                        if (countDesert == 2 || countDesert == 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.15 * countDesert)
                                tempField[x, y] = tileType.DESERT;
                        }
                        //Chance sich in ein Aschefeld zu verwandeln.
                        if (countVolcano >= 1 || countAsh >= 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.5 * countVolcano + 0.1 * countAsh)
                                tempField[x, y] = tileType.ASH;
                        }

                        //Chance sich in ein Sumpffeld zu verwandeln.
                        if (countVolcano < 1 && countSwamp > 0 && countMountain < 1)
                        {
                            float chance = Random.Range(0f, 1f);
                            if (chance <= 0.05 * (countSwamp + 1))
                                tempField[x, y] = tileType.SWAMP;
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
                        /*
                        if (Random.Range(0f, 1f) <= 0.02)
                        {
                            tempField[x, y] = tileType.VOLCANO;
                        }
                        */
                    }
                }
            }
        }

        //Die ausgewürfelten Feldertypen werden jetzt in richtige Felder verwandelt.
        GenerateMapFromTileTypeArray(tempField);




        //Die Erstellung von allen Feldern ist jetzt abgeschlossen.
        //Nun folgt das setzen der weiteren Weltbedingten Resourcen wie Licht.
        windStrength = (int)Random.Range(minimumWindStrength, maximumWindStrength);
        lightStrength = (int)Random.Range(minimumLightStrength, maximumLightStrength);

        windDirection = Random.Range(0, 6);
    }



    public bool GenerateMapFromTileTypeArray(tileType[,] typeTiles)
    {
        if (typeTiles.GetLength(0) > xSize && typeTiles.GetLength(1) > ySize)
            return false;
        //Die ausgewürfelten Feldertypen werden jetzt in richtige Felder verwandelt.
        felder = new IsTile[xSize * ySize];

        movementTypes pattern = spawnPattern;
        if (pattern == movementTypes.RANDOM)
        {
            pattern = (movementTypes)Random.Range(0, maxNumberOfPatterns);
        }

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {


                Vector2 size = new Vector2(0.875f, 0.75f);
                Vector2 position2d;
                position2d = new Vector2(x - xSize / 2, y - ySize / 2);
                if (y % 2 == 1)
                    position2d.x += 0.5f;



                Vector3 targetPosition = new Vector3(this.transform.position.x + position2d.x * size.x, this.transform.position.y + position2d.y * size.y, 1);
                Vector3 position;

                switch (pattern)
                {
                    case movementTypes.MIDDLE:
                        position = this.transform.position;
                        tileSpeed = 0.1F;
                        break;
                    case movementTypes.SLIDEIN:
                        position = new Vector3(targetPosition.x + (1 + y) * 5, targetPosition.y, targetPosition.z);
                        tileSpeed = 0.35F;
                        break;
                    default:
                        position = targetPosition;
                        break;
                }

                IsTile newTile = null;
                switch (typeTiles[x, y])
                {
                    case tileType.WATER:
                        newTile = Instantiate(waterTilePrefab, position, Quaternion.identity);
                        waterTiles.Add(newTile);
                        break;
                    case tileType.SWAMP:
                        newTile = Instantiate(swampTilePrefab, position, Quaternion.identity);
                        break;
                    case tileType.MOUNTAIN:
                        newTile = Instantiate(mountainTilePrefab, position, Quaternion.identity);
                        break;
                    case tileType.VOLCANO:
                        newTile = Instantiate(volcanoTilePrefab, position, Quaternion.identity);
                        vulcanos.Add(newTile);
                        break;
                    case tileType.ASH:
                        newTile = Instantiate(ashTilePrefab, position, Quaternion.identity);
                        break;
                    case tileType.FIRE:
                        newTile = Instantiate(fireTilePrefab, position, Quaternion.identity);
                        break;
                    case tileType.ICE:
                        newTile = Instantiate(iceTilePrefab, position, Quaternion.identity);
                        break;
                    case tileType.GROUND:
                        newTile = Instantiate(groundTilePrefab, position, Quaternion.identity);
                        break;
                    case tileType.DESERT:
                        newTile = Instantiate(desertTilePrefab, position, Quaternion.identity);
                        break;
                    default:
                        if (extraTiles.Length > Mathf.Abs((int)typeTiles[x, y] + 1))
                        {
                            newTile = Instantiate(extraTiles[Mathf.Abs((int)typeTiles[x, y] + 1)], position, Quaternion.identity);
                        }
                        break;
                }
                newTile.setTarget(targetPosition);

                if (newTile != null)
                {
                    if (newTile.getHasGroundValue())
                        newTile.setNutrientValue((int)Random.Range(getMinimumNutrientValue(newTile.type), getMaximumNutrientValue(newTile.type)));
                    if (newTile.getHasWaterValue())
                        newTile.setWaterStrength((int)Random.Range(getMinimumWaterStrength(newTile.type), getMaximumWaterStrength(newTile.type)));

                    newTile.setPlayingField(this);
                }

                felder[y * xSize + x] = newTile;

            }
        }

        //Jetzt sind alle Felder erstellt, und allen Feldern können jetzt ihre Nachbarn gegeben werden.
        //Sie werden dann auch in Bewegung gesetzt.

        for (int i = 0; i < felder.Length; i++)
        {
            //
            int tempX = i % xSize;
            int tempY = (i - i % xSize) / xSize;

            IsTile[] neighbours = new IsTile[6];

            int[,] neighbourCoords;

            if (tempY % 2 == 1)
                neighbourCoords = unevenNeighbourCoords;
            else
                neighbourCoords = evenNeighbourCoords;

            for (int j = 0; j < 6; j++)
            {
                bool correctCoords = true;
                int tempX2 = tempX + neighbourCoords[j, 0];
                if (tempX2 < 0 || tempX2 >= xSize)
                    correctCoords = false;
                int tempY2 = tempY + neighbourCoords[j, 1];
                if (tempY2 < 0 || tempY2 >= ySize)
                    correctCoords = false;

                int tempListPosition = tempY2 * xSize + tempX2;

                if (tempListPosition >= 0 && tempListPosition < felder.Length && correctCoords)
                    neighbours[j] = felder[tempListPosition];
                else
                    neighbours[j] = null;
            }
            felder[i].setNeighbours(neighbours);

            felder[i].setMoving();
        }


        isSetupPhase = true;
        return true;
    }



    public void GenerateEvents(int maximaleTickAnzahl)
    {
        if (eventListe == null)
            eventListe = new List<RandomEvent>();
        int ticks = 0;

        int toCatastropheEnd = -1;
        while (ticks < maximaleTickAnzahl)
        {
            RandomEvent neu = new RandomEvent();
            //Würfel Typ aus
            int random;
            if (toCatastropheEnd <= -1)
                random = Random.Range(0, normalEventChance + catastropheEventChance);
            else
                random = 0;

            if (random >= 0 && random < normalEventChance)
            {
                int random2 = Random.Range(0, windDirectionChangeChance + windStrengthChangeChance + LightStrengthChangeChance);

                if (random2 >= 0 && random2 < windStrengthChangeChance)
                {
                    neu.type = eventTypes.WINDDIRECTION;
                    //Würfel Änderung nach links oder rechts aus
                    if (Random.Range(0, 1) == 0)
                    {
                        //Rechts
                        neu.change = 1;
                    }
                    else
                    {
                        //Links
                        neu.change = -1;
                    }
                }
                else if (random2 < windStrengthChangeChance + windStrengthChangeChance)
                {
                    neu.type = eventTypes.WINDSTRENGTH;
                    //Würfel positiven oder negativenEffekt aus
                    if (Random.Range(0, 2) == 0)
                    {
                        //Positiv
                        //Diese Methode sorgt dafür das extreme Ergebnisse, nicht so häufig auftreten.
                        neu.change = Random.Range(2, 18) + Random.Range(3, 19);
                    }
                    else
                    {
                        //Negativ
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie -5 oder -35, nicht so häufig auftreten.
                        neu.change = Random.Range(-18, -2) + Random.Range(-19, -3);
                    }
                }
                else
                {
                    neu.type = eventTypes.LIGHT;
                    //Würfel positiven oder negativenEffekt aus
                    if (Random.Range(0, 2) == 0)
                    {
                        //Positiv
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie 1 oder +15, nicht so häufig auftreten.
                        //Durchschnitt wäre 7 oder 8.
                        neu.change = Random.Range(0, 7) + Random.Range(1, 8);
                    }
                    else
                    {
                        //Negativ
                        //Diese Methode sorgt dafür das extreme Ergebnisse, wie -1 oder -15, nicht so häufig auftreten.
                        //Durchschnitt wäre -7 oder -8.
                        neu.change = Random.Range(-7, 0) + Random.Range(-8, -1);
                    }
                }
            }
            else
            {
                //Rolle für eine Katastrophe!
                int randomCatastrophe = Random.Range(0, BlizzardChance + EruptionChance);
                if (randomCatastrophe >= 0 && randomCatastrophe < BlizzardChance)
                {
                    //Blizzard
                    neu.type = eventTypes.BLIZZARDSTART;
                    neu.change = Random.Range(minimumBlizzardDuration, maximumBlizzardDuration) + Random.Range(minimumBlizzardDuration, maximumBlizzardDuration);
                    toCatastropheEnd = neu.change;
                }
                else if (randomCatastrophe < BlizzardChance + EruptionChance)
                {
                    neu.type = eventTypes.ERUPTIONSTART;
                    neu.change = Random.Range(minimumEruptionDuration, maximumEruptionDuration) + Random.Range(minimumEruptionDuration, maximumEruptionDuration);
                    toCatastropheEnd = neu.change;
                }
            }

            //Wirf eine Zeit aus
            //Jeden Tick wird die Time des obersten Events um 1 verringert, erreicht sie 0 wird das Event ausgehandelt, und aus der Liste genommen.
            //Minimalwert = 0, Maximalwert = 360 Ticks, Durschnitt ist ~180?
            //Muss noch poliert werden.
            neu.time = Random.Range(0, averageTimeBetweenEvents * 2 / 3) + Random.Range(0, averageTimeBetweenEvents * 2 / 3) + Random.Range(0, averageTimeBetweenEvents * 2 / 3);
            toCatastropheEnd -= neu.time;
            ticks += neu.time;
            eventListe.Add(neu);

            if (toCatastropheEnd < 0)
            {
                RandomEvent weatherEnd = new RandomEvent();
                weatherEnd.type = eventTypes.WEATHERSTOP;
                weatherEnd.time = 0;
                eventListe.Add(weatherEnd);
            }
        }
    }

    bool shake = true;

    float timeGame = seed.getTime();

    // Update is called once per frame
    void Update()
    {
        //Ist das Spiel pausiert, bewegt sich der Timer nicht.
        if (!isPaused)
        {
            //Setze den Timer
            /*
            timeHappened += Time.deltaTime;
            ticksHappened++;
            int seconds = Mathf.FloorToInt(timeHappened);
            timer.text = "Timer: " + string.Format("{0:00}", seconds / 60) + ":" + string.Format("{0:00}", seconds % 60);
            if (seconds == seed.getTime() / 60)
            {
                isPaused = true;
                isFinished = true;
            }
            */

            timeGame -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timeGame);
            timer.text = "Timer: " + string.Format("{0:00}", seconds / 60) + ":" + string.Format("{0:00}", seconds % 60); ;

            if(timeGame <= 0)
            {
                isPaused = true;
                isFinished = true;

                SceneManager.LoadScene("inbetween_Test");
            }


            /*
            if (eventListe[2].type == eventTypes.BLIZZARDSTART && shake)
            {
                shake = false;
                var em = snow.emission;
                em.rateOverTime = 8;

            }

            if (eventListe[2].type == eventTypes.ERUPTIONSTART && shake)
            {

                if(vulcanos.Count != 0)
                {
                    cS.setShakeTimer(2);
                    for (int i = 0; i < vulcanos.Count; i++)
                    {
                        Instantiate(erupt, new Vector3(vulcanos[i].transform.position.x, vulcanos[i].transform.position.y, 0), Quaternion.identity);

                    }
                    shake = false;
                }

            }
            
            if (eventListe[1].type == eventTypes.WEATHERSTOP && shake)
            {
                shake = false;
                var em = snow.emission;
                em.rateOverTime = 0;
            }
            */
            //Starte die Events
            if (eventListe != null && eventListe.Count > 0)
            {
                RandomEvent current = eventListe[0];
                current.time--;
                if (current.time <= 0)
                {
                    eventListe.RemoveAt(0);
                    shake = true;
                    handleEvent(current);
                }
            }
        }
        else if (felder[felder.GetLength(0) - 1].hasReachedTarget() && isSetupPhase)
        {
            isPaused = false;
            isSetupPhase = false;
        }

    }
    

    private void handleEvent(RandomEvent currentEvent)
    {
        switch (currentEvent.type)
        {
            case eventTypes.LIGHT:
                lightStrength += currentEvent.change;
                if (lightStrength < minimumLightStrength)
                    lightStrength = minimumLightStrength;
                else if (lightStrength > maximumLightStrength)
                    lightStrength = maximumLightStrength;
                break;
            case eventTypes.WINDSTRENGTH:
                windStrength += currentEvent.change;
                if (windStrength < minimumWindStrength)
                    windStrength = minimumWindStrength;
                else if (windStrength > maximumWindStrength)
                    windStrength = maximumWindStrength;

                forceWindupate();
                break;
            case eventTypes.WINDDIRECTION:
                windDirection += currentEvent.change;
                if (windDirection == -1)
                    windDirection = 5;
                else if (windDirection == 6)
                    windDirection = 0;

                forceWindupate();
                break;
            case eventTypes.BLIZZARDSTART:
                
                //currentWeather = weatherType.BLIZZARD;
                //forceWindupate();
                
                int i = Random.Range(0, waterTiles.Count);
                Instantiate(testPrefab, new Vector3(waterTiles[i].transform.position.x, waterTiles[i].transform.position.y, 0), Quaternion.identity);
                
                break;
            case eventTypes.ERUPTIONSTART:
                currentWeather = weatherType.ERUPTION;
                break;
            case eventTypes.WEATHERSTOP:
                currentWeather = weatherType.NORMAL;
                forceWindupate();
                break;
            default:
                break;
        }
    }

    public void forceWindupate()
    {
        foreach (IsTile tile in felder)
        {
            tile.forceWindUpdate();
        }
    }


    /// <summary>
    /// Ersetzt ein Feld durch ein neugeneriertes Feld eines anderen (oder dem selben) Typs.
    /// Tötet jede Pflanze die sich auf dem Feld befindet, automatisch.
    /// </summary>
    public void replaceTile(IsTile tile, tileType newType)
    {
        replaceTile(tile, newType, false, false);
    }

    public void replaceTile(IsTile tile, tileType newType, bool keepStats, bool keepPlant)
    {
        int index = findPositionOfTile(tile);
        if (index >= 0)
        {

            IsTile newTile = null;
            switch (newType)
            {
                case tileType.GROUND:
                    newTile = Instantiate(groundTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;
                case tileType.ASH:
                    newTile = Instantiate(ashTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;
                case tileType.WATER:
                    newTile = Instantiate(waterTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;
                case tileType.SWAMP:
                    newTile = Instantiate(swampTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;
                case tileType.ICE:
                    newTile = Instantiate(iceTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;
                case tileType.MOUNTAIN:
                    newTile = Instantiate(mountainTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;
                case tileType.VOLCANO:
                    newTile = Instantiate(volcanoTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    vulcanos.Add(newTile);
                    break;
                case tileType.FIRE:
                    newTile = Instantiate(fireTilePrefab, felder[index].getTransform().position, Quaternion.identity);
                    break;

                default:
                    if (extraTiles.Length > Mathf.Abs((int)newType + 1))
                    {
                        newTile = Instantiate(extraTiles[Mathf.Abs((int)newType + 1)], felder[index].getTransform().position, Quaternion.identity);
                    }
                    return;
            }



            if (newTile != null)
            {
                if (keepStats)
                    newTile.setNutrientValue(felder[index].getNutrientValue(true));
                else if (newTile.getHasGroundValue())
                    newTile.setNutrientValue(Random.Range(getMinimumNutrientValue(newType), getMaximumNutrientValue(newType)));

                if (keepStats)
                    newTile.setWaterStrength(felder[index].getWaterStrength(true));
                else if (newTile.getHasWaterValue())
                    newTile.setWaterStrength(Random.Range(getMinimumWaterStrength(newType), getMaximumWaterStrength(newType)));

                if (keepPlant && newTile.getCanSustainPlant() && felder[index].getPlant() != null)
                {
                    newTile.SetPlant(felder[index].getPlant());
                }
                else if (felder[index].getPlant() != null)
                {
                    GameObject.Destroy(felder[index].getPlant().gameObject);
                }

                newTile.setPlayingField(this);
                newTile.setNeighbours(felder[index].getNeighbours());
                for (int i = 0; i < 6; i++)
                {
                    if (newTile.getNeighbours()[i] != null)
                        newTile.getNeighbours()[i].replaceNeighbour(felder[index], newTile);
                }
                felder[index].removeObject();
                felder[index] = newTile;
            }
        }
    }

    private int getMinimumWaterStrength(tileType type)
    {
        switch (type)
        {
            case tileType.WATER:
                return minimumWaterStrength;
            case tileType.SWAMP:
                return minimumSwampStrength;
            default:
                return 0;
        }
    }

    private int getMaximumWaterStrength(tileType type)
    {
        switch (type)
        {
            case tileType.WATER:
                return maximumWaterStrength;
            case tileType.SWAMP:
                return maximumSwampStrength;
            default:
                return 0;
        }
    }

    private int getMinimumNutrientValue(tileType type)
    {
        switch (type)
        {
            case tileType.GROUND:
                return minimumGroundValueStart;
            case tileType.ASH:
                return minimumAshValueStart;
            case tileType.SWAMP:
                return minimumSwampValueStart;
            default:
                return 0;
        }
    }

    private int getMaximumNutrientValue(tileType type)
    {
        switch (type)
        {
            case tileType.GROUND:
                return maximumGroundValueStart;
            case tileType.ASH:
                return maximumAshValueStart;
            case tileType.SWAMP:
                return maximumSwampValueStart;
            default:
                return 0;
        }
    }



    int findPositionOfTile(IsTile tile)
    {
        for (int i = 0; i < felder.Length; i++)
        {
            if (felder[i] == tile)
                return i;
        }
        return -1;
    }


    public float getLightStrength()
    {
        switch (currentWeather)
        {
            case (weatherType.BLIZZARD):
                return lightStrength / 3;
            default:
                return lightStrength;
        }

    }

    public int getWindDirection()
    {
        return windDirection;
    }

    public int getWindStrength()
    {
        switch (currentWeather)
        {
            case (weatherType.BLIZZARD):
                return Mathf.FloorToInt(windStrength * 1.5f);
            default:
                return windStrength;
        }
    }


    public void setLightStrength(int lightStrength)
    {
        this.lightStrength = lightStrength;
    }
    public void setWindStrength(int windStrength)
    {
        this.windStrength = windStrength;
    }
    public void setWindDirection(int windDirection)
    {
        this.windDirection = windDirection;
    }

    public List<RandomEvent> getEventList()
    {
        if (eventListe == null)
            eventListe = new List<RandomEvent>();
        return eventListe;
    }

    public bool getPaused()
    {
        return isPaused;
    }

    public void setPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public float getSpeed()
    {
        return tileSpeed;
    }
    public bool getIsFinished()
    {
        return isFinished;
    }

    public IsTile[] getFelder()
    {
        return felder;
        
    }

}
