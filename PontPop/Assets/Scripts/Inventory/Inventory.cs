using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour {
    public List<RectTransform> panels;
    public RectTransform selectedPanel;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public int index = 0;
    public TMP_Text inventoryText;

    public delegate void InventoryEvent ();
    public static InventoryEvent onNextPanel;
    public static InventoryEvent onPreviousPanel;

    void OnEnable () {
        onNextPanel += selectNextPanel;
        onPreviousPanel += selectPreviousPanel;
        selectedPanel = panels[0];
        inventoryText.text = selectedPanel.name;
    }
    void OnDisable () {
        onNextPanel -= selectNextPanel;
        onPreviousPanel -= selectPreviousPanel;
    }

    void OnStart () {
        selectedPanel = panels[0];
        inventoryText.text = selectedPanel.name;

    }
    public void selectNextPanel () {
        if (!GameMngr.instance.can_play) { return; }
        selectedPanel.gameObject.SetActive (false);
        int newIndex = index + 1 >= panels.Count? 0 : index + 1;
        selectedPanel = panels[newIndex];

        index = newIndex;
        selectedPanel.gameObject.SetActive (true);
        inventoryText.text = selectedPanel.name;

    }

    public void selectPreviousPanel () {
        if (!GameMngr.instance.can_play) { return; }

        selectedPanel.gameObject.SetActive (false);
        int newIndex = index - 1 < 0 ? panels.Count - 1 : index - 1;
        selectedPanel = panels[newIndex];

        index = newIndex;
        selectedPanel.gameObject.SetActive (true);
        inventoryText.text = selectedPanel.name;

    }

}