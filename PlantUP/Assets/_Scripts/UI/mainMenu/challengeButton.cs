using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class challengeButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Start des Spiels falls der eingebene Seed ein Integer ist;
        int s = 0;
        seed.setSeedField(s);
        seed.setAutoStart(false);
        SceneManager.LoadScene("UI_Test");

    }

}
