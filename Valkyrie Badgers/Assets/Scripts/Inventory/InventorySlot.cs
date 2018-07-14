using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class InventorySlot : MonoBehaviour
{
  [HideInInspector]
  public Item item;

  public Image icon;
  public Image active;
  public Image inactive;
  public ItemDescription description;

  public void AddItem(Item newItem)
  {
    item = newItem;
    icon.sprite = item.icon;
    icon.enabled = true;
  }

  public void ChangeFeatherOnCount()
  {
    if (item.counter >= GameHandler.inventory.featherSprites.Length)
      Debug.Log("Missing icon in feathers resources, no:" + item.counter);
    else
      icon.sprite = GameHandler.inventory.featherSprites[item.counter - 2];
  }

  public void ClearSlot()
  {
    item = null;
    icon.sprite = null;
    icon.enabled = false;
    active.enabled = false;
    inactive.enabled = true;
  }

  public void OnMouseDown()
  {
    if (item)
    {
      MouseCursor.instance.DragItem(item);
      icon.enabled = false;
      ShowItemDescription();
    }
  }

  public void OnMouseUp()
  {
    if (item)
    {
      MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
      icon.enabled = true;
    }
  }

  public void OnClick()
  {
    if (item)
    {
      ShowItemDescription();
    }
  }

  public void ShowItemDescription()
  {
    if (!item)
      return;

    active.enabled = description.SetDescription(this.item);
    inactive.enabled = !active.enabled;
  }
}