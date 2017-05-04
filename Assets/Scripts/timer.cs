using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    // Timer zum testen

    private float time;
    private int minutes;
    private float seconds;
    private Text textTimer;

	// Use this for initialization
	void Start () {
        time = 200;
        textTimer = GetComponent<Text>();

		
	}
	
	// Update is called once per frame
	void Update () {

        // timer updaten und darstellen
        time -= Time.deltaTime;
        minutes = (int)time / 60;
        seconds = (int)time % 60;
        textTimer.text = "Time: " + minutes + ":" + seconds; 
        if(time <= 0)
        {
            textTimer.gameObject.SetActive(false);
        }
		
	}
}
