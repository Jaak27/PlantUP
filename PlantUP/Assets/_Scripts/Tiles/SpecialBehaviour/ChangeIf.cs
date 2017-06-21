using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIf : SpecialBehaviour
{
    /// <summary>
    /// Tötet der Wandel die Pflane? (Keinen Effekt wenn das resultierene Feld keine Pflanze halten kann
    /// </summary>
    public bool destructive;
    /// <summary>
    /// Ändert der Wandel die Wasser- und Bodenwerte? (Keinen Effekt wenn das resultierene Feld nicht die selben Werte hat
    /// </summary>
    public bool refreshing;
    // Wie hoch ist die Wechsel chance? (1 in x)
    public int changeChance;
    /// <summary>
    /// Wieviele Ticks sollen übersprungen werden?
    /// </summary>
    public int skipTicks;
    private int skippedTicks = 0;

    public List<AbstractIf> conditions;

    /// <summary>
    /// In welches Feld verwandelt es sich?
    /// </summary>
    public tileType targetTile;


	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!this.GetComponent<IsTile>().getPlayingField().getPaused())
        {
            if (skippedTicks <= 0)
            {
                skippedTicks = skipTicks;

                bool conditionsFullfilled = true;
                foreach (AbstractIf test in conditions)
                {
                    if (!test.conditionFulfilled(this.GetComponent<IsTile>()))
                        conditionsFullfilled = false;
                }


                if (conditionsFullfilled)
                {
                    if (Random.Range(0, changeChance) == 0)
                    {
                        this.GetComponent<IsTile>().getPlayingField().replaceTile(this.GetComponent<IsTile>(), targetTile, !refreshing, !destructive);
                    }

                }
            }
            skippedTicks--;
        }
        
    }
}
