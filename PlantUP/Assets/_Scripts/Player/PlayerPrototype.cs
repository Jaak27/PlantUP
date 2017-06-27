using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrototype : MonoBehaviour {

    /// <summary>
    /// Die Angesammelte Punktzahl des Spielers
    /// </summary>
    public float points = 0;
    public List<Blueprint> blueprints;
    //private string name;
    public Text uiText;
    public static int playerCount = 0;
    public int myNum;
    private int plantCount = 0;
    public List<Plant> plants = new List<Plant>();

    private void Awake()
    {
        myNum = ++playerCount;
        UpdateUIText();
    }

    public void AddPoints(float value)
    {
        points += value;

        UpdateUIText();
        
    }

    public void AddPlant(Plant plant)
    {
        plants.Add(plant);
        plantCount++;
    }

    public float GetPoints() {
        return points;
    }

    public int GetPlantCount()
    {
        return plantCount;
    }

    public int GetPlantWithBlueprintCount(Blueprint bp)
    {
        int count = 0;
        foreach (Plant plant in plants)
        {
            if (plant.GetBlueprint() == bp)
            {
                ++count;
            }
        }
        return count;
    }

    public int GetPlayerNum()
    {
        return myNum;
    }

    private void UpdateUIText()
    {
        uiText.text = "Player" + playerCount + ": " + points;
    }
}
