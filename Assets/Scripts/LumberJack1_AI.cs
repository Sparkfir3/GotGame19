using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class LumberJack1_AI : MonoBehaviour
{
    //LumberJack Data
    NavMeshAgent lumbJackAgent;
    Collider2D lumbJackCollider;
    Rigidbody2D lumbJackRigid;
    public Transform lumbJackSpawnLocation;
    public int lumbJackHealth;
    public AudioClip lumbJackShout;

    //External Data
    public GameObject player;
    Collider2D playerCollider;
    public Collider2D[] animalColliders;            //0=mouse, 1=rabbit, 2=squirrel, 3=fox, 4=wolf, 5=bear
    public AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        lumbJackAgent = GetComponent<NavMeshAgent>();
        lumbJackCollider = GetComponent<Collider2D>();
        lumbJackRigid = GetComponent<Rigidbody2D>();
        Instantiate(gameObject, lumbJackSpawnLocation);
        playerCollider = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= 12)
        {
            lumbJackAgent.SetDestination(player.transform.position);
            StartCoroutine("Shout");
        }


    }

    //Applies damage and knockback when lumberjack collides with player or forest ally
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (lumbJackCollider.IsTouching(animalColliders[0]) || lumbJackCollider.IsTouching(animalColliders[1])
            || lumbJackCollider.IsTouching(playerCollider))
        {
            lumbJackHealth--;
            lumbJackRigid.AddForce((player.transform.position*-100));
        }

        if (lumbJackCollider.IsTouching(animalColliders[2]) || lumbJackCollider.IsTouching(animalColliders[3]))
        {
            lumbJackHealth -= 2;
            lumbJackRigid.AddForce((player.transform.position * -150));
        }

        if (lumbJackCollider.IsTouching(animalColliders[4]) || lumbJackCollider.IsTouching(animalColliders[5]))
        {
            lumbJackHealth -= 3;
            lumbJackRigid.AddForce((player.transform.position * -200));
        }

        if (lumbJackHealth <= 0)
            Destroy(gameObject);
        myAudioSource.PlayOneShot(lumbJackShout);
    }

    IEnumerator Shout()
    {
        myAudioSource.PlayOneShot(lumbJackShout);

        yield return new WaitForSeconds(2f);
    }
}
