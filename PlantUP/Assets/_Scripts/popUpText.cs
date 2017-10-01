using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpText : MonoBehaviour {

    public Animator animator;
    private Text energyText;

	// Use this for initialization
	void Start () {


        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);

        energyText = animator.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(string text)
    {
        animator.GetComponent<Text>().text = text;
    }
}
