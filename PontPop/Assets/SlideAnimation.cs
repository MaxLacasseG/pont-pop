using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideAnimation : MonoBehaviour, IPointerDownHandler {
    Animator animator;
    // Start is called before the first frame update
    void Start () {
        animator = GetComponent<Animator> ();
    }

    public void OnPointerDown (PointerEventData eventData) {
        SlideOut ();
    }

    public void SlideIn () {
        animator.SetTrigger ("slideIn");
    }

    public void SlideOut () {
        animator.SetTrigger ("slideOut");
    }
    public void HandleText (TMP_InputField value) {
        GameMngr.instance.GetType ().GetField (value.name).SetValue (GameMngr.instance, (object) value.text);
    }
}