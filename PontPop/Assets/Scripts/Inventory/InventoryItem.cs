using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler {
    public GameObject prefab;
    public GameObject clone;
    public Transform prefabParent;
    public bool isBusy;
    public bool isDragging;

    public void OnPointerDown (PointerEventData eventData) {
        if (!isBusy) {
            isBusy = true;
            isDragging = true;
            clone = Instantiate (prefab, Vector2.zero, Quaternion.identity, prefabParent);

        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp (0)) {
            isDragging = false;
            isBusy = false;
            clone = null;
        }

        if (isDragging) {
            Vector2 cursor = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            clone.transform.position = new Vector2 (cursor.x, cursor.y);
        }
    }
}