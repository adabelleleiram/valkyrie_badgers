using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    InventorySlot[] slots;
    Inventory inventory;
    ItemDescription description;

    void Start()
    {
        inventory = GameHandler.inventory;
        inventory.OnItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        description = GetComponentInChildren<ItemDescription>();
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; ++i)
        {
            if (i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();
        }

        description.ClearDescription();
    }
}
