using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour {
    public Transform[] wayPoints;
    public float speed = 5;
    public float stay = 5;

    private int currentWayPoint = 0;
    private Vector3 target, moveDiraction;
    private bool readyNow = true;



    // Update is called once per frame
    void Update () {
        target = wayPoints[currentWayPoint].position;
        moveDiraction = target - transform.position;
        if ((moveDiraction.magnitude<1)&& readyNow)
        {
            StartCoroutine(Stay());
            currentWayPoint++;
            currentWayPoint %= wayPoints.Length;
        }
        Debug.Log("GetComponent<Rigidbody>().velocity " + GetComponent<Rigidbody>().velocity);

        GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;
    }

    private IEnumerator Stay()
    {
        readyNow = false;
        yield return new WaitForSeconds(stay);
        readyNow = true;
    }
}
