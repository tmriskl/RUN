
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointScript : MonoBehaviour {

    public ChackPointScript[] points;
    public int nextSceneNum = 0;
    public bool chacked = false;
    public MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer.enabled = false;

    }
    private void OnCollisionEnter(Collision other)
    {/*
        private void OnTriggerEnter(Collider other)
    {*/
        if(other.gameObject.tag == "Player")
        {
            if (chacked)
            {
                SceneManager.LoadScene(nextSceneNum);
            }
        }
    }

    private void Update()
    {
        int i;
        chacked = true;
        for (i = 0; i < points.Length; i++)
            chacked = chacked && points[i].Chacked;
        if (chacked)
        {
            meshRenderer.enabled = true;
        }
    }
}
