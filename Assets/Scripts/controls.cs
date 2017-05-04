using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controls : MonoBehaviour {

    private Transform cameraControl;


    // Use this for initialization
    void Start () {

        cameraControl = GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        // Dient zur Bewegung der Camera mit den Pfeiltasten
        if(Input.GetKey(KeyCode.RightArrow))
        {
            cameraControl.Translate(new Vector3(0.25f, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraControl.Translate(new Vector3(-0.25f, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraControl.Translate(new Vector3(0, -0.25f, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraControl.Translate(new Vector3(0, 0.25f, 0));
        }
        

    }
}
