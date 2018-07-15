using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
  public Transform itemsParent;
  InventorySlot[] slots;
  Inventory inventory;
  int slotsFilled = 0;

  void Start()
  {
    inventory = GameHandler.inventory;
    slots = itemsParent.GetComponentsInChildren<InventorySlot>();
  }

  void Update()
  {
    for (int i = 0; i < slots.Length; ++i)
    {
      if (i < inventory.items.Count)
      {
        Item currentItem = inventory.items[i];
        slots[i].AddItem(currentItem);
        slotsFilled++;

        //Feathers
        if (currentItem.counter > 1 && currentItem)
          slots[i].UpdateItemCount();
      }
      else
      {
        slots[i].ClearSlot();
        slotsFilled--;
      }
    }
  }
}
