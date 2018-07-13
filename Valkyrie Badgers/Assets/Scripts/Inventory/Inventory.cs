using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
  public event UnityAction OnItemPickUp;
  public List<Item> items = new List<Item>();
  public List<Item> hiddenItems = new List<Item>();

  public void Add(Item item)
  {
    if (!item.isDefaultItem)
    {
      items.Add(item);
    }
    else
    {
      hiddenItems.Add(item);
    }
    OnItemPickUp.Invoke();
  }

  public void Remove(Item item)
  {
    items.Remove(item);
  }
}
