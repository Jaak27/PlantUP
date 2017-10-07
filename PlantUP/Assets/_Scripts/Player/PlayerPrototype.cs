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


    //Blueprints des Players
    public Blueprint blueprint0;
    public Blueprint blueprint1;
    public Blueprint blueprint2;
    public Blueprint blueprint3;

    void Start()
    {
        InvokeRepeating("cost", 0, 1f);
    }

    private void Awake()
    {
        myNum = ++playerCount;
        
    }

    private void Update()
    {
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




    private void cost()
    {
        print("cost");
        points -= blueprint0.GetCost() * blueprint0.getPlants() + blueprint1.GetCost() * blueprint1.getPlants() + blueprint2.GetCost() * blueprint2.getPlants() + blueprint3.GetCost() * blueprint3.getPlants();
    }

    public void setBlueprint0(Blueprint bp)
    {
        blueprint0 = bp;
    }

    public Blueprint getBlueprint0()
    {
        return blueprint0;
    }

    public void setBlueprint1(Blueprint bp)
    {
        blueprint1 = bp;
    }

    public Blueprint getBlueprint1()
    {
        return blueprint1;
    }

    public void setBlueprint2(Blueprint bp)
    {
        blueprint2 = bp;
    }

    public Blueprint getBlueprint2()
    {
        return blueprint2;
    }

    public void setBlueprint3(Blueprint bp)
    {
        blueprint3 = bp;
    }

    public Blueprint getBlueprint3()
    {
        return blueprint3;
    }

}
