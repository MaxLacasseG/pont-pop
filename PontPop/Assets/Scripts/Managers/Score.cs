using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public TMP_Text scoreText;
    public GameObject heartPrefab;
    public RectTransform heartPanel;
    public TMP_Text bridgeSolidityText;
    public TMP_Text weightText;
    public TMP_Text solidityScoreText;
    public TMP_Text brokenText;
    [Space]
    public Color32 regularColor;
    public Color32 warningColor;
    public Color32 brokenColor;

    // Start is called before the first frame update
    void Start () {
        UpdateUI ();

    }

    public void UpdateUI () {
        scoreText.text = GameMngr.instance.score + " M$";
        bridgeSolidityText.text = "X " + RoundMngr.instance.solidity;

        if (RoundMngr.instance.isBroken) {
            weightText.color = brokenColor;
            bridgeSolidityText.color = brokenColor;
            weightText.text = "---";
            bridgeSolidityText.text = "---";
            brokenText.text = "Pont effondré!";
        } else if ((RoundMngr.instance.solidity <= RoundMngr.instance.vehiculeWeight + 5) && RoundMngr.instance.bridgeInaugurated && RoundMngr.instance.solidity > 0) {
            weightText.color = warningColor;
            bridgeSolidityText.color = warningColor;
            weightText.text = "X " + RoundMngr.instance.vehiculeWeight;
            solidityScoreText.text = "X " + GameMngr.instance.solidity;
        } else {
            weightText.color = regularColor;
            bridgeSolidityText.color = regularColor;
            weightText.text = "X " + RoundMngr.instance.vehiculeWeight;
            solidityScoreText.text = "X " + GameMngr.instance.solidity;
        }

        int heartsToRemove = heartPanel.transform.childCount;

        for (int i = 0; i < heartsToRemove; i++) {
            Destroy (heartPanel.GetChild (i).gameObject);
        }

        if (GameMngr.instance.heart_amount < 0) {
            PenaltyHearts ();
        } else {
            InstanciateHearts ();
        }

    }

    void UpdateScore (int amount) {
        GameMngr.instance.score += amount;
    }

    void UpdateHearts (int amount) {
        GameMngr.instance.heart_amount += amount;
        GameMngr.instance.heart_amount = Mathf.Clamp (GameMngr.instance.heart_amount, 0, 3);
    }

    void InstanciateHearts () {
        for (int i = 0; i < GameMngr.instance.heart_amount; i++) {
            Instantiate (heartPrefab, Vector2.zero, Quaternion.identity, heartPanel);
        }
    }

    void PenaltyHearts () {
        for (int i = 0; i < 3; i++) {
            var go = Instantiate (heartPrefab, Vector2.zero, Quaternion.identity, heartPanel);
            go.GetComponent<Image> ().color = new Color32 (0, 0, 0, 255);
        }
    }
}