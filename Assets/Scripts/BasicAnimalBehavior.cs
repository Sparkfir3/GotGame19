using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class BasicAnimalBehavior : MonoBehaviour
{
    //Animal Data
    NavMeshAgent animalAgent;
    Collider2D animalCollider;
    public Transform animalSpawnLocation;
    int health;
    AudioClip animalNoise;

    //External Data
    public GameObject playerObject;
    Collider2D playerCollider;
    Touch myTouch;
    public GameObject lumbJackObject;
    Collider2D lumbJackCollider;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        animalAgent = GetComponent<NavMeshAgent>();
        animalCollider = GetComponent<Collider2D>();
        lumbJackCollider = lumbJackObject.GetComponent<Collider2D>();
    }

    private void Awake()
    {
        if (tag == "Mouse")
            health = 2;
        if (tag == "Rabbit")
            health = 3;
        if (tag == "Squirell")
            health = 3;
        if (tag == "Fox")
            health = 4;
        if (tag == "Wolf")
            health = 4;
        if (tag == "Bear")
            health = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerObject.transform.position) > 10)
            StartCoroutine("AnimalMovement");

        else if (Vector2.Distance(transform.position, lumbJackObject.transform.position) <= 5)
            animalAgent.SetDestination(lumbJackObject.transform.position);

        else if (Vector2.Distance(transform.position, playerObject.transform.position) < 2)
            StartCoroutine("AnimalMovement");
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animalCollider.IsTouching(lumbJackCollider))
            health--;
        if (health <= 0)
            Destroy(gameObject, 0.25f);
        myAudioSource.PlayOneShot(animalNoise);
    }

    IEnumerator AnimalMovement()
    {
        animalAgent.SetDestination(new Vector2(playerObject.transform.position.x + Random.Range(-7, 7),
            playerObject.transform.position.y + Random.Range(-7, 7)));

        yield return new WaitForSeconds(0.5f);
    }
}
