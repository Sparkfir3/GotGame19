using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public enum Direction { Vertical, Horizontal }

    public Transform player;
    public Vector3 currentAxis;
    public Direction currentDir;

    private Vector3 basePos;

    private void Awake() {
        basePos = transform.position;
    }

    private float posDif;
    private void LateUpdate() {
        if(currentDir == Direction.Vertical) {
            transform.position = new Vector3(basePos.x, player.position.y, basePos.z);
        } else {
            transform.position = new Vector3(player.position.x, basePos.y, basePos.z);
        }
    }

}
