using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameBtn : MonoBehaviour {
    public TMP_Text round1Text;
    public TMP_Text round2Text;
    public TMP_Text round3Text;
    public TMP_Text finalScoreText;

    public SceneMngr sceneMngr;

    private void Start () {
        round1Text.text = GameMngr.instance.r1_total + " pts";
        round2Text.text = GameMngr.instance.r2_total + " pts";
        round3Text.text = GameMngr.instance.r3_total + " pts";
        int finalScore = GameMngr.instance.r1_total + GameMngr.instance.r2_total + GameMngr.instance.r3_total;
        GameMngr.instance.final_score = finalScore;
        finalScoreText.text = finalScore + " points";
        sceneMngr = GameObject.FindObjectOfType<SceneMngr> ();
    }

    public void EndGame () {
        GameMngr.instance.OnGameEnd ();
        sceneMngr.SwitchScene ("MainMenu");
    }
}