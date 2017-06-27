using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIf : AbstractIf {

    public int time = 0;
    public int maxTime;
    public List<AbstractIf> conditions = new List<AbstractIf>();

    public override bool conditionFulfilled(IsTile tile)
    {
        if (time >= maxTime)
            return true;
        return false;
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool conditionsFullfilled = true;
        if (!this.GetComponent<IsTile>().getPlayingField().getPaused())
        {
            if (conditions != null)
            {
                foreach (AbstractIf test in conditions)
                {
                    if (!test.conditionFulfilled(this.GetComponent<IsTile>()))
                        conditionsFullfilled = false;
                }
            }


            if (conditionsFullfilled && maxTime > time)
            {
                time++;
            }
        }
        
        
    }
}
