using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inbetweenMenuControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("D Pad Y") <= -0.5 && gameObject.transform.position.y == -1)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -2, 1);
        }
        else if (Input.GetAxis("D Pad Y") >= 0.5 && gameObject.transform.position.y == -2)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1, 1);
        }

        if (Input.GetButtonDown("A") && gameObject.transform.position.y == -1)
        {
            int s = Random.Range(0, 900000);

            seed.setSeedField(s);
            seed.setAutoStart(true);
            SceneManager.LoadScene("Multiplayer_Test");
        }

        if (Input.GetButtonDown("A") && gameObject.transform.position.y == -2)
        {
            SceneManager.LoadScene("menu_Test");
        }

    }
}
