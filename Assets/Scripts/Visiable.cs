using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visiable : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // gameObjects sichtbar schalten
	public void setVisible()
	{
        gameObject.SetActive(true);
	}

    // gameObjects unsichtbar schalten
    public void setInvisible()
    {
        gameObject.SetActive(false);
    }
}
