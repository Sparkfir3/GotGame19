﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            player.health++;
            player.score++;
            Destroy(this.gameObject);
        }
    }

}
