using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Connection : MonoBehaviour {
    public GameObject from;
    public GameObject to;
}

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
    public const int TEST_TIME = 10;

    public int r1_budget = 150;
    public int r2_budget = 100;
    public int r3_budget = 75;

    public int r1_time = 120;
    public int r2_time = 90;
    public int r3_time = 60;

    public int r1_heart = 0;
    public int r2_heart = 0;
    public int r3_heart = 0;

    // GAME VARIABLES
    public bool can_play = false;
    public bool is_paused = false;
    public bool is_gravity_on = false;

    public int round_index = 0;
    public int remaining_time = 60;
    public int heart_amount = 0;
    public int score = 0;

    public string first_name = "";
    public string last_name = "";

    public ArrayList pieces = new ArrayList ();
    public Dictionary<GameObject, List<Connection>> connections;

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
    public void AddToDictonnary (GameObject objectToAdd) {
        if (!connections.ContainsKey (objectToAdd)) {
            connections.Add (objectToAdd, new List<Connection> ());
        }
    }
    public void RemoveFromDictonnary (GameObject objectToRemove) {
        if (connections.ContainsKey (objectToRemove)) {
            connections.Remove (objectToRemove);
        }
    }
    public void AddConnexion (GameObject from, GameObject to) {
        List<Connection> list;
        Connection conn = new Connection ();
        conn.from = from;
        conn.to = to;
        connections.TryGetValue (from, out list);

        if (!list.Contains (conn)) {
            list.Add (conn);
        };
    }

    public void RemoveConnexion (GameObject from, GameObject to) {
        List<Connection> list;
        Connection conn = new Connection ();
        conn.from = from;
        conn.to = to;
        connections.TryGetValue (from, out list);

        if (list.Contains (conn)) {
            list.Remove (conn);
        };
    }

    public void ResetStats () {
        is_gravity_on = false;
        is_paused = false;
        can_play = false;

        switch (round_index) {
            case 0:
                score = r1_budget;
                heart_amount = 0;
                remaining_time = r1_time;
                break;

            case 1:
                score = r2_budget;
                heart_amount = 0;
                remaining_time = r2_time;
                break;

            case 2:
                score = r3_budget;
                heart_amount = 0;
                remaining_time = r3_time;
                break;

            default:
                score = r1_budget;
                heart_amount = 0;
                remaining_time = r1_time;
                break;
        }
    }
    #endregion

}