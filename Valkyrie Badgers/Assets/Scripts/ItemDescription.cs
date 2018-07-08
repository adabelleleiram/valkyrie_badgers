using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour {

    InventorySlot currentInventorySlot;

    Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        text.enabled = false;
    }

    public void SetDescription(InventorySlot inventorySlot)
    {
        if (inventorySlot == currentInventorySlot)
            return;

        if (currentInventorySlot)
            ClearDescription();

        currentInventorySlot = inventorySlot;
        text.text = inventorySlot.item.description;
        text.enabled = true;
    }

    public void ClearDescription()
    {
        if (!currentInventorySlot)
            return;

        text.enabled = false;
        currentInventorySlot.Deactivate();
        currentInventorySlot = null;
    }
}
