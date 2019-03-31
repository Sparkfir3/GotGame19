﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class BasicAnimalBehavior : MonoBehaviour
{
    //Animal Data
    Rigidbody2D animalRigid;
    Collider2D animalCollider;
    int health;
    public float moveSpeed;
    AudioClip animalNoise;
    bool nearPlayer;

    //External Data
    public Transform player;
    Collider2D playerCollider;
    Collider2D lumbJackCollider;
    AudioSource myAudioSource;
    Vector3 myVector;
    GameObject[] lumberjacks;

    // Start is called before the first frame update
    void Start()
    {
        animalRigid = GetComponent<Rigidbody2D>();
        animalCollider = GetComponent<Collider2D>();
        lumberjacks = GameObject.FindGameObjectsWithTag("Lumberjack");
    }

    private void Awake()
    {
        nearPlayer = false;
        //Assigning Health
        if (tag == "Mouse")
            health = 2;
        if (tag == "Rabbit")
            health = 3;
        if (tag == "Squirrel")
            health = 3;
        if (tag == "Fox")
            health = 4;
        if (tag == "Wolf")
            health = 5;
        if (tag == "Bear")
            health = 7;
    }

    //Basic AI Movement Logic
    void Update()
    {    
        //AI will stay withing a certain range
        if (Vector2.Distance(transform.position, player.position) > 1)
        {
            Debug.Log("Player");
            myVector = Vector3.Normalize(player.position - transform.position +
                new Vector3(Random.Range(-0.25f, .25f), Random.Range(-0.25f, .25f))) * moveSpeed;
        }

        else if (Vector2.Distance(transform.position, player.position) < 1)
        {
            myVector = Vector3.Normalize(new Vector3(0, 0));
        }
        
        //Will target enemies within range 
        foreach (GameObject lumb in lumberjacks)
        {
            if (lumb == null)
            {
                lumberjacks = GameObject.FindGameObjectsWithTag("Lumberjack");
            }

            if (Vector2.Distance(transform.position, lumb.transform.position) < 3)
            {
                myVector = Vector3.Normalize(lumb.transform.position - transform.position) * moveSpeed;
                break;
            }
        }


        animalRigid.velocity = myVector;
    } 

    //Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Lumberjack")
            health--;

        if (health <= 0)
            Destroy(gameObject, 0.25f);
        //myAudioSource.PlayOneShot(animalNoise);
    }
}
