using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public GameObject tapMarker;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(Input.touchCount > 0) {
            rb.velocity = Vector3.Normalize(Camera.main.ScreenToWorldPoint((Vector3)Input.GetTouch(0).position) - transform.position) * moveSpeed;
        } else if(Input.GetMouseButton(0)) {
            rb.velocity = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * moveSpeed;
        } else {
            rb.velocity = Vector3.zero;
        }
    }

}
