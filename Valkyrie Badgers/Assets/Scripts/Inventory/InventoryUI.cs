using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
  public Transform itemsParent;
  InventorySlot[] slots;
  Inventory inventory;
  ItemDescription description;
  int slotsFilled = 0;

  void Start()
  {
    inventory = GameHandler.inventory;
    slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    description = GetComponentInChildren<ItemDescription>();
  }

  void Update()
  {
    //On reload screen
    for (int i = 0; i < slots.Length; ++i)
    {
      if (i < inventory.items.Count)
      {
        slots[i].AddItem(inventory.items[i]);
        slotsFilled++;
      }
      else
      {
        slots[i].ClearSlot();
        slotsFilled--;
      }
    }
  }
}
