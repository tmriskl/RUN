using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombiePopScript : MonoBehaviour {
    Animator animator;
    bool moving = false;
    public GameObject player;
    public float initialSpeed = 3;
    public float speed = 0;
    public float stay1 = 6.5f;
    public float stay2 = 3;
    public float stay3 = 30;
    private Vector3 moveDiraction;
    private int counter = 1;
    private AudioSource audioSource;
    bool dead = false;
    public RemainsScript deatroy;
    private bool first = true;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //animator.applyRootMotion = false;
        deatroy.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && !dead)
        {
            if (!moving&&first)
            {
                player = other.gameObject;
                //Debug.Log(counter++);
                //animator.applyRootMotion = false;
                animator.SetTrigger("Up");
                audioSource.Play();
                StartCoroutine(Walk());
                player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num++;
                first = false;
            }
            else if (moving)
            {
                counter--;
                if (counter < 1)
                {
                    player.GetComponent<MyUnityChanControlScriptWithRgidBody>().health--;
                    if (player.GetComponent<MyUnityChanControlScriptWithRgidBody>().health == 0)
                    {
                        SceneManager.LoadScene(5);
                    }
                }
            }
        }
        else if ((other.gameObject.tag == "Ammo") && !dead)
        {
            if (!moving&&first)
            {
                player = other.gameObject.GetComponent<ProjectileScript>().player;
                //Debug.Log(counter++);
                //animator.applyRootMotion = false;
                animator.SetTrigger("Up");
                audioSource.Play();
                StartCoroutine(Walk());
                player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num++;
                first = false;
            }
            else if (moving)
            {
                dead = true;
                //player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num--;
            }
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Walk()
    {
        yield return new WaitForSeconds(stay1);
        //animator.ResetTrigger("Up");
        //animator.SetTrigger("Walk");
        GetComponent<CapsuleCollider>().radius = 0.15F;
        GetComponent<CapsuleCollider>().height = 1.2F;
        //GetComponent<Rigidbody>().useGravity = true;
        speed = initialSpeed;
        //GetComponent<CapsuleCollider>().radius = 0.15F;
        //GetComponent<CapsuleCollider>().height = 1.2F;
        moving = true;
        //animator.applyRootMotion = false;
        StartCoroutine(Run());
    }
    private IEnumerator Run()
    {
        yield return new WaitForSeconds(stay2);
        speed += 2;
        //animator.ResetTrigger("Walk");
        //animator.SetTrigger("Run");
        //animator.applyRootMotion = false;
        //StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(stay3);
        /*animator.SetTrigger("Dead");
        moveDiraction = Vector3.zero;
        speed = 0;
        GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;
        moving = false;
        audioSource.Stop();*/
        //if (!dead)
        //    player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num--;
        dead = true;
        //animator.ResetTrigger("Walk");
    }


    void Update()
    {
        if (dead)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            animator.SetTrigger("Dead");
            moveDiraction = Vector3.zero;
            speed = 0;
            GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;
            moving = false;
            audioSource.Stop();
            if (deatroy.enabled == false)
            {
                deatroy.enabled = true;
                player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num--;
            }
        }
        else if (moving)
        {
            moveDiraction = player.transform.position - transform.position;
            gameObject.transform.LookAt(player.transform.position);
            GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;
        }
    }
}
