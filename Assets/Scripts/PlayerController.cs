using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum TapMarkerStatus { Spawning, Moving, Despawning, Null };
    private TapMarkerStatus tapStatus = TapMarkerStatus.Null;

    public float moveSpeed;
    public GameObject tapMarker;

    private float holdTime = 0;
    private bool moveToTap = false;
    private Rigidbody2D rb;

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
    Touch touch1;
        // Basic movement -----------------------------------------------------
        if(moveToTap) {
            rb.velocity = Vector3.Normalize(tapMarker.transform.position - transform.position) * moveSpeed;
        }
        // Inputs -------------------------------------------------------------
        if(Input.touchCount > 0) {
            touch1 = Input.GetTouch(0);
            //rb.velocity = Vector3.Normalize(GetTouchPos(touch1) - transform.position) * moveSpeed;
            if(touch1.phase == TouchPhase.Began) { 
                StartCoroutine(SpawnTapMarker(GetTouchPos(touch1)));
            } else {
                holdTime += touch1.deltaTime;
            }
            if(holdTime > 0.1f) {
                rb.velocity = Vector3.Normalize(GetTouchPos(touch1) - transform.position) * moveSpeed;
            }
        } else {
            rb.velocity = Vector3.zero;
            holdTime = 0;
        }
    }

    private IEnumerator SpawnTapMarker(Vector3 pos) {
        tapMarker.transform.position = pos;
        moveToTap = true;
        tapStatus = TapMarkerStatus.Spawning;
        yield return null;
    }

    private IEnumerator DespawnTapMarker() {
        tapStatus = TapMarkerStatus.Despawning;
        yield return null;
    }

    private Vector3 GetTouchPos(Touch touch) {
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
        pos.z = 0;
        return pos;
    }

}
