using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicule : MonoBehaviour {
    public Rigidbody2D rb;
    public float speed;
    public int direction;
    public bool onBridge;
    public int weight;
    public int points;

    // Start is called before the first frame update
    void Start () {
        rb = GetComponent<Rigidbody2D> ();

    }

    // Update is called once per frame
    void FixedUpdate () {

        rb.AddForce (new Vector3 (1 * direction, 0, 0) * Time.deltaTime * speed);

    }

    private void OnTriggerEnter2D (Collider2D other) {

        if (other.transform.tag == "bridgeLimit") {
            onBridge = !onBridge;
            if (!RoundMngr.instance.isBroken) {
                GameMngr.instance.vehiculesAmount += onBridge? weight : weight * -1;
                if (!onBridge) {
                    RoundMngr.instance.vehiculeWeight -= weight;
                    GameMngr.instance.solidity += points;
                    RoundMngr.instance.UpdateUI ();

                } else {
                    RoundMngr.instance.vehiculeWeight += weight;
                    RoundMngr.instance.UpdateUI ();
                }
            }
        }

        if (other.transform.tag == "destroyLimit") {
            Destroy (this.gameObject);
        }

        if (other.transform.tag == "bottomLimit") {
            GetComponent<Collider2D> ().enabled = false;
            GameMngr.instance.heart_amount = -100;
            GameMngr.instance.solidity = -100;
            GameMngr.instance.remaining_time = 0;

            RoundMngr.instance.DestroyPieces ();
            RoundMngr.instance.score.UpdateUI ();
            Destroy (this.gameObject, 2f);
        }
    }

}