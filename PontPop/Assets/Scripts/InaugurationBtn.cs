using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InaugurationBtn : MonoBehaviour {

    public void InaugurateBridge () {
        GetComponent<AudioSource> ().enabled = true;
        GameMngr.instance.OnBridgeInauguration ();
    }
}