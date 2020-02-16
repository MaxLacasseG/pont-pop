using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class parapet : MonoBehaviour {
    public bool parapetIsOn = false;
    public int chances = 10;
    public TMP_Text shield;
    public Color32 safeColor;
    public Color32 unsafeColor;

    private void Start () {
        shield.color = unsafeColor;

    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "parapet") {
            shield.color = safeColor;
            parapetIsOn = true;
        }
        if (other.tag == "vehicule" && !parapetIsOn) {
            bool isFalling = Random.Range (0, chances) < 2;
            if (isFalling) {
                GameMngr.instance.heart_amount = -100;
                GameMngr.instance.solidity -= other.GetComponent<Vehicule> ().points;
                RoundMngr.instance.UpdateUI ();
                other.GetComponent<Collider2D> ().enabled = false;
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "parapet") {
            shield.color = unsafeColor;
            parapetIsOn = false;
        }
    }
}