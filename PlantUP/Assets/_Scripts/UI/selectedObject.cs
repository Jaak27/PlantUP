﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class selectedObject : MonoBehaviour
{

    public IsTile tileSelect;

    public Text text_Feld; // Text welcher die Feldinfos darstellt
    public Text text_Pflanze;
    public feldInfoUI fenster_FeldInfo;
    public feldInfoUI fenster_PflanzenInfo;
    feldInfoUI fenster_UpgradeInfo;

    public selectedBP blueprintSelect;

    public GameObject plantText;
    public GameObject createPlantPanel;

    bool moved;
    bool movedtwo;
    bool menu;
    bool pressDpad;

    public RectTransform bpIcon0;
    public RectTransform bpIcon1;
    public RectTransform bpIcon2;
    public RectTransform bpIcon3;

    public RectTransform menuCursor;

    public Plant plant;

    public PlayerPrototype player;

    public bool playerSelect;

    public GameObject feldselect;

    // Use this for initialization
    void Start()
    {


        //fenster_UpgradeInfo = GameObject.Find("fenster_UpgradeInfo").GetComponent<feldInfoUI>();
        moved = false;
        movedtwo = false;
        menu = false;
        pressDpad = false;

        menuCursor.transform.position = bpIcon0.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //print("AD");
        //Player 1
        if(!playerSelect)
        {
            // Blueprint wählen um Pflanze zu erstellen
            if(Input.GetAxis("Right Trigger") == 0.0f)
            {
                if(Input.GetButtonUp("A") && menu == false)
                {
                    menu = true;
                }
                else if(Input.GetButtonUp("A") && menu == true)
                {
                    menu = false;
                }

                if (menu == true)
                {
                    menuCursor.gameObject.SetActive(true);

                    if (Input.GetAxis("Right Stick X") >= 0.5 && menuCursor.transform.position == bpIcon0.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Right Stick X") >= 0.5 && menuCursor.transform.position == bpIcon1.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        movedtwo = true;
                    }
                    if (Input.GetAxis("Right Stick X") <= -0.5 && menuCursor.transform.position == bpIcon1.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon0.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Right Stick X") >= 0.5 && menuCursor.transform.position == bpIcon2.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon3.transform.position;
                        movedtwo = true;
                    }
                    if (Input.GetAxis("Right Stick X") <= -0.5 && menuCursor.transform.position == bpIcon2.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Right Stick X") <= -0.5 && menuCursor.transform.position == bpIcon3.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Right Stick X") == 0.0)
                    {
                        movedtwo = false;
                    }



                    if (Input.GetButtonDown("A") && menuCursor.transform.position == bpIcon0.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint0());
                    }

                    if (Input.GetButtonDown("A") && menuCursor.transform.position == bpIcon1.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint1());
                    }

                    if (Input.GetButtonDown("A") && menuCursor.transform.position == bpIcon2.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint2());
                    }

                    if (Input.GetButtonDown("A") && menuCursor.transform.position == bpIcon3.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint3());
                    }

                    if (Input.GetButtonDown("B"))
                    {
                        menu = false;
                        menuCursor.transform.position = bpIcon0.transform.position;
                    }
                }

            }

            // Tile auswahl
            if(menu == false && Input.GetAxis("Right Trigger") == 0.0)
            {
                menuCursor.gameObject.SetActive(false);

                if (Input.GetAxis("Right Stick X") >= 0.5 && Input.GetAxis("Right Stick Y") <= -0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[3];
                    moved = true;
                }

                if (Input.GetAxis("Right Stick X") <= -0.5 && Input.GetAxis("Right Stick Y") <= -0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[4];
                    moved = true;
                }

                if (Input.GetAxis("Right Stick X") <= -0.5 && Input.GetAxis("Right Stick Y") == 0.0 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[5];
                    moved = true;
                }

                if (Input.GetAxis("Right Stick X") <= -0.5 && Input.GetAxis("Right Stick Y") >= 0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[0];
                    moved = true;
                }

                if (Input.GetAxis("Right Stick X") >= 0.5 && Input.GetAxis("Right Stick Y") >= 0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[1];
                    moved = true;
                }

                if (Input.GetAxis("Right Stick X") >= 0.5 && Input.GetAxis("Right Stick Y") == 0.0 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[2];
                    moved = true;
                }

                if (Input.GetAxis("Right Stick X") == 0.0 && Input.GetAxis("Right Stick Y") == 0.0)
                {
                    moved = false;
                }

                if (Input.GetButtonDown("X"))
                {
                    player.AddPoints(tileSelect.getPlant().GetStats()[8].GetCurrent());
                    tileSelect.getPlant().GetStats()[8].SetCurrent(0);
                }
            }



            if (tileSelect != null)
            {
                feldselect.transform.position = new Vector3(tileSelect.transform.position.x, tileSelect.transform.position.y, 1);
                feldselect.GetComponent<SpriteRenderer>().enabled = true;

                fenster_FeldInfo.setUp(true);
                // Welche Infos angezeigt werden sollen abhängig von den Tiles

                if (tileSelect.GetComponent<IsTile>() != null)
                {
                    IsTile tile = tileSelect.GetComponent<IsTile>();



                    //Jedes Feld hat 3 booleans die Aussagen ob es 1. Naerstoffe hat, 2. Wasserkraft hat, und 3. Eine Pflanze haben kann
                    text_Feld.text = "-Feldata-";
                    if (tile.getHasGroundValue())
                        text_Feld.text += "\nNaehrstoffe........" + tile.getNutrientValue();
                    if (tile.getHasWaterValue())
                        text_Feld.text += "\nWasserwert........." + tile.getWaterStrength();
                    //Jedes Feld hat Licht und Wind
                    text_Feld.text += "\nWindstaerke........" + tile.getWindStrength() +
                                          "\nLichtintensitaet..." + tile.getLightValue();

                    if (tile.getCanSustainPlant())
                    {
                        fenster_PflanzenInfo.setUp(true);

                        if(tile.getPlant() != null)
                        {

                            createPlantPanel.SetActive(false);
                            plantText.SetActive(true);
                            text_Pflanze.text =
                                             "Health........" + tile.getPlant().GetStats()[2].GetCurrent() +
                                             "\n" + "Energy......." + tile.getPlant().GetStats()[8].GetCurrent() +
                                             "\n" + "N..........................." + tile.getPlant().GetStats()[4].GetCurrent() +
                                             "\n" + "Wa........................." + tile.getPlant().GetStats()[7].GetCurrent() +
                                             "\n" + "Wi.........................." + tile.getPlant().GetStats()[6].GetCurrent() +
                                             "\n" + "S............................" + tile.getPlant().GetStats()[5].GetCurrent();
                                         
         
                        }
                        else
                        {
                            createPlantPanel.SetActive(true);
                            //plantText.SetActive(false);
                            text_Pflanze.text = "-Plant-";
                        }

                        if(Input.GetKeyDown(KeyCode.Space))
                        {

                        }

                    }
                    else
                    {
                        fenster_PflanzenInfo.setUp(false);
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
 
                    }
                }
                else
                {
                    fenster_FeldInfo.setUp(false);
                    fenster_PflanzenInfo.setUp(false);
                }
            }

        


        }

        // Player 2
        if (playerSelect)
        {
            // Blueprint wählen um Pflanze zu erstellen
            if (Input.GetAxis("Left Trigger") == 0.0f)
            {
                if (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false && menu == false)
                {
                    menu = true;
                    pressDpad = true;
                }
                else if (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false && menu == true)
                {
                    //menu = false;
                   // pressDpad = true;
                }

                if(Input.GetAxis("D Pad Y") == 0.0)
                {
                    pressDpad = false;
                }

                if (menu == true)
                {
                    menuCursor.gameObject.SetActive(true);

                    if (Input.GetAxis("Left Stick X") >= 0.5 && menuCursor.transform.position == bpIcon0.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Left Stick X") >= 0.5 && menuCursor.transform.position == bpIcon1.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        movedtwo = true;
                    }
                    if (Input.GetAxis("Left Stick X") <= -0.5 && menuCursor.transform.position == bpIcon1.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon0.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Left Stick X") >= 0.5 && menuCursor.transform.position == bpIcon2.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon3.transform.position;
                        movedtwo = true;
                    }
                    if (Input.GetAxis("Left Stick X") <= -0.5 && menuCursor.transform.position == bpIcon2.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Left Stick X") <= -0.5 && menuCursor.transform.position == bpIcon3.transform.position && !movedtwo)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        movedtwo = true;
                    }

                    if (Input.GetAxis("Left Stick X") == 0.0)
                    {
                        movedtwo = false;
                    }



                    if (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false && menuCursor.transform.position == bpIcon0.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint0());
                        pressDpad = true;
                        print("AD");
                    }

                    if (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false && menuCursor.transform.position == bpIcon1.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint1());
                        pressDpad = true;
                    }

                    if (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false && menuCursor.transform.position == bpIcon2.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint2());
                        pressDpad = true;
                    }

                    if (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false && menuCursor.transform.position == bpIcon3.transform.position)
                    {
                        createPlant(blueprintSelect.getBlueprint3());
                        pressDpad = true;
                    }

                    if (Input.GetAxis("D Pad X") >= 0.5 && pressDpad == false)
                    {
                        menu = false;
                        pressDpad = true;
                        menuCursor.transform.position = bpIcon0.transform.position;
                    }
                }

            }

            // Tile auswahl
            if (menu == false && Input.GetAxis("Left Trigger") == 0.0)
            {
                menuCursor.gameObject.SetActive(false);

                if (Input.GetAxis("Left Stick X") >= 0.5 && Input.GetAxis("Left Stick Y") <= -0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[3];
                    moved = true;
                }

                if (Input.GetAxis("Left Stick X") <= -0.5 && Input.GetAxis("Left Stick Y") <= -0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[4];
                    moved = true;
                }

                if (Input.GetAxis("Left Stick X") <= -0.5 && Input.GetAxis("Left Stick Y") == 0.0 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[5];
                    moved = true;
                }

                if (Input.GetAxis("Left Stick X") <= -0.5 && Input.GetAxis("Left Stick Y") >= 0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[0];
                    moved = true;
                }

                if (Input.GetAxis("Left Stick X") >= 0.5 && Input.GetAxis("Left Stick Y") >= 0.5 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[1];
                    moved = true;
                }

                if (Input.GetAxis("Left Stick X") >= 0.5 && Input.GetAxis("Left Stick Y") == 0.0 && moved == false)
                {
                    tileSelect = tileSelect.getNeighbours()[2];
                    moved = true;
                }

                if (Input.GetAxis("Left Stick X") == 0.0 && Input.GetAxis("Left Stick Y") == 0.0)
                {
                    moved = false;
                }
            }



            if (tileSelect != null)
            {
                feldselect.transform.position = new Vector3(tileSelect.transform.position.x, tileSelect.transform.position.y, 1);
                feldselect.GetComponent<SpriteRenderer>().enabled = true;

                fenster_FeldInfo.setUp(true);
                // Welche Infos angezeigt werden sollen abhängig von den Tiles

                if (tileSelect.GetComponent<IsTile>() != null)
                {
                    IsTile tile = tileSelect.GetComponent<IsTile>();



                    //Jedes Feld hat 3 booleans die Aussagen ob es 1. Naerstoffe hat, 2. Wasserkraft hat, und 3. Eine Pflanze haben kann
                    text_Feld.text = "-Feldata-";
                    if (tile.getHasGroundValue())
                        text_Feld.text += "\nNaehrstoffe........" + tile.getNutrientValue();
                    if (tile.getHasWaterValue())
                        text_Feld.text += "\nWasserwert........." + tile.getWaterStrength();
                    //Jedes Feld hat Licht und Wind
                    text_Feld.text += "\nWindstaerke........" + tile.getWindStrength() +
                                          "\nLichtintensitaet..." + tile.getLightValue();

                    if (tile.getCanSustainPlant())
                    {
                        fenster_PflanzenInfo.setUp(true);

                        if (tile.getPlant() != null)
                        {

                            createPlantPanel.SetActive(false);
                            plantText.SetActive(true);
                            text_Pflanze.text =
                                             "Health........" + tile.getPlant().GetStats()[2].GetCurrent() +
                                             "\n" + "Energy......." + tile.getPlant().GetStats()[8].GetCurrent() +
                                             "\n" + "N..........................." + tile.getPlant().GetStats()[4].GetCurrent() +
                                             "\n" + "Wa........................." + tile.getPlant().GetStats()[7].GetCurrent() +
                                             "\n" + "Wi.........................." + tile.getPlant().GetStats()[6].GetCurrent() +
                                             "\n" + "S............................" + tile.getPlant().GetStats()[5].GetCurrent();


                        }
                        else
                        {
                            createPlantPanel.SetActive(true);
                            //plantText.SetActive(false);
                            text_Pflanze.text = "-Plant-";
                        }

                        if (Input.GetKeyDown(KeyCode.Space))
                        {

                        }

                    }
                    else
                    {
                        fenster_PflanzenInfo.setUp(false);
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {

                    }
                }
                else
                {
                    fenster_FeldInfo.setUp(false);
                    fenster_PflanzenInfo.setUp(false);
                }
            }




        }

    }

    public IsTile getTile()
    {
        return tileSelect;
    }

    public void setTile(IsTile test)
    {
        tileSelect = test;
    }

    void createPlant(Blueprint blueprint)
    {
        IsTile tile = tileSelect;


        float cost = blueprint.GetCost();
        if (tile != null && tile.canSustainPlant && !tile.getPlant())
        {
            if (cost >= 0 && player.GetPoints() >= cost)
            {
                tile.GrowPlant(player, plant, blueprint);
                //player.AddPoints(-cost);
            }
            else
            {
                //print("Nicht genug Energie! Spieler" + player.myNum+ " hat "+ player.GetPoints()+ " Punkte, BP kostet "+ cost);
            }

        }
    }
}