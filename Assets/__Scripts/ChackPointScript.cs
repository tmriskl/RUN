using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChackPointScript : MonoBehaviour {

    public bool Chacked = false;
    public MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            meshRenderer.enabled = false;
            Chacked = true;
        }
    }
}
