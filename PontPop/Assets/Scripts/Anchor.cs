using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnchorPosition {
    TOP,
    RIGHT,
    BOTTOM,
    LEFT
}
public class Anchor : MonoBehaviour {
    public Color32 regularColor;
    public Color32 levelColor;
    public Color32 acceptedColor;
    public Color32 deniedColor;

    public GameObject attachedAnchor;
    public HingeJoint2D attachedHingeJoint;
    public GameObject parent;

    public SpriteRenderer sp;
    public AnchorPosition anchorPosition = AnchorPosition.LEFT;

    public virtual void Start () {
        sp = GetComponent<SpriteRenderer> ();
        parent = transform.parent.gameObject;
    }

    private void OnEnable () {
        //GameMngr.instance.AddToDictonnary (gameObject);
        //Debug.Log (GameMngr.instance.connections.Keys.Count);
    }

    private void OnDisable () {
        //GameMngr.instance.RemoveFromDictonnary (gameObject);
        //foreach (GameObject go in GameMngr.instance.connections.Keys) {
        //Debug.Log (go.name);
        //}
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag != "Level_anchor" && other.tag != "Regular_anchor") return;

        if (other.tag == "Level_anchor") {
            sp.color = levelColor;

        } else if (other.tag == "Regular_anchor") {

            if (attachedAnchor == null) {
                sp.color = acceptedColor;
                parent.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        sp.color = regularColor;
        attachedAnchor = null;
    }

}