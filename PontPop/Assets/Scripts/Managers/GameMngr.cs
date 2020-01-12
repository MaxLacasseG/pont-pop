using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMngr : MonoBehaviour {
    // MANAGER SINGLETON
    public static GameMngr instance;

    // ARRAY OF OTHER MANAGERS INSTANCIATED AFTERWARDS
    public GameObject[] managers;

    // GAME EVENTS
    public delegate void GameEvent ();
    public static GameEvent onGameStart;
    public static GameEvent onGamePause;
    public static GameEvent onRoundStart;
    public static GameEvent onBridgeInauguration;
    public static GameEvent onRoundEnd;
    public static GameEvent onGameEnd;

    // GAME CONSTANTS
    public const int MAX_ROUNDS = 3;
    public const int MAX_HEARTS = 3;

    public const int R1_BUDGET = 150;
    public const int R2_BUDGET = 100;
    public const int R3_BUDGET = 75;

    public const int R1_TIME = 120;
    public const int R2_TIME = 90;
    public const int R3_TIME = 60;

    public const int R1_HEART_OBJ = 0;
    public const int R2_HEART_OBJ = 0;
    public const int R3_HEART_OBJ = 1;

    // GAME VARIABLES
    public bool can_play = false;
    public bool is_paused = false;
    public bool is_gravity_on = false;

    public int remaining_time = 60;
    public int round_index = 0;
    public int heart_amount = 0;
    public int score = 0;

    public string first_name = "";
    public string last_name = "";

    public ArrayList pieces = new ArrayList ();

    #region UnityHooks

    // USE THIS FOR INITIALIZATION
    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad (gameObject);
            InstantiateSubManagers ();
            ResetStats ();
        } else {
            Destroy (gameObject);
        }
    }

    //REGISTERS GAME EVENTS
    void OnEnable () {

    }

    //UNREGISTERS GAME EVENTS
    void OnDisable () {

    }

    public void Start () {
        //START GAME 
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            //OPTIONS
            //EventManager.TriggerEvent (GameActions.OnNewGame);
            //EventManager.OnGameStart ();
        }
    }
    #endregion

    #region === Game Events ===
    public void OnGameStart () {
        Debug.Log ("Game Start");
        onGameStart ();
        //Resets UI
    }

    public void OnGamePause () {
        Debug.Log ("Game Pause");
        onGamePause ();
        //Resets UI
    }

    public void OnGameEnd () {
        Debug.Log ("GAme END");
        onGameEnd ();
        //LvlManager.StartLevel ("Intro");
    }
    public void OnRoundStart () {
        Debug.Log ("Round start");
        ResetStats ();
        //UIManager.instance.RefreshPoints (points);
        //LvlManager.StartLevel ("Round");
    }
    public void OnRoundEnd () {
        Debug.Log ("Round END");
        //IF round_index > MAX_ROUNDS
        //  round_index ++
        //  start new round
        //ELSE 
        //  end game

    }

    public void OnBridgeInauguration () {
        is_gravity_on = true;
        is_paused = true;
        can_play = false;
        onBridgeInauguration ();
    }
    #endregion

    #region === Manager Methods ===
    private void InstantiateSubManagers () {
        if (managers.Length <= 0) return;
        foreach (GameObject manager in managers) {
            Instantiate (manager, transform.position, transform.rotation, transform);
        }
    }

    private void ResetStats () {
        is_gravity_on = false;
        is_paused = false;
        can_play = false;

        switch (round_index) {
            case 0:
                score = R1_BUDGET;
                heart_amount = 0;
                remaining_time = R1_TIME;
                break;

            case 1:
                score = R2_BUDGET;
                heart_amount = 0;
                remaining_time = R2_TIME;
                break;

            case 2:
                score = R3_BUDGET;
                heart_amount = 0;
                remaining_time = R3_TIME;
                break;

            default:
                score = R1_BUDGET;
                heart_amount = 0;
                remaining_time = R1_TIME;
                break;
        }
    }
    #endregion

}