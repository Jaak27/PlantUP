using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedBP : MonoBehaviour {

    public Blueprint blueprintSelect;

    public Blueprint blueprintPrefab;

    public Blueprint blueprint0;
    public Blueprint blueprint1;
    public Blueprint blueprint2;
    public Blueprint blueprint3;

    public PlayerPrototype player;


    void Start()
    {
        blueprint0 = Instantiate(blueprintPrefab);
        blueprint0.s = Resources.Load<Sprite>("BaseFlower1");
        player.setBlueprint0(blueprint0);

        blueprint1 = Instantiate(blueprintPrefab);
        blueprint1.s = Resources.Load<Sprite>("BaseFlower2");
        player.setBlueprint1(blueprint1);

        blueprint2 = Instantiate(blueprintPrefab);
        blueprint2.s = Resources.Load<Sprite>("BaseFlower3");
        player.setBlueprint2(blueprint2);

        blueprint3 = Instantiate(blueprintPrefab);
        blueprint3.s = Resources.Load<Sprite>("BaseFlower4");
        player.setBlueprint3(blueprint3);
    }


    public void setBlueprintSelect(Blueprint bp)
    {
        blueprintSelect = bp;
    }

    public Blueprint getBlueprintSelect()
    {
        return blueprintSelect;
    }

    public void setBlueprint0(Blueprint bp)
    {
        blueprint0 = bp;
    }

    public Blueprint getBlueprint0()
    {
        return blueprint0;
    }

    public void setBlueprint1(Blueprint bp)
    {
        blueprint1 = bp;
    }

    public Blueprint getBlueprint1()
    {
        return blueprint1;
    }

    public void setBlueprint2(Blueprint bp)
    {
        blueprint2 = bp;
    }

    public Blueprint getBlueprint2()
    {
        return blueprint2;
    }

    public void setBlueprint3(Blueprint bp)
    {
        blueprint3 = bp;
    }

    public Blueprint getBlueprint3()
    {
        return blueprint3;
    }
}
