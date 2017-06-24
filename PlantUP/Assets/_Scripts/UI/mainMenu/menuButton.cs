using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Start des Spiels falls der eingebene Seed ein Integer ist;
        int s = 0;
        string str = GameObject.Find("InputField").GetComponent<InputField>().text;
        if(Int32.TryParse(str, out s))
        {
            seed.setSeedField(s);
            SceneManager.LoadScene("UI_Test");
        }
        else
        {

        }
    }

}
