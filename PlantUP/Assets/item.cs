using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour {

    public int type;
    public float timestamp;

	// Use this for initialization
	void Start () {

        type = Random.Range(0, 3);

        if(type == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.0f, 1.0f, 1.0f);
        }
        else if( type == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 1.0f, 1.0f);
        }
        else if (type == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.5f, 1.0f);
        }
        else if (type == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1.0f, 1.0f);
        }

        timestamp = Time.time + 5.0f;

    }
	
	// Update is called once per frame
	void Update () {

        if(timestamp != 0.0f && timestamp < Time.time)
        {
            Destroy(gameObject);
        }
		
	}

    public int getType()
    {
        return type;
    }
}
