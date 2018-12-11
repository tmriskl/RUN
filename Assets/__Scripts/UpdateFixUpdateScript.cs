using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFixUpdateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    /*void Update()
    {
        Debug.Log("Update Time:" + Time.deltaTime);
    }*/
    void FixedUpdate()
    {
        Debug.Log("Fixed Update Time:" + Time.deltaTime);
    }
}
