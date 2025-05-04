using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudMover : MonoBehaviour {
    public float speed = 0.2f;

    void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Optional: loop clouds when they go off-screen
        if (transform.position.x < -7) {
            transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }
    }
}
