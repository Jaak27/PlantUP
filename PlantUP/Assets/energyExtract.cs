using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyExtract : MonoBehaviour {

    public GameObject pos;
    bool used;
    bool triggered;

    public PlayerPrototype player;

	// Use this for initialization
	void Start () {
        used = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = pos.transform.position;
		
	}


    public void setUsed(bool b)
    {
        used = b;
    }

    public bool getUsed()
    {
        return used;
    }
    

    void OnTriggerStay2D(Collider2D other)
    {
        //print("EX ENTER");
        if(other.tag == "Tile" && other.GetComponent<IsTile>().getPlant() != null && used == true)
        {
            print("GET IT ALL");
            player.AddPoints(other.GetComponent<IsTile>().getPlant().GetStats()[8].GetCurrent() * player.getMultiplier());
            other.GetComponent<IsTile>().getPlant().GetStats()[8].SetCurrent(0);
        }
    }

}
