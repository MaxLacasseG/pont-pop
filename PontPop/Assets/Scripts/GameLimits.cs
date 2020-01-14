using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLimits : MonoBehaviour {
    private void OnCollisionEnter (Collision other) {
        Debug.Log ("COLLISION");
        Debug.Log (other.transform.name);
        Destroy (other.gameObject);
    }
    private void OnTriggerEnter (Collider other) {
        Debug.Log (other.transform.name);
        Destroy (other.gameObject);
    }
}