using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrototype : MonoBehaviour
{ 
    /// <summary>
    /// Die Angesammelte Punktzahl des Spielers
    /// </summary>
    public float points = 0;
    public List<Blueprint> blueprints;
    //private string name;
    public Text uiText;
    public static int playerCount = 0;
    public int myNum;
    public int plantCount = 0;

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

    public float GetPoints()
    {
        return points;
    }

    public void AddPlant()
    {
        plantCount++;
    }
    public int GetPlantCount()
    {
        return plantCount;
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
