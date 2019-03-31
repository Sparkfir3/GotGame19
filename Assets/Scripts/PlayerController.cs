using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public enum TapMarkerStatus { Spawning, Moving, Despawning, Null };
    private TapMarkerStatus tapStatus = TapMarkerStatus.Null;

    public int health, score;
    public float moveSpeed;
    public GameObject tapMarker;
    public Text healthText, scoreText, scoreB, scoreC;
    public GameManager gameManager;

    private float holdTime = 0;
    private bool moveToTap = false;
    private Rigidbody2D rb;

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(health <= 0) {
            gameManager.GameOver();
        }
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
        } else if(Input.GetMouseButtonDown(0)) {
            StartCoroutine(SpawnTapMarker(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        } else {
            holdTime = 0;
        }
        rb.velocity = vel;
    }

    private float angle;
    private void LateUpdate() {
        if(vel.x > 0.05f) {
            angle = Vector3.Angle(Vector3.up, vel * -1f) - 180f;
        } else {
            angle = Vector3.Angle(Vector3.up, vel);
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        healthText.text = "Health: " + health;
        scoreText.text = "Score: " + score;
        scoreB.text = "Score: " + score;
        scoreC.text = "Score: " + score;
    }

    private bool AtDestination(Vector3 target) {
        return Vector3.Distance(transform.position, target) < 0.1f;
    }

    private IEnumerator SpawnTapMarker(Vector3 pos) {
        tapMarker.transform.position = pos;
        tapMarker.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
        moveToTap = true;
        tapStatus = TapMarkerStatus.Spawning;
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(DespawnTapMarker());
    }

    private IEnumerator DespawnTapMarker() {
        tapStatus = TapMarkerStatus.Despawning;
        SpriteRenderer sprite = tapMarker.GetComponent<SpriteRenderer>();
        while(sprite.color.a > 0) {
            sprite.color -= new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(0.025f);
            if(tapStatus == TapMarkerStatus.Spawning) {
                sprite.color = new Color32(255, 0, 0, 255);
                break;
            }
        }
        yield return null;
    }

    private Vector3 GetTouchPos(Touch touch) {
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
        pos.z = 0;
        return pos;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Lumberjack")) {
            health--;
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Terrain")) {
            moveToTap = false;
            //holdTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("WinCondition")) {
            gameManager.OnWin();
        }
    }

}
