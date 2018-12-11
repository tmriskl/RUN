using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
    public GameObject player;
    public Vector3 projectile_dir;
    public float projectile_speed = 30;
    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Rigidbody>().velocity = projectile_dir * projectile_speed;
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.LookAt(look);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
