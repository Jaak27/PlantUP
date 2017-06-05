using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBlueprint : MonoBehaviour {

    public List<int> UpgradeSequence;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<int> getSequence() {
        return UpgradeSequence;
    }
}
