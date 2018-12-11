using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffectScript : MonoBehaviour {
    public Text hellText;
    public float fadeSpeed;
    public bool enterance = false;
    private Color color;
    

    // Use this for initialization
    void Start ()
    {
        color = hellText.color;
        hellText.color = Color.clear;

    }
	
	// Update is called once per frame
	void Update () {
        ColorChange();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterance = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterance = false;
        }
    }

    public void ColorChange()
    {
        if (enterance)
        {
            hellText.color = Color.Lerp(hellText.color, color, fadeSpeed*Time.deltaTime);
        }
        else
        {
            hellText.color = Color.Lerp(hellText.color, Color.clear, fadeSpeed*Time.deltaTime);
        }
    }
}
