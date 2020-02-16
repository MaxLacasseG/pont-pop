using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowButtons : MonoBehaviour {
    public GameObject backToGame;
    public GameObject backToEndScreen;
    [Space]
    public TMP_Text roundText;
    public TMP_Text totalScoreText;
    [Space]
    public TMP_Text timeScoreText;
    public TMP_Text budgetScoreText;
    public TMP_Text aestheticsScoreText;
    public TMP_Text solidityScoreText;
    public TMP_Text touristScoreText;
    public TMP_Text bonusScoreText;

    // Start is called before the first frame update
    void Start () {
        if (GameMngr.instance.round_index >= 3) {
            backToGame.SetActive (false);
            backToEndScreen.SetActive (true);
        } else {
            backToGame.SetActive (true);
            backToEndScreen.SetActive (false);
        }
        int round_index = GameMngr.instance.round_index;
        roundText.text = round_index.ToString ();

        timeScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_time").GetValue (GameMngr.instance).ToString ();
        budgetScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_budget").GetValue (GameMngr.instance).ToString ();
        aestheticsScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_aesthetic").GetValue (GameMngr.instance).ToString ();
        solidityScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_solidity").GetValue (GameMngr.instance).ToString ();
        touristScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_heart").GetValue (GameMngr.instance).ToString ();
        totalScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_total").GetValue (GameMngr.instance).ToString ();
        bonusScoreText.text = GameMngr.instance.GetType ().GetField ("r" + round_index + "_bonus").GetValue (GameMngr.instance).ToString () + "X";
    }

}