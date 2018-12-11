using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsScript : MonoBehaviour {
    public int counter = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter--;
        if (counter <= 0)
        {
            Destroy(gameObject);
        }

    }
}
