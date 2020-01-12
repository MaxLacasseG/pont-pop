using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour {
    /*
    TODO
    1- X DRAG N DROP
        a- Change in size and position
        b- Colored outline
    2- DElETE
    3- ANCHOR CONNECTION
    4- ANCHOR DECONNECTION
    5- POINTS UPDATE
    */

    public float distanceFromAnchor;
    public List<GameObject> anchors;
    public bool canMove;
    public bool isMoving;
    public Color32 movingColor;
    public Color32 regularColor;

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    public float lastClickTime;
    public float DOUBLE_CLICK_TIME = 0.2f;

    void OnEnable () {
        GameMngr.onBridgeInauguration += OnBridgeInauguration;
    }
    private void OnDisable () {
        GameMngr.onBridgeInauguration -= OnBridgeInauguration;
    }

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        rb = GetComponent<Rigidbody2D> ();
        rb.centerOfMass = new Vector2 (0, 0);
        rb.inertia = 1.0f;

        lastClickTime = Time.time;
        canMove = true;
    }

    void OnMouseDown () {
        if (!canMove) return;

        float timeSinceClick = Time.time - lastClickTime;
        if (timeSinceClick <= DOUBLE_CLICK_TIME && timeSinceClick != 0) {
            Destroy (this.gameObject);
        } else {
            isMoving = true;
            spriteRenderer.color = movingColor;
            Vector2 newLocalScale = new Vector2 (0.52f, 0.52f);
            this.transform.localScale = newLocalScale;
        }

        lastClickTime = Time.time;

    }

    void OnMouseDrag () {
        if (!canMove) return;

        if (isMoving) {
            Vector2 newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            this.transform.position = newPos;
        }
    }

    void OnMouseUp () {
        if (!canMove) return;

        Vector2 newLocalScale = new Vector2 (0.5f, 0.5f);
        spriteRenderer.color = regularColor;
        this.transform.localScale = newLocalScale;
        isMoving = false;
    }

    void OnBridgeInauguration () {
        canMove = false;
        rb.gravityScale = 1.0f;
        rb.constraints = RigidbodyConstraints2D.None;
    }

}