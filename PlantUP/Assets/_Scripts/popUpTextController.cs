using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUpTextController : MonoBehaviour {

    private static popUpText popUpText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("canvas_UI");

        if(!popUpText)
        {
            popUpText = Resources.Load<popUpText>("popUpText_parent");
        }
        
    }

    public static void CreatePopUpText(string text, Transform location)
    {
        popUpText instance = Instantiate(popUpText);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.setText(text);
    }
}
