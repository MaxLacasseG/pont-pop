using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionNav : MonoBehaviour {

    public int panelIndex;
    public GameObject[] selectedPanels;

    private void Start () {
        panelIndex = 0;
        selectedPanels[panelIndex].SetActive (true);
    }

    public void SwitchPanel () {
        selectedPanels[panelIndex].SetActive (false);
        panelIndex++;
        if (panelIndex >= selectedPanels.Length) {
            panelIndex = 0;
        }
        selectedPanels[panelIndex].SetActive (true);
    }

}