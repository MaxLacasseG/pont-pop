using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CountDown : MonoBehaviour {
    AudioSource audioSource;
    public AudioClip woosh;
    public AudioClip ding;
    // Start is called before the first frame update
    void Start () {
        audioSource = GetComponent<AudioSource> ();
    }

    public void SetAudioClip (string name) {
        if (name == "woosh") {
            audioSource.clip = woosh;

        } else {
            audioSource.clip = ding;
        }
    }

    public void StartRound () {
        RoundMngr.instance.StartRound ();
    }
}