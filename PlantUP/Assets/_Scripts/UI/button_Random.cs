using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button_Random : MonoBehaviour, IPointerClickHandler
{
    // erstellen eines Random Seeds;
    public void OnPointerClick(PointerEventData eventData)
    {
        System.Random rnd = new System.Random();
        int seed = rnd.Next(1, 1000000);
        GameObject.Find("InputField").GetComponent<InputField>().text = seed.ToString();
    }
}
