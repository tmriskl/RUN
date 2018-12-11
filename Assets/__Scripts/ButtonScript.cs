using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    public Slider slider;
    //public Button button;

    public void Do()
    {
       Debug.Log(slider.value+"");
    }
}
