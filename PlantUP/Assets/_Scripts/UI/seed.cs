using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seed : MonoBehaviour {

    // klasse welche den Seed enthält

    static int seedField = 0;

    public static int getSeedField()
    {
        return seedField;
    }

    public static void setSeedField(int s)
    {
        seedField = s;
    }

}
