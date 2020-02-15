using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InaugurationBtn : MonoBehaviour {

    public void InaugurateBridge () {
        if (GameMngr.instance.can_play) {
            GetComponent<AudioSource> ().enabled = true;
            GameMngr.instance.OnBridgeInauguration ();
        }
    }
}