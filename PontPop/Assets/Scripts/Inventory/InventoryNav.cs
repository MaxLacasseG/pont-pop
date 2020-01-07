using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNav : MonoBehaviour {
    public void OnNext () {
        Inventory.onNextPanel ();
    }
    public void OnPrevious () {
        Inventory.onPreviousPanel ();
    }
}