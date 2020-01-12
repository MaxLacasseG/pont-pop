using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnchor : MonoBehaviour {
    HingeJoint2D connectedHingeJoint;
    Rigidbody2D rb;
    public Rigidbody2D connectedBody;

    private void Start () {
        rb = GetComponent<Rigidbody2D> ();
        rb.centerOfMass = new Vector2 (0, 0);
        rb.inertia = 1.0f;
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Regular_anchor") {
            Anchor connectedAnchor = other.GetComponent<Anchor> ();
            if (connectedAnchor.attachedAnchor == null) {
                /*Rigidbody2D otherRb = other.transform.parent.GetComponent<Rigidbody2D> ();
                BoxCollider2D bc = other.transform.parent.GetComponent<BoxCollider2D> ();

                connectedHingeJoint = other.transform.parent.gameObject.AddComponent<HingeJoint2D> ();
                connectedHingeJoint.autoConfigureConnectedAnchor = false;
                connectedHingeJoint.connectedAnchor = Vector2.zero;

                Vector2 anchorPlacement = new Vector2 ();
                switch (connectedAnchor.anchorPosition) {
                    case (AnchorPosition.TOP):
                        anchorPlacement = new Vector2 (bc.size.y / 2, 0f);
                        break;
                    case (AnchorPosition.RIGHT):
                        anchorPlacement = new Vector2 (bc.size.x / 2, 0f);
                        break;
                    case (AnchorPosition.BOTTOM):
                        anchorPlacement = new Vector2 (-bc.size.y / 2, 0f);
                        break;
                    case (AnchorPosition.LEFT):
                    default:
                        anchorPlacement = new Vector2 (-bc.size.x / 2, 0f);
                        break;

                }
                connectedHingeJoint.anchor = anchorPlacement;

                connectedAnchor.attachedHingeJoint = connectedHingeJoint;
                connectedAnchor.attachedAnchor = this.gameObject;
                other.transform.parent.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;*/
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        Debug.Log ("LEAVE");
    }
}