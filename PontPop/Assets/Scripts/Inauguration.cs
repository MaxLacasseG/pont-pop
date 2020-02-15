using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inauguration : MonoBehaviour {
    public TMP_Text bonusText;
    public Image bikeIcon;
    public Image carIcon;
    public Image truckIcon;

    public Color32 highlightedColor;
    public Color32 disabledColor;
    void OnEnable () {

    }
    void Start () {
        bonusText.text = GameMngr.instance.bonus + "X";
    }

    public void SwitchBonus (int amount) {
        if (!GameMngr.instance.can_play) { return; }
        int newBonus = Mathf.Clamp (GameMngr.instance.bonus + amount, 1, 3);
        GameMngr.instance.bonus = newBonus;
        bonusText.text = newBonus + "X";

        switch (newBonus) {
            case (1):
                bikeIcon.color = highlightedColor;
                carIcon.color = highlightedColor;
                truckIcon.color = disabledColor;
                break;
            case (2):
                bikeIcon.color = disabledColor;
                carIcon.color = highlightedColor;
                truckIcon.color = highlightedColor;
                break;
            case (3):
                bikeIcon.color = disabledColor;
                carIcon.color = disabledColor;
                truckIcon.color = highlightedColor;
                break;
            default:
                bikeIcon.color = disabledColor;
                carIcon.color = disabledColor;
                truckIcon.color = disabledColor;
                break;
        }
    }
}