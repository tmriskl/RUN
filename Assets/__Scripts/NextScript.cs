using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScript : MonoBehaviour
{
    public bool show = false;
    public bool last = false;
    public int nextSceneNum = 0;
    int counter = 1000;

    // Use this for initialization
    private void OnGUI()
    {
        if (!last)
        {
            if (show)
            {
                GUI.Box(new Rect(5, 10, 250, 150), "Goals");
                if (counter-- <= 0)
                {
                    show = false;
                }
                GUI.Label(new Rect(10, 70 + 20, 200, 20), "You run fast enough");
                GUI.Label(new Rect(10, 90 + 20, 200, 50), "You survived the last level");
                GUI.Label(new Rect(10, 110 + 20, 200, 50), "Run faster!!!");
            }
            else
                GUI.Box(new Rect(5, 10, 250, 80), "Goals");
            GUI.Label(new Rect(10, 10 + 20, 200, 20), "Get the Green Arrows & the Red one");
            GUI.Label(new Rect(10, 30 + 20, 200, 50), "Don't let the Zombies touch you");
            GUI.Label(new Rect(10, 50 + 20, 200, 50), "Press N for the next level");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            SceneManager.LoadScene(nextSceneNum);
        }
    }
}
