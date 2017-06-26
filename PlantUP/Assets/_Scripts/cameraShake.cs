using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {

    public float shakeTimer;
    public float shakePower;

	// Use this for initialization
	void Start () {
		
	}

    
	
	// Update is called once per frame
	void Update () {

        if(shakeTimer >= 0)
        {
            Vector2 shakepos = Random.insideUnitCircle * shakePower;
            transform.position = new Vector3(0 + shakepos.x,0+ shakepos.y, -10);
            shakeTimer -= Time.deltaTime;
        }

		
	}

    public void setShakeTimer(float time)
    {
        shakeTimer = time;
    }
}
