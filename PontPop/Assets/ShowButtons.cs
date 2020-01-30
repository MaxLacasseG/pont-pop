using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowButtons : MonoBehaviour {
    public GameObject backToGame;
    public GameObject backToEndScreen;

    // Start is called before the first frame update
    void Start () {
        if (GameMngr.instance.round_index >= 3) {
            backToGame.SetActive (false);
            backToEndScreen.SetActive (true);
        } else {
            backToGame.SetActive (true);
            backToEndScreen.SetActive (false);
        }
    }

}