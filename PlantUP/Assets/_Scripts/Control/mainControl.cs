using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainControl : MonoBehaviour {

    private Transform cameraControl; // Transform Component der Camera
    static bool useSkills;

    public PlayingFieldLogic playingfield;

    public GameObject pauseMenu;

    public RectTransform bpIcon0;
    public RectTransform bpIcon1;
    public RectTransform bpIcon2;
    public RectTransform bpIcon3;

    public RectTransform upIcon0;
    public RectTransform upIcon1;
    public RectTransform upIcon2;
    public RectTransform upIcon3;

    public RectTransform menuCursor;
    public RectTransform upgradeCursor;

    public selectedBP bp;

    bool moved;
    bool changeMenu;
    bool pressDpad;

    public bool playerSelect;

    public Text blueprintInfo;

    public Text upgradeInfo;

    //Menus
    public GameObject addUpgrades;
    public GameObject selectBlueprint;
    public GameObject blueprintMenu;

    // Use this for initialization
    void Start () {

        changeMenu = false;
        moved = false;

        cameraControl = GetComponent<Transform>();
        useSkills = false;

        menuCursor.transform.position = bpIcon3.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        // Dient zur Bewegung der Camera mit den Pfeiltasten


        //print(1000);

        if(bp.getBlueprintSelect() != null)
        {
            blueprintInfo.text =

                                    
                                "\n\n\nrunning........." + bp.getBlueprintSelect().GetCost() +
                                "\nupgrade.........." + (int) (1000 + bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * 1000) +
                                "\n" +
                                "\n" + "Added Upgrades";
        }



        if (Input.GetButtonDown("X")) // falls true auf dem Bildschirm bewegen
        {

        }

        if(!playerSelect)
        {
            if(Input.GetAxis("Right Trigger") >= 0.5)
            {
                blueprintMenu.SetActive(true);

                if (changeMenu == false && Input.GetButtonDown("A")) // BP bearbeiten
                {
                    addUpgrades.SetActive(true);
                    upgradeCursor.transform.position = upIcon0.transform.position;
                    selectBlueprint.SetActive(false);
                }

                if(Input.GetButtonUp("A"))
                {
                    changeMenu = true;
                }

                if( changeMenu == true && Input.GetButtonDown("B"))
                {
                    changeMenu = false;
                    addUpgrades.SetActive(false);
                    selectBlueprint.SetActive(true);
                }


                if(changeMenu == false)
                {
                    if (Input.GetAxis("Right Stick X") >= 0.5 && menuCursor.transform.position == bpIcon3.transform.position && !moved)
                    {
                            menuCursor.transform.position = bpIcon1.transform.position;
                            moved = true;
                    }

                    if (Input.GetAxis("Right Stick X") >= 0.5 && menuCursor.transform.position == bpIcon1.transform.position && !moved)
                    {
                            menuCursor.transform.position = bpIcon2.transform.position;
                            moved = true;
                    }
                    if (Input.GetAxis("Right Stick X") <= -0.5 && menuCursor.transform.position == bpIcon1.transform.position && !moved)
                    {
                            menuCursor.transform.position = bpIcon3.transform.position;
                            moved = true;
                    }

                    if (Input.GetAxis("Right Stick X") >= 0.5 && menuCursor.transform.position == bpIcon2.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon0.transform.position;
                        moved = true;
                    }
                    if (Input.GetAxis("Right Stick X") <= -0.5 && menuCursor.transform.position == bpIcon2.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Right Stick X") <= -0.5 && menuCursor.transform.position == bpIcon0.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        moved = true;
                    }


                    if (menuCursor.transform.position == bpIcon3.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint0());
                    }

                    if (menuCursor.transform.position == bpIcon1.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint1());
                    }

                    if (menuCursor.transform.position == bpIcon2.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint2());
                    }

                    if (menuCursor.transform.position == bpIcon0.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint3());
                    }


                    if (Input.GetAxis("Right Stick X") == 0.0)
                    {
                        moved = false;
                    }
                    /*
                    if (Input.GetButtonDown("X"))
                    {
                        bp.getBlueprintSelect().GetTypeSequence().Clear();
                        bp.getBlueprintSelect().hasChanged = true;
                    }
                    */
                }


                if (changeMenu == true)
                {
                    if (Input.GetAxis("Right Stick X") >= 0.5 && upgradeCursor.transform.position == upIcon0.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon3.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Right Stick X") >= 0.5 && upgradeCursor.transform.position == upIcon3.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon2.transform.position;
                        moved = true;
                    }
                    if (Input.GetAxis("Right Stick X") <= -0.5 && upgradeCursor.transform.position == upIcon3.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon0.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Right Stick X") <= -0.5 && upgradeCursor.transform.position == upIcon2.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon3.transform.position;
                        moved = true;
                    }

            
                    if (upgradeCursor.transform.position == upIcon0.transform.position && Input.GetButtonDown("A"))
                    {
                        addUpgrade(upIcon0.GetComponent<upgradeButton>().upgrade);
                    }

                    if (upgradeCursor.transform.position == upIcon2.transform.position && Input.GetButtonDown("A"))
                    {
                        addUpgrade(upIcon2.GetComponent<upgradeButton>().upgrade);
                    }

                    if (upgradeCursor.transform.position == upIcon3.transform.position && Input.GetButtonDown("A"))
                    {
                        addUpgrade(upIcon3.GetComponent<upgradeButton>().upgrade);
                    }



                    /*
                    if (Input.GetButtonDown("X"))
                    {
                        bp.getBlueprintSelect().GetTypeSequence().Clear();
                        bp.getBlueprintSelect().hasChanged = true;
                    }
                    */

                    if (upgradeCursor.transform.position == upIcon0.transform.position)
                    {
                        upgradeInfo.text = "raises ground energy absorb" + "\n\nrunning........." + bp.getBlueprintSelect().GetCost() + "\nupgrade.........." + (int)(1000 + bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * 1000) +  "\n\nadded upgrades";
                    }

                    if (upgradeCursor.transform.position == upIcon2.transform.position)
                    {
                        upgradeInfo.text = "raises light energy absorb"  + "\n\nrunning........." + bp.getBlueprintSelect().GetCost() + "\nupgrade.........." + (int)(1000 + bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * 1000) + "\n\nadded upgrades";
                    }

                    if (upgradeCursor.transform.position == upIcon3.transform.position)
                    {
                        upgradeInfo.text = "raises water energy absorb" + "\n\nrunning........." + bp.getBlueprintSelect().GetCost() + "\nupgrade.........." + (int)(1000 + bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * 1000) + "\n\nadded upgrades";
                    }


                    if (Input.GetAxis("Right Stick X") == 0.0)
                    {
                        moved = false;

                    }
                }
            }
            else
            {
                blueprintMenu.SetActive(false);
                selectBlueprint.SetActive(true);
                addUpgrades.SetActive(false);
                changeMenu = false;
                menuCursor.transform.position = bpIcon3.transform.position;
            }

            if (Input.GetAxis("Left Trigger") >= 0.5 && Input.GetAxis("Right Trigger") == 0.0)
            {
                //skillMenu.SetActive(true);
            }
            else
            {
                //skillMenu.SetActive(false);
            }
        }

        if (playerSelect)
        {

            if (Input.GetAxis("D Pad Y") == 0.0)
            {
                pressDpad = false;
            }

            if (Input.GetAxis("Left Trigger") >= 0.5)
            {
                blueprintMenu.SetActive(true);

                if (changeMenu == false && (Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false)) // BP bearbeiten
                {
                    addUpgrades.SetActive(true);
                    upgradeCursor.transform.position = upIcon0.transform.position;
                    selectBlueprint.SetActive(false);
                    pressDpad = true;
                    changeMenu = true;
                }

                if (changeMenu == true && Input.GetButtonDown("B"))
                {
                    changeMenu = false;
                    addUpgrades.SetActive(false);
                    selectBlueprint.SetActive(true);
                }


                if (changeMenu == false)
                {
                    if (Input.GetAxis("Left Stick X") >= 0.5 && menuCursor.transform.position == bpIcon3.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Left Stick X") >= 0.5 && menuCursor.transform.position == bpIcon1.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        moved = true;
                    }
                    if (Input.GetAxis("Left Stick X") <= -0.5 && menuCursor.transform.position == bpIcon1.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon3.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Left Stick X") >= 0.5 && menuCursor.transform.position == bpIcon2.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon0.transform.position;
                        moved = true;
                    }
                    if (Input.GetAxis("Left Stick X") <= -0.5 && menuCursor.transform.position == bpIcon2.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon1.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Left Stick X") <= -0.5 && menuCursor.transform.position == bpIcon0.transform.position && !moved)
                    {
                        menuCursor.transform.position = bpIcon2.transform.position;
                        moved = true;
                    }


                    if (menuCursor.transform.position == bpIcon3.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint0());
                    }

                    if (menuCursor.transform.position == bpIcon1.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint1());
                    }

                    if (menuCursor.transform.position == bpIcon2.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint2());
                    }

                    if (menuCursor.transform.position == bpIcon0.transform.position)
                    {
                        bp.setBlueprintSelect(bp.getBlueprint3());
                    }


                    if (Input.GetAxis("Left Stick X") == 0.0)
                    {
                        moved = false;
                    }

                    if (Input.GetAxis("D Pad X") <= -0.5 && pressDpad == false)
                    {
                        bp.getBlueprintSelect().GetTypeSequence().Clear();
                        pressDpad = true;
                    }
                }


                if (changeMenu == true)
                {
                    if (Input.GetAxis("Left Stick X") >= 0.5 && upgradeCursor.transform.position == upIcon0.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon1.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Left Stick X") >= 0.5 && upgradeCursor.transform.position == upIcon1.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon3.transform.position;
                        moved = true;
                    }
                    if (Input.GetAxis("Left Stick X") <= -0.5 && upgradeCursor.transform.position == upIcon1.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon0.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Left Stick X") >= 0.5 && upgradeCursor.transform.position == upIcon3.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon2.transform.position;
                        moved = true;
                    }
                    if (Input.GetAxis("Left Stick X") <= -0.5 && upgradeCursor.transform.position == upIcon3.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon1.transform.position;
                        moved = true;
                    }

                    if (Input.GetAxis("Left Stick X") <= -0.5 && upgradeCursor.transform.position == upIcon2.transform.position && !moved)
                    {
                        upgradeCursor.transform.position = upIcon3.transform.position;
                        moved = true;
                    }


                    if (upgradeCursor.transform.position == upIcon0.transform.position && Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false)
                    {
                        addUpgrade(upIcon0.GetComponent<upgradeButton>().upgrade);
                        pressDpad = true;
                    }

                    if (upgradeCursor.transform.position == upIcon1.transform.position && Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false)
                    {
                        addUpgrade(upIcon1.GetComponent<upgradeButton>().upgrade);
                        pressDpad = true;
                    }

                    if (upgradeCursor.transform.position == upIcon2.transform.position && Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false)
                    {
                        addUpgrade(upIcon2.GetComponent<upgradeButton>().upgrade);
                        pressDpad = true;
                    }

                    if (upgradeCursor.transform.position == upIcon3.transform.position && Input.GetAxis("D Pad Y") <= -0.5 && pressDpad == false)
                    {
                        addUpgrade(upIcon3.GetComponent<upgradeButton>().upgrade);
                        pressDpad = true;
                    }


                    if (Input.GetAxis("Left Stick X") == 0.0)
                    {
                        moved = false;
                    }

                    if (Input.GetAxis("D Pad X") <= -0.5 && pressDpad == false)
                    {
                        bp.getBlueprintSelect().GetTypeSequence().Clear();
                        pressDpad = true;
                    }
                }
            }
            else
            {
                blueprintMenu.SetActive(false);
                selectBlueprint.SetActive(true);
                addUpgrades.SetActive(false);
                changeMenu = false;
                menuCursor.transform.position = bpIcon3.transform.position;
            }

            if (Input.GetAxis("Left Trigger") >= 0.5 && Input.GetAxis("Right Trigger") == 0.0)
            {
                //skillMenu.SetActive(true);
            }
            else
            {
                //skillMenu.SetActive(false);
            }
        }



        if (Input.GetKey(KeyCode.Escape)) // falls true auf dem Bildschirm bewegen
        {
            playingfield = GameObject.Find("basePlayingField(Clone)").GetComponent<PlayingFieldLogic>();
            //SceneManager.LoadScene("menu_Test");
            playingfield.setPaused(true);
            pauseMenu.SetActive(true);
        }



    }

    public static void setUseSkills(bool b)
    {
        useSkills = b;
    }

    public static bool getUseSkills()
    {
        return useSkills;
    }

    private void addUpgrade(UpgradeType upgrade)
    {
        int count = 0;

        for (int i = 0; i < bp.getBlueprintSelect().typeSequence.Count; i++)
        {
            if (bp.getBlueprintSelect().typeSequence[i] == upgrade)
            {
                count++;
            }
        }

        if (bp.getBlueprintSelect().getUpgradeCount() < 4 && gameObject.GetComponent<PlayerPrototype>().UpgradeCost() <= gameObject.GetComponent<PlayerPrototype>().GetPoints())
        {
            gameObject.GetComponent<PlayerPrototype>().AddPoints(-(1000 + bp.getBlueprintSelect().getUpgradeCount()* bp.getBlueprintSelect().getUpgradeCount() * bp.getBlueprintSelect().getUpgradeCount() * 1000));
            bp.getBlueprintSelect().typeSequence.Add(upgrade);
            bp.getBlueprintSelect().hasChanged = true;
            

            print("TEST " + upgrade);

        }
    }
}
