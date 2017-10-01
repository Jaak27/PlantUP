using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class txtResult : MonoBehaviour {

    public PlayerPrototype player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Text>().text =  "Energy collected: " + player.GetPoints() +
                                                "Plant Count: " + player.GetPlantCount() +
                                                "Blueprint Count: " + player.blueprints.Count;
		
	}
}
