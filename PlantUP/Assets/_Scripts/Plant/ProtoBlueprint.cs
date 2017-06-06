using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBlueprint : MonoBehaviour {

    public List<int> UpgradeSequence;
    public bool hasChanged = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<int> GetSequence() {
        return UpgradeSequence;
    }

    public bool HasChanged() {
        return hasChanged;
    }

    public void ChangeNoticed() {
        hasChanged = false;
    }
}
