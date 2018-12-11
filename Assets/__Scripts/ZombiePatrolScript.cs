using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombiePatrolScript : MonoBehaviour
{
    Animator animator;
    public Transform[] wayPoints;
    public float speed = 3;
    public float stay = 5;
    private bool patrol = true;
    public GameObject player;
    public float stay1 = 1;
    public float stay2 = 3;
    public float stay3 = 30;
    public RemainsScript deatroy;
    bool moving = false;
    bool dead = false;
    int counter = 2;
    private AudioSource audioSource;

    private int currentWayPoint = 0;
    private Vector3 target, moveDiraction;
    private bool readyNow = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.loop = true;
        deatroy.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol)
        {
            target = wayPoints[currentWayPoint].position;
            moveDiraction = target - transform.position;
            if ((moveDiraction.magnitude < 1) && readyNow)
            {
                StartCoroutine(Stay());
                currentWayPoint++;
                currentWayPoint %= wayPoints.Length;
            }
            //Debug.Log("GetComponent<Rigidbody>().velocity " + GetComponent<Rigidbody>().position);

            GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;
            gameObject.transform.LookAt(target);
        }
        else if (dead)
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
        else if(moving)
        {
            animator.SetTrigger("Run");
            moveDiraction = player.transform.position - transform.position;
            gameObject.transform.LookAt(player.transform.position);
            GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;

        }
    }

    private IEnumerator Stay()
    {
        readyNow = false;
        yield return new WaitForSeconds(stay);
        readyNow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && !dead)
        {
            if (!moving)
            {
                player = other.gameObject;
                GetComponent<CapsuleCollider>().radius = 0.15F;
                GetComponent<CapsuleCollider>().height = 1.2F;
                moving = true;
                patrol = false;
                StartCoroutine(Dead());
                speed += 2;
                player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num++;
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
            if (!moving)
            {
                player = other.gameObject.GetComponent<ProjectileScript>().player;
                GetComponent<CapsuleCollider>().radius = 0.15F;
                GetComponent<CapsuleCollider>().height = 1.2F;
                moving = true;
                patrol = false;
                StartCoroutine(Dead());
                speed += 2;
                player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num++;
            }
            else if (moving)
            {

                dead = true;
                //player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num--;
            }
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(stay2);
        speed += 2;
        //animator.ResetTrigger("Walk");
        //animator.SetTrigger("Run");
        //animator.applyRootMotion = false;
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(stay3);
        /*.SetTrigger("Dead");
        moveDiraction = Vector3.zero;
        speed = 0;
        GetComponent<Rigidbody>().velocity = moveDiraction.normalized * speed;
        moving = false;
        audioSource.Stop();*/
        /*if(!dead)
            player.gameObject.GetComponent<MyUnityChanControlScriptWithRgidBody>().zombies_num--;*/
        dead = true;
       // deatroy.enabled = true;

        //animator.ResetTrigger("Walk");
    }

}
