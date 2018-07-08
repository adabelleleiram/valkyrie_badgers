using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
  Item item;
  public Image icon;
  public Image active;
  public Text description;
  public void AddItem(Item newItem)
  {
    item = newItem;
    icon.sprite = item.icon;
    icon.enabled = true;
  }

  public void ClearSlot()
  {
    item = null;
    icon.sprite = null;
    icon.enabled = false;
  }

  public void UseItem()
  {
    if (item != null)
      item.Use();
    active.enabled = !active.enabled;
    if ( active.enabled )
    {
      Debug.Log("Player pick up");
      GameHandler.player.SetItem(item);
      description.enabled = true;
      description.text = item.description;
    }
    else
    {
      Debug.Log("Player drops");
      GameHandler.player.SetItem(null);
      description.enabled = false;
    }
  }
}
