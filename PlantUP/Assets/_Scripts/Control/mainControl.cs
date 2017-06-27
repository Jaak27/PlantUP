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


    // Use this for initialization
    void Start () {

        cameraControl = GetComponent<Transform>();
        useSkills = false;

	}
	
	// Update is called once per frame
	void Update () {

        // Dient zur Bewegung der Camera mit den Pfeiltasten
        if(Input.GetKey(KeyCode.RightArrow))
        {
            cameraControl.Translate(new Vector3(0.25f, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cameraControl.Translate(new Vector3(-0.25f, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraControl.Translate(new Vector3(0, -0.25f, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraControl.Translate(new Vector3(0, 0.25f, 0));
        }

        if (Input.GetKey(KeyCode.Q)) // falls true auf dem Bildschirm bewegen
        {
            GameObject skills = GameObject.Find("fenster_Skills");
            float newX = Mathf.Lerp(skills.GetComponent<RectTransform>().anchoredPosition.x, -60, 0.3f);
            skills.GetComponent<RectTransform>().anchoredPosition = new Vector3(newX,skills.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
        else // falls false offScreen bewegen
        {
            GameObject skills = GameObject.Find("fenster_Skills");
            float newX = Mathf.Lerp(skills.GetComponent<RectTransform>().anchoredPosition.x, 71, 0.3f);
            skills.GetComponent<RectTransform>().anchoredPosition = new Vector3(newX,skills.GetComponent<RectTransform>().anchoredPosition.y, 1);

        }

       
        if (Input.GetKey(KeyCode.W)) // falls true auf dem Bildschirm bewegen
        {
            GameObject bp = GameObject.Find("fenster_CreateBluePrints");
            float newX = Mathf.Lerp(bp.GetComponent<RectTransform>().anchoredPosition.x, 180, 0.3f);
            bp.GetComponent<RectTransform>().anchoredPosition = new Vector3(newX, bp.GetComponent<RectTransform>().anchoredPosition.y, 1);
        }
        else // falls false offScreen bewegen
        {
            GameObject bp = GameObject.Find("fenster_CreateBluePrints");
            float newX = Mathf.Lerp(bp.GetComponent<RectTransform>().anchoredPosition.x, 310, 0.3f);
            bp.GetComponent<RectTransform>().anchoredPosition = new Vector3(newX, bp.GetComponent<RectTransform>().anchoredPosition.y, 1);

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
}
