using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour {
    Rigidbody2D rb;
    Collider2D cd;

    private void Start () {
        rb = GetComponent<Rigidbody2D> ();
        cd = GetComponent<Collider2D> ();
    }
    void OnCollisionEnter2D (Collision2D other) {
        if (other.relativeVelocity.y > 5.0f) {
            Debug.Log ("test");
        }
    }
}