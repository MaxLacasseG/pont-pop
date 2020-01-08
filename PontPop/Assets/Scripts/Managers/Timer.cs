using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Slider slider;
    public bool timeOver;
    public int startTime = 10;

    // Start is called before the first frame update
    void Start () {
        ResetTimer ();
        // Initialize with GameManager round time
        // StartTimer ();
    }

    void ResetTimer () {
        timeOver = false;
        startTime = GameMngr.instance.remaining_time;
        slider.value = GameMngr.instance.remaining_time / startTime;
    }

    void StartTimer () {
        StartCoroutine ("ReduceTimer");
    }

    public IEnumerator ReduceTimer () {
        while (GameMngr.instance.remaining_time > 0) {
            yield return new WaitForSeconds (1.0f);
            GameMngr.instance.remaining_time--;
            Debug.Log (GameMngr.instance.remaining_time);
        }
        timeOver = true;
    }

    public void Update () {
        slider.value = Mathf.Lerp (slider.value, (float) GameMngr.instance.remaining_time / startTime, 5f * Time.deltaTime);
    }

}