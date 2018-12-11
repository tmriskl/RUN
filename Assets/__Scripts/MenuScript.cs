using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public Canvas quickMenu;
    public Button playText, exitText;

	// Use this for initialization
	void Start () {
        quickMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play()
    {
        SceneManager.LoadScene(1);
    }


    public void openMenu2()//Exit button
    {
        quickMenu.enabled = true;
        playText.enabled = false;
        exitText.enabled = false;
    }
    public void closeMenu2()// Yes button
    {
        quickMenu.enabled = false;
        playText.enabled = true;
        exitText.enabled = true;
    }

    public void exit()
    {
        Application.Quit();
    }
}
