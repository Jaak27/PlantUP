using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherInEffect : AbstractIf {

    /// <summary>
    /// Nach welchem Effekt gesucht werden soll.
    /// </summary>
    public weatherType weather;
    /// <summary>
    /// Soll er aktiv oder inaktiv sein?
    /// </summary>
    public bool checkForActive;

    public override bool conditionFulfilled(IsTile tile)
    {
        if ((tile.getPlayingField().currentWeather == weather) == checkForActive)
        {
            return true;
        }
        else
            return false;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
