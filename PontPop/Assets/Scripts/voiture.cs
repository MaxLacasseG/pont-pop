using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiture : MonoBehaviour {
    Rigidbody2D rigid;
    public float force;
    // Start is called before the first frame update
    void Start () {
        rigid = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
        Vector2 dir = new Vector2 (1, 0) * Time.deltaTime * force;
        rigid.AddRelativeForce (dir, ForceMode2D.Force);
    }
}