using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPScript : MonoBehaviour {
    public GameObject remains;
    public GameObject player;
    private void Update()
    {
        gameObject.transform.LookAt(player.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            other.GetComponent<ProjectileScript>().player.GetComponent<MyUnityChanControlScriptWithRgidBody>().health++;
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<MyUnityChanControlScriptWithRgidBody>().health++;
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
