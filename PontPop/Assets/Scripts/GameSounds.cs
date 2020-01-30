using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour {
    public static GameSounds instance;
    AudioSource audioSource;
    public AudioClip R1_soundtrack;
    public AudioClip R2_soundtrack;
    public AudioClip R3_soundtrack;

    // Start is called before the first frame update
    void Awake () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
        }
    }

    private void Start () {
        audioSource = GetComponent<AudioSource> ();
    }
    public void Play (AudioClip clip) {
        audioSource.PlayOneShot (clip);
    }
    public void Stop () {
        audioSource.Stop ();
    }
}