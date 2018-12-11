using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceScript : MonoBehaviour {

    public GameObject replace;
    private void OnCollisionEnter(Collision other)
    {
       
    /*}
    private void OnTriggerEnter(Collider other)
    {*/
        if (other.gameObject.tag == "Player")
        {
            
            Instantiate(replace, transform.position, replace.transform.rotation);
            Destroy(other.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().getRoot());
            Destroy(gameObject);

        }
    }
}
