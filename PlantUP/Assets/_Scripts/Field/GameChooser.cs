using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameChooser : MonoBehaviour
{
    public PlayingFieldLogic baseRules;

    public List<PlayingFieldLogic> Challenges;


    public Text timer;
	// Use this for initialization
	void Start ()
    {
        Random.InitState(seed.getSeedField());
        //seed.setAutoStart(false);
        if (seed.getAutoStart())
        {

            PlayingFieldLogic logic = Instantiate<PlayingFieldLogic>(baseRules);
            logic.timer = timer;
            logic.GenerateRectangleMap();
            logic.GenerateEvents(36000);
        }
        else
        {
            if (seed.getChallenge() >= 0 && seed.getChallenge() < Challenges.Count)
            {
                PlayingFieldLogic logic  = Instantiate<PlayingFieldLogic>(Challenges[seed.getChallenge()]);
                logic.timer = timer;
                getChallenge(seed.getChallenge(), logic);
            }
            else
            {
                PlayingFieldLogic logic =  Instantiate<PlayingFieldLogic>(baseRules);
                logic.timer = timer;
                logic.GenerateRectangleMap();
                logic.GenerateEvents(seed.getTime());
            }
        }
	}


    private void getChallenge(int challenge, PlayingFieldLogic logic)
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
            {tileType.WATER,    tileType.WATER , tileType.GROUND, tileType.GROUND, tileType.MOUNTAIN, tileType.VOLCANO, tileType.MOUNTAIN, tileType.GROUND, tileType.GROUND, tileType.WATER, tileType.WATER },
            {tileType.WATER,    tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND,   tileType.ASH,     tileType.FIRE,     tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.WATER },
            {tileType.GROUND,   tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND,   tileType.ASH,     tileType.FIRE,     tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND },
            {tileType.MOUNTAIN, tileType.GROUND, tileType.GROUND, tileType.WATER,  tileType.WATER,    tileType.SWAMP,   tileType.WATER,    tileType.WATER,  tileType.GROUND, tileType.GROUND, tileType.MOUNTAIN },
            {tileType.MOUNTAIN, tileType.GROUND, tileType.GROUND, tileType.WATER,  tileType.WATER,    tileType.SWAMP,   tileType.WATER,    tileType.WATER,  tileType.GROUND, tileType.GROUND, tileType.MOUNTAIN },
            {tileType.GROUND,   tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.FIRE,     tileType.ASH,     tileType.GROUND,   tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.GROUND },
            {tileType.WATER,    tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.FIRE,     tileType.ASH,     tileType.GROUND,   tileType.GROUND, tileType.GROUND, tileType.GROUND, tileType.WATER },
            {tileType.WATER,    tileType.WATER,  tileType.GROUND, tileType.GROUND, tileType.MOUNTAIN, tileType.VOLCANO, tileType.MOUNTAIN,   tileType.GROUND, tileType.GROUND, tileType.WATER, tileType.WATER },
        };

        //Die Map wird nun generiert
        logic.GenerateMapFromTileTypeArray(test);
        logic.setLightStrength(30);
        logic.setWindStrength(120);
        logic.setWindDirection(0);
        
        //Fülle mit Events
        logic.GenerateEvents(seed.getTime());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
