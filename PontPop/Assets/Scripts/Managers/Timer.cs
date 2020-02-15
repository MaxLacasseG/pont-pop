using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    public Slider slider;
    public TMP_Text timerText;

    public bool timeOver;
    public int startTime = 10;
    public Animator animator;
    // Start is called before the first frame update
    void Start () {
        ResetTimer ();
        animator = GetComponent<Animator> ();
    }

    public void ResetTimer () {
        timeOver = false;
        startTime = GameMngr.instance.remaining_time;
        timerText.text = ((float) GameMngr.instance.remaining_time / 10).ToString ("#.0");
        //slider.value = GameMngr.instance.remaining_time / startTime;
    }

    public void StartTimer () {
        StartCoroutine ("ReduceTimer");
    }

    public IEnumerator ReduceTimer () {
        while (GameMngr.instance.remaining_time > 0) {
            yield return new WaitForSeconds (0.1f);
            GameMngr.instance.remaining_time--;

            if (GameMngr.instance.remaining_time <= 150 && animator.GetBool ("timeWarning") == false) {
                animator.SetBool ("timeWarning", true);
            }
            //Debug.Log (GameMngr.instance.remaining_time);
        }
        timeOver = true;
        GameMngr.instance.OnBridgeInauguration ();
    }

    public void StopTimer () {
        StopCoroutine ("ReduceTimer");
    }

    public void Update () {
        /*slider.value = Mathf.Lerp (slider.value, (float) GameMngr.instance.remaining_time / startTime, 5f * Time.deltaTime);*/
        timerText.text = ((float) GameMngr.instance.remaining_time / 10).ToString ("#.0");

    }

}