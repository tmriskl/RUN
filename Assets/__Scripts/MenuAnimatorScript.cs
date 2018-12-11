using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimatorScript : MonoBehaviour {
    Animator animator;
    public bool menu;
    public bool win;
    public bool lose;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Menu", menu);
        animator.SetBool("Win", win);
        animator.SetBool("Lose", lose);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
