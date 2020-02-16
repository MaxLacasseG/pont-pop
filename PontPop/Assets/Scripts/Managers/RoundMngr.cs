using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RoundMngr : MonoBehaviour {
    public static RoundMngr instance;

    public Timer timer;
    public TMP_Text bonusText;
    public VehiculeSpawner vehiculeSpawner;
    public Score score;
    public Button inaugurationButton;
    public int testTime;
    public int brokenTime;

    public int vehiculeWeight;
    public int solidity;
    public bool bridgeInaugurated;
    public bool isBroken;
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
    private void Update () {
        if (bridgeInaugurated) {
            if (solidity <= vehiculeWeight && solidity > 0) {
                DestroyPieces ();
            }
        }
    }
    public void DestroyPieces () {
        if (bridgeInaugurated && !isBroken) {
            isBroken = true;
            GamePiece[] pieces = transform.GetComponentsInChildren<GamePiece> ();
            foreach (var item in pieces) {
                item.GetComponent<Collider2D> ().enabled = false;
            }
        }
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
        GameSounds.instance.audioSource.clip = clip;
        GameSounds.instance.PlayLoop ();
    }

    public void OnBridgeInauguration () {
        bridgeInaugurated = true;
        GameMngr.instance.can_play = false;

        TestBridge ();
        timer.StopTimer ();
    }

    public void TestBridge () {
        StartCoroutine ("TestTimer");
    }

    public IEnumerator TestTimer () {
        while (testTime > 0) {
            yield return new WaitForSeconds (1.0f);
            if (isBroken) {
                GameMngr.instance.bonus = 1;
                GameMngr.instance.score = 0;
                GameMngr.instance.remaining_time = 0;
                GameMngr.instance.woodPieces = 0;
                GameMngr.instance.stonePieces = 0;
                GameMngr.instance.steelPieces = 0;
                bonusText.text = "1X";
                UpdateUI ();

                StartCoroutine ("BrokenTimer");
                StopCoroutine ("TestTimer");
            };

            vehiculeSpawner.SpawnVehicule ();
            testTime--;

        }
        EndRound ();
    }

    public IEnumerator BrokenTimer () {
        while (brokenTime > 0) {
            yield return new WaitForSeconds (1.0f);
            brokenTime--;
        }
        EndRound ();
    }

    public void EndRound () {
        SaveRoundInfos ();
        GameSounds.instance.Stop ();
        //CleanGamePieces ();
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
                GameMngr.instance.r1_time = (int) GameMngr.instance.remaining_time / 10;
                GameMngr.instance.r1_heart = GameMngr.instance.heart_amount > 0 ? 100 * GameMngr.instance.heart_amount : GameMngr.instance.heart_amount;
                GameMngr.instance.r1_solidity = GameMngr.instance.solidity > 0 ? 10 * GameMngr.instance.solidity : GameMngr.instance.solidity;
                GameMngr.instance.r1_bonus = GameMngr.instance.bonus;
                GameMngr.instance.r1_total = (GameMngr.instance.r1_aesthetic + GameMngr.instance.r1_budget + GameMngr.instance.r1_time + GameMngr.instance.r1_heart + GameMngr.instance.r1_solidity) * GameMngr.instance.bonus;

                if ((GameMngr.instance.woodPieces > 0 && GameMngr.instance.steelPieces == 0 && GameMngr.instance.stonePieces == 0) || (GameMngr.instance.woodPieces == 0 && GameMngr.instance.steelPieces > 0 && GameMngr.instance.stonePieces == 0) || (GameMngr.instance.woodPieces == 0 && GameMngr.instance.steelPieces == 0 && GameMngr.instance.stonePieces > 0)) {
                    int amount = GameMngr.instance.woodPieces + GameMngr.instance.stonePieces + GameMngr.instance.steelPieces;
                    GameMngr.instance.r1_aesthetic = amount * 10;
                }

                if (GameMngr.instance.score > 0) {
                    GameMngr.instance.r2_budget += GameMngr.instance.score;
                }
                if (GameMngr.instance.remaining_time > 0) {
                    GameMngr.instance.r2_budget += (int) GameMngr.instance.remaining_time / 10;
                }

                break;
            case (1):
                GameMngr.instance.r2_budget = GameMngr.instance.score;
                GameMngr.instance.r2_time = (int) GameMngr.instance.remaining_time / 10;
                GameMngr.instance.r2_heart = GameMngr.instance.heart_amount > 0 ? 100 * GameMngr.instance.heart_amount : GameMngr.instance.heart_amount;
                GameMngr.instance.r2_solidity = GameMngr.instance.solidity > 0 ? 10 * GameMngr.instance.solidity : GameMngr.instance.solidity;
                GameMngr.instance.r2_bonus = GameMngr.instance.bonus;
                GameMngr.instance.r2_total = (GameMngr.instance.r2_aesthetic + GameMngr.instance.r2_budget + GameMngr.instance.r2_time + GameMngr.instance.r2_heart + GameMngr.instance.r2_solidity) * GameMngr.instance.bonus;

                if ((GameMngr.instance.woodPieces > 0 && GameMngr.instance.steelPieces == 0 && GameMngr.instance.stonePieces == 0) || (GameMngr.instance.woodPieces == 0 && GameMngr.instance.steelPieces > 0 && GameMngr.instance.stonePieces == 0) || (GameMngr.instance.woodPieces == 0 && GameMngr.instance.steelPieces == 0 && GameMngr.instance.stonePieces > 0)) {
                    int amount = GameMngr.instance.woodPieces + GameMngr.instance.stonePieces + GameMngr.instance.steelPieces;
                    GameMngr.instance.r2_aesthetic = amount * 10;
                }

                if (GameMngr.instance.score > 0) {
                    GameMngr.instance.r3_budget += GameMngr.instance.score;
                }
                if (GameMngr.instance.remaining_time > 0) {
                    GameMngr.instance.r3_budget += (int) GameMngr.instance.remaining_time / 10;
                }
                break;
            case (2):
                GameMngr.instance.r3_budget = GameMngr.instance.score;
                GameMngr.instance.r3_time = (int) GameMngr.instance.remaining_time / 10;
                GameMngr.instance.r3_heart = GameMngr.instance.heart_amount > 0 ? 100 * GameMngr.instance.heart_amount : GameMngr.instance.heart_amount;
                GameMngr.instance.r3_solidity = GameMngr.instance.solidity > 0 ? 10 * GameMngr.instance.solidity : GameMngr.instance.solidity;
                GameMngr.instance.r3_bonus = GameMngr.instance.bonus;
                GameMngr.instance.r3_total = (GameMngr.instance.r3_aesthetic + GameMngr.instance.r3_budget + GameMngr.instance.r3_time + GameMngr.instance.r3_heart + GameMngr.instance.r3_solidity) * GameMngr.instance.bonus;

                if ((GameMngr.instance.woodPieces > 0 && GameMngr.instance.steelPieces == 0 && GameMngr.instance.stonePieces == 0) || (GameMngr.instance.woodPieces == 0 && GameMngr.instance.steelPieces > 0 && GameMngr.instance.stonePieces == 0) || (GameMngr.instance.woodPieces == 0 && GameMngr.instance.steelPieces == 0 && GameMngr.instance.stonePieces > 0)) {
                    int amount = GameMngr.instance.woodPieces + GameMngr.instance.stonePieces + GameMngr.instance.steelPieces;
                    GameMngr.instance.r3_aesthetic = amount * 10;
                }
                break;
            default:
                break;
        }

    }

}