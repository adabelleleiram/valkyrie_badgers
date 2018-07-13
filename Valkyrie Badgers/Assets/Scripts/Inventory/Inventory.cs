using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public delegate void OnItemChanged();

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
  }

  public void Remove(Item item)
  {
    items.Remove(item);
  }
}
