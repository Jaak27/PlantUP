using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adjacendTo : AbstractIf {

    /// <summary>
    /// Nach welchem Feld gesucht werden soll.
    /// </summary>
    public tileType type;
    public int number;

    /// <summary>
    /// Soll es mehr oder weniger geben?
    /// </summary>
    public checkFor checkForRelation;

    public enum checkFor
    {
        MORE,LESS,EQUAL,NOTEQUAL
    }

    public override bool conditionFulfilled(IsTile tile)
    {
        int counted = 0;
        IsTile[] neighbours = tile.getNeighbours();
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null && neighbours[i].getTileType() == type)
                counted++;
        }
        bool fulfilled = false;

        switch (checkForRelation)
        {
            case checkFor.EQUAL:
                fulfilled = counted == number;
                break;
            case checkFor.MORE:
                fulfilled = counted > number;
                break;
            case checkFor.LESS:
                fulfilled = counted < number;
                break;
            case checkFor.NOTEQUAL:
                fulfilled = counted != number;
                break;
        }

        return fulfilled;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
