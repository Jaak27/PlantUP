using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seed : MonoBehaviour
{

    // klasse welche den Seed enthält

    static int seedField = 0;
    static int xSize = 0;
    static int ySize = 0;
    //Ist autoStart false, wird ein ChallengeLevel geladen
    static bool autoStart = true;
    //Gibt an welches Challengelevel gwünscht ist.
    static int challenge = 0;
    //Wie lange soll das Spiel gehen?
    //36000 ~ 10 Minuten
    static int time = 180;

    public static bool getAutoStart()
    {
        return autoStart;
    }
    public static void setAutoStart(bool aStart)
    {
        autoStart = aStart;
    }

    public static int getTime()
    {
        return time;
    }

    public static int getChallenge()
    {
        return challenge;
    }
    public static void setChallenge(int challenge)
    {
        seed.challenge = challenge;
    }

    public static int getSeedField()
    {
        return seedField;
    }

    public static void setSeedField(int s)
    {
        seedField = s;
    }

}
