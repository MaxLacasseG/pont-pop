using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public List<RectTransform> panels;
    public RectTransform selectedPanel;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public int index = 0;

    public delegate void InventoryEvent ();
    public static InventoryEvent onNextPanel;
    public static InventoryEvent onPreviousPanel;

    void OnEnable () {
        onNextPanel += selectNextPanel;
        onPreviousPanel += selectPreviousPanel;
    }
    void OnDisable () {
        onNextPanel -= selectNextPanel;
        onPreviousPanel -= selectPreviousPanel;
    }

    void OnStart () {
        selectedPanel = panels[0];
    }
    public void selectNextPanel () {
        selectedPanel.gameObject.SetActive (false);
        int newIndex = index + 1 >= panels.Count? 0 : index + 1;
        selectedPanel = panels[newIndex];

        index = newIndex;
        selectedPanel.gameObject.SetActive (true);

    }

    public void selectPreviousPanel () {
        selectedPanel.gameObject.SetActive (false);
        int newIndex = index - 1 < 0 ? panels.Count - 1 : index - 1;
        selectedPanel = panels[newIndex];

        index = newIndex;
        selectedPanel.gameObject.SetActive (true);

    }

}