using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGunScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(new Vector3(0, 0.5f, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<MyUnityChanControlScriptWithRgidBody>().GetGun();

            Destroy(gameObject);
        }
    }
}
