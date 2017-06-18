using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeMaps : MonoBehaviour
{
    


	public static void getChallenge(int challenge, PlayingFieldLogic logic)
    {
        switch (challenge)
        {
            case 0:
                getChallengeTest(logic);
                break;

        }
    }


    private static void getChallengeTest(PlayingFieldLogic logic)
    {
        tileType[,] test = {
            {tileType.WATER,    tileType.WATER , tileType.GROUND, tileType.GROUND, tileType.ASH,     tileType.VOLCANO, tileType.ASH,    tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.WATER },
            {tileType.WATER,    tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND,  tileType.ASH,     tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.WATER },
            {tileType.GROUND,   tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND,  tileType.GROUND,  tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND },
            {tileType.MOUNTAIN, tileType.GROUND, tileType.GROUND, tileType.WATER,  tileType.WATER,   tileType.SWAMP,   tileType.WATER,  tileType.WATER,  tileType.GROUND, tileType.GROUND, tileType.MOUNTAIN },
            {tileType.MOUNTAIN, tileType.GROUND, tileType.GROUND, tileType.WATER,  tileType.WATER,   tileType.SWAMP,   tileType.WATER,  tileType.WATER,  tileType.GROUND, tileType.GROUND, tileType.MOUNTAIN },
            {tileType.GROUND,   tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND,  tileType.GROUND,  tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND },
            {tileType.WATER,    tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.ASH,     tileType.GROUND,  tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.WATER },
            {tileType.WATER,    tileType.WATER,  tileType.GROUND, tileType.ASH,    tileType.VOLCANO, tileType.ASH,     tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.WATER },
        };
        //Die Map wird nun generiert
        logic.GenerateMapFromTileTypeArray(test);
        logic.setLightStrength(30);
        logic.setWindStrength(120);
        logic.setWindDirection(0);
        
        //Setze neue Eventliste
        List<PlayingFieldLogic.RandomEvent> eventList = logic.getEventList();
        {
            PlayingFieldLogic.RandomEvent newEvent = new PlayingFieldLogic.RandomEvent();
            newEvent.time = 600;
            newEvent.type = PlayingFieldLogic.eventTypes.ERUPTIONSTART;
            eventList.Add(newEvent);
        }
        {
            PlayingFieldLogic.RandomEvent newEvent = new PlayingFieldLogic.RandomEvent();
            newEvent.time = 240;
            newEvent.type = PlayingFieldLogic.eventTypes.WEATHERSTOP;
            eventList.Add(newEvent);
        }
        logic.GenerateEvents(36000);
    }

    /// <summary>
    /// Stringform = "tileTypeAlsInt;tileTypeAlsInt;...."
    /// </summary>
    /// <param name="input">Der inputString aus dem ein Feld generiert wird</param>
    /// <returns>Das generierte Feld basierend auf dem Inputstring</returns>

    public static tileType[,] stringToTileTypeArray(string[] input, int xSize, int ySize)
    {
        tileType[,] tiles = new tileType[xSize, ySize];

        for (int y = 0; y < ySize; y++)
        {
            string[] line = input[y].Split(';');
            for (int x = 0; x < xSize; x++) 
            {
                int tile = 0;
                bool success = int.TryParse(line[x],out tile);
                if (success)
                    tiles[x, y] = (tileType)tile;
                else
                    tiles[x, y] = tileType.GROUND;
            }
        }


        return null;
    }
}
