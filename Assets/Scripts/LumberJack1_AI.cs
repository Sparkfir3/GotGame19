using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class LumberJack1_AI : MonoBehaviour
{
    
    //LumberJack Data
    Rigidbody2D lumbJackRigid;
    public Transform lumbJackSpawnLocation;
    public int lumbJackHealth;
    public float moveSpeed;
    public int knockback;
    //public AudioClip lumbJackShout;

    //External Data
    public Transform player;
   // public AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        lumbJackRigid = GetComponent<Rigidbody2D>();
        //Instantiate(gameObject, lumbJackSpawnLocation);
    }

    //Basic enemy movement logic
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) <= 12)
        {
            lumbJackRigid.velocity = Vector3.Normalize(player.position - transform.position) * moveSpeed;
        }


    }

    //Applies damage and knockback when lumberjack collides with player or forest ally
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.tag == "Mouse" || collision.collider.tag == "Rabbit"
            || collision.collider.tag == "Player")
        {
            lumbJackHealth--;
            lumbJackRigid.AddForce(Vector3.Normalize(collision.collider.gameObject.transform.position - transform.position) * moveSpeed * knockback);
        }

        if (collision.collider.tag == "Squirrel" || collision.collider.tag == "Fox")
        {
            lumbJackHealth -= 2;
            lumbJackRigid.AddForce(Vector3.Normalize(collision.collider.gameObject.transform.position - transform.position) * moveSpeed * knockback);
        }

        if (collision.collider.tag == "Wolf" || collision.collider.tag == "Bear")
        {
            Debug.Log("Wolf Collision");
            lumbJackHealth -= 3;
            lumbJackRigid.AddForce(Vector3.Normalize(collision.collider.gameObject.transform.position - transform.position) * moveSpeed * knockback);
        }

        if (lumbJackHealth <= 0)
            Destroy(gameObject);
       // myAudioSource.PlayOneShot(lumbJackShout);
    }

    IEnumerator Shout()
    {
       // myAudioSource.PlayOneShot(lumbJackShout);

        yield return new WaitForSeconds(3f);
    }
}
