using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrototype : MonoBehaviour {

    /// <summary>
    /// Die Angesammelte Punktzahl des Spielers
    /// </summary>
    public float points = 0;
    //private string name;
    private Text uiText;
    private static int count = 0;

    private void Awake()
    {
        uiText = GameObject.Find("txt_Energie").GetComponent<Text>();
        count++;
    }

    public void AddPoints(float value)
    {
        points += value;
        uiText.text = "Player"+count + ": " + points;
    }

    public float GetPoints() {
        return points;
    }

}
