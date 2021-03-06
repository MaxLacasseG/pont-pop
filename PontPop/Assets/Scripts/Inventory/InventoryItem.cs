﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour, IPointerDownHandler {
    public GameObject prefab;
    public GameObject clone;
    public Transform prefabParent;
    public bool isBusy;
    public bool isDragging;
    public GamePiece pieceInfos;
    public Image image;
    public GameObject blocked;

    private void Start () {
        prefabParent = GameObject.FindGameObjectWithTag ("RoundMngr").transform;
        pieceInfos = prefab.GetComponent<GamePiece> ();
        blocked.SetActive (false);
    }

    public void OnPointerDown (PointerEventData eventData) {
        if (GameMngr.instance.score < pieceInfos.cost) {
            return;
        }

        //Debug.Log ("lcik");
        if (!GameMngr.instance.can_play) { return; }

        if (!isBusy) {
            isBusy = true;
            isDragging = true;
            clone = Instantiate (prefab, Vector2.zero, Quaternion.identity, prefabParent);
        }
    }

    // Update is called once per frame
    void Update () {
        if (GameMngr.instance.score < pieceInfos.cost) {
            blocked.SetActive (true);
        } else {
            blocked.SetActive (false);
        }

        if (Input.GetMouseButtonUp (0)) {
            isDragging = false;
            isBusy = false;
            clone = null;
        }

        if (isDragging && GameMngr.instance.can_play) {
            Vector2 cursor = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            clone.transform.position = new Vector2 (cursor.x, cursor.y);
        }
    }
}