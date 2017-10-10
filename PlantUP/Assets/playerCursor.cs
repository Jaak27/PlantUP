using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCursor : MonoBehaviour {

    // Use this for initialization
    public selectedObject so;
    public PlayerPrototype player;
    public float timestamp;
    bool special;

	void Start () {
        special = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(special && timestamp < Time.time)
        {
            player.setMultiplier(1);
            player.setCostDevide(1);
            special = false;
        }
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("collision Enter");
        if(collision.gameObject.GetComponent<IsTile>() != null)
        {
            so.setTile(collision.gameObject.GetComponent<IsTile>());
            print("tileSelect");
        }
        if (collision.gameObject.tag == "Item")
        {
            print("ITEM");
            item it = collision.gameObject.GetComponent<item>();

            int type = it.getType();

            if (type == 0)
            {
                player.setMultiplier(2);
                timestamp = Time.time+15;
                special = true;
            }
            else if (type == 1)
            {
                player.AddPoints(5000);
            }
            else if (type == 2)
            {
                player.setCostDevide(2);
                timestamp = Time.time+15;
                special = true;
            }
            else if (type == 3)
            {
                player.setCostDevide(2);
                timestamp = Time.time;
                special = true;
            }

            Destroy(collision.gameObject);
        }



    }
}
