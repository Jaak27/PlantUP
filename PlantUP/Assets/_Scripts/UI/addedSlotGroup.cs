using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addedSlotGroup : MonoBehaviour {

    public Blueprint mybp;
    public List<addedSlot> elements;

    public void setMyBp(Blueprint bp)
    {
        mybp = bp;
    }

    public Blueprint getMyBp()
    {
        return mybp;
    }

    private void Update()
    {
        if(mybp == GameObject.Find("bpSelectHandlerChange").GetComponent<selectedBP>().getBlueprint() | mybp == GameObject.Find("bpSelectHandler").GetComponent<selectedBP>().getBlueprint())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
