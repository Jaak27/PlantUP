using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCursor : MonoBehaviour {

    // Use this for initialization
    public selectedObject so;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("collision Enter");
        if(collision.gameObject.GetComponent<IsTile>() != null)
        {
            so.setTile(collision.gameObject.GetComponent<IsTile>());
            print("tileSelect");
        }

        

    }
}
