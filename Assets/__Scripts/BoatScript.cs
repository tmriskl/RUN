using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public float turnSpeed = 1000;
    public float accellarationSpeed = 1000;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * Time.deltaTime;
        rb.AddTorque(0, h * turnSpeed, 0);
        rb.AddForce(transform.forward * v * accellarationSpeed);
    }
}
