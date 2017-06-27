using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class continueButton : MonoBehaviour, IPointerClickHandler
{
    PlayingFieldLogic playingfield;



    // Use this for initialization
    void Start()
    {

        playingfield = GameObject.Find("basePlayingField(Clone)").GetComponent<PlayingFieldLogic>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        playingfield.setPaused(false);
    }
}
