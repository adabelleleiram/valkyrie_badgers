using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
  public event UnityAction OnItemPickUp;
  public List<Item> items = new List<Item>();
  public List<Item> hiddenItems = new List<Item>();

  public Sprite[] featherSprites;

  void Awake()
  {
    featherSprites = Resources.LoadAll<Sprite>("feathers");
  }

  public void Add(Item item)
  {
    if (!item.isDefaultItem)
    {
      if ( items.Contains(item) )
      {
        Item current = items.Find(x => item == x);
        current.counter++;
        return;
      }
      items.Add(item);
      item.counter = 1;
    }
    else
    {
      hiddenItems.Add(item);
      item.counter = 1;
    }
    OnItemPickUp.Invoke();
  }

  public void Remove(Item item)
  {
    item.counter = 0;
    items.Remove(item);
  }
}
