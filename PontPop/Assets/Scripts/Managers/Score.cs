using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public TMP_Text scoreText;
    public GameObject heartPrefab;
    public RectTransform heartPanel;

    // Start is called before the first frame update
    void Start () {
        UpdateUI ();

    }

    public void UpdateUI () {
        scoreText.text = GameMngr.instance.score + " M$";
        int heartsToRemove = heartPanel.transform.childCount;
        for (int i = 0; i < heartsToRemove; i++) {
            Destroy (heartPanel.GetChild (i).gameObject);
        }
        for (int i = 0; i < GameMngr.instance.heart_amount; i++) {
            Instantiate (heartPrefab, Vector2.zero, Quaternion.identity, heartPanel);
        }
    }

    void UpdateScore (int amount) {
        GameMngr.instance.score += amount;
    }

    void UpdateHearts (int amount) {
        GameMngr.instance.heart_amount += amount;
        GameMngr.instance.heart_amount = Mathf.Clamp (GameMngr.instance.heart_amount, 0, 3);
    }
}