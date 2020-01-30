using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundMngr : MonoBehaviour {
    public static RoundMngr instance;

    public Timer timer;
    public Score score;
    public Button inaugurationButton;
    public int testTime;
    AudioClip clip;
    public SceneMngr sceneMngr;

    // Start is called before the first frame update
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }
    }

    void OnEnable () {
        GameMngr.onBridgeInauguration += OnBridgeInauguration;
    }
    private void OnDisable () {
        GameMngr.onBridgeInauguration -= OnBridgeInauguration;
    }

    void Start () {
        PrepareRound ();
    }

    public void PrepareRound () {
        timer.ResetTimer ();
        score.UpdateUI ();
        testTime = GameMngr.TEST_TIME;
        switch (GameMngr.instance.round_index) {
            case (0):
                clip = GameSounds.instance.R1_soundtrack;
                break;
            case (1):
                clip = GameSounds.instance.R2_soundtrack;
                break;
            case (2):
                clip = GameSounds.instance.R3_soundtrack;
                break;
            default:
                clip = GameSounds.instance.R1_soundtrack;
                break;
        }

    }

    public void StartRound () {
        timer.StartTimer ();
        GameMngr.instance.can_play = true;
        GameSounds.instance.Play (clip);
    }

    public void OnBridgeInauguration () {
        GameMngr.instance.can_play = false;
        TestBridge ();
        timer.StopTimer ();
        // SHOW MESSAGE
        //START CARS
    }

    public void TestBridge () {
        StartCoroutine ("TestTimer");
    }

    public IEnumerator TestTimer () {
        while (testTime > 0) {
            yield return new WaitForSeconds (1.0f);
            //ADD CARS
            testTime--;
            Debug.Log (testTime);
        }
        Debug.Log (testTime);
        EndRound ();
    }

    public void EndRound () {
        SaveRoundInfos ();
        GameSounds.instance.Stop ();
        CleanGamePieces ();
        GameMngr.instance.round_index++;

        GameMngr.instance.ResetStats ();
        PrepareRound ();
        sceneMngr.SwitchScene ("Score");
    }

    public void UpdateUI () {
        score.UpdateUI ();
    }

    public void RemoveCostFromBudget (int cost) {
        GameMngr.instance.score -= cost;
        UpdateUI ();
    }

    public void GiveMoneyBack (int cost) {
        GameMngr.instance.score += cost;
        UpdateUI ();
    }

    public void CleanGamePieces () {
        int gamePiecesToRemove = transform.childCount;
        for (int i = 0; i < gamePiecesToRemove; i++) {
            Destroy (transform.GetChild (i).gameObject);
        }

    }

    public void SaveRoundInfos () {
        int round_index = GameMngr.instance.round_index;

        switch (round_index) {
            case (0):
                GameMngr.instance.r1_budget = GameMngr.instance.score;
                GameMngr.instance.r1_time = GameMngr.instance.remaining_time;
                GameMngr.instance.r1_heart = GameMngr.instance.heart_amount;
                break;
            case (1):
                GameMngr.instance.r2_budget = GameMngr.instance.score;
                GameMngr.instance.r2_time = GameMngr.instance.remaining_time;
                GameMngr.instance.r2_heart = GameMngr.instance.heart_amount;

                break;
            case (2):
                GameMngr.instance.r3_budget = GameMngr.instance.score;
                GameMngr.instance.r3_time = GameMngr.instance.remaining_time;
                GameMngr.instance.r3_heart = GameMngr.instance.heart_amount;

                break;
            default:
                break;
        }

    }

}