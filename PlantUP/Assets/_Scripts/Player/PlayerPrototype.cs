using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPrototype : MonoBehaviour
{
    /// <summary>
    /// Die Angesammelte Punktzahl des Spielers
    /// </summary>
    public float points = 0;
    public List<Blueprint> blueprints;

    public List<Plant> plants;
    //private string name;
    public Text uiText;
    public static int playerCount = 0;
    public int myNum;
    public int plantCount = 0;

    public int costDivide;

    int multiplier;

    //Blueprints des Players
    public Blueprint blueprint0;
    public Blueprint blueprint1;
    public Blueprint blueprint2;
    public Blueprint blueprint3;

    void Start()
    {
        InvokeRepeating("cost", 0, 5f);
        multiplier = 1;
        costDivide = 1;
    }

    private void Awake()
    {
        myNum = ++playerCount;

    }

    private void Update()
    {
        UpdateUIText();
        if(points <= 0)
        {
            SceneManager.LoadScene("menu_Test");
        }
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

    public void removePlant()
    {
        plantCount--;
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

        int reserve = 0;

        for(int i = 0; i < plants.Count; i++)
        {
            reserve = reserve + (int) plants[i].GetStats()[8].GetCurrent();
        }
        uiText.text = "Player" + playerCount + ": " + points + " Running Costs: " + (int)((blueprint0.GetCost() * blueprint0.getPlants() + blueprint1.GetCost() * blueprint1.getPlants() + blueprint2.GetCost() * blueprint2.getPlants() + blueprint3.GetCost() * blueprint3.getPlants())/costDivide) + " reserve: " + reserve * multiplier;

    }


    private void cost()
    {
        points = points -((blueprint0.GetCost() * blueprint0.getPlants() + blueprint1.GetCost() * blueprint1.getPlants() + blueprint2.GetCost() * blueprint2.getPlants() + blueprint3.GetCost() * blueprint3.getPlants())/costDivide);
    }

    public int UpgradeCost()
    {
        int cost = 1000 + (blueprint0.getUpgradeCount() + blueprint1.getUpgradeCount() + blueprint2.getUpgradeCount() + blueprint3.getUpgradeCount()) * 1000;
        return cost;
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


    public void setMultiplier(int m)
    {
        multiplier = m;
    }

    public int getMultiplier()
    {
        return multiplier;
    }

    public void setCostDevide(int m)
    {
        costDivide = m;
    }

    public int getCostDevide()
    {
        return costDivide;
    }

}
