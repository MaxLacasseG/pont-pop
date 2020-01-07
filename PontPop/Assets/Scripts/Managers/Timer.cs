using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Slider slider;
    public bool timeOver;
    public int startTime = 10;

    int _timeInSeconds;

    // Start is called before the first frame update
    void Start () {
        ResetTimer ();
        StartTimer ();
    }

    void ResetTimer () {
        timeOver = false;
        _timeInSeconds = startTime;
        slider.value = _timeInSeconds / startTime;
    }

    void StartTimer () {
        StartCoroutine ("ReduceTimer");
    }

    public IEnumerator ReduceTimer () {
        while (_timeInSeconds > 0) {
            yield return new WaitForSeconds (1.0f);
            _timeInSeconds--;
            Debug.Log (_timeInSeconds);
        }
        timeOver = true;
    }

    public void Update () {
        slider.value = Mathf.Lerp (slider.value, (float) _timeInSeconds / startTime, 5f * Time.deltaTime);
    }

}