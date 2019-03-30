using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum TapMarkerStatus { Spawning, Moving, Despawning, Null };
    private TapMarkerStatus tapStatus = TapMarkerStatus.Null;

    public float moveSpeed;
    public GameObject tapMarker;

    private float holdTime = 0;
    private bool moveToTap = true;
    private Rigidbody2D rb;

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        rb = GetComponent<Rigidbody2D>();
    }

    Touch touch1;
    Vector3 vel;
    private void FixedUpdate() {
        // Basic movement -----------------------------------------------------
        if(moveToTap && !AtDestination(tapMarker.transform.position)) {
            vel = Vector3.Normalize(tapMarker.transform.position - transform.position) * moveSpeed;
        } else
            vel = Vector3.zero;
        // Inputs -------------------------------------------------------------
        if(Input.touchCount > 0) {
            touch1 = Input.GetTouch(0);
            //rb.velocity = Vector3.Normalize(GetTouchPos(touch1) - transform.position) * moveSpeed;
            if(touch1.phase == TouchPhase.Began) { 
                StartCoroutine(SpawnTapMarker(GetTouchPos(touch1)));
            } else {
                holdTime += touch1.deltaTime;
            }
            if(holdTime > 0.1f && !AtDestination(GetTouchPos(touch1))) {
                moveToTap = false;
                vel = Vector3.Normalize(GetTouchPos(touch1) - transform.position) * moveSpeed;
            } else if(holdTime > 0.1f)
                vel = Vector3.zero;
        } else {
            holdTime = 0;
        }
        rb.velocity = vel;
    }

    private bool AtDestination(Vector3 target) {
        return Vector3.Distance(transform.position, target) < 0.1f;
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
