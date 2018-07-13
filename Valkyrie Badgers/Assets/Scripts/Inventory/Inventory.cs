using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
 
    public List<Item> items = new List<Item>();
    public event UnityAction OnItemPickUp;


    public void Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Add(item);
        }
        OnItemPickUp.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
