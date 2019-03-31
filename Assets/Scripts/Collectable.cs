using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            player.health++;
            player.score++;
            //AudioClip sound = Resources.Load<AudioClip>("getapplesound");
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer() {
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
