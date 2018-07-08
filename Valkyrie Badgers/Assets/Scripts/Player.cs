using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public Item item;

  public void SetItem(Item newItem)
  {
    Debug.Log("Setting the item");
    item = newItem;
  }

  void Update()
  {
    if (item != null && Input.GetMouseButtonDown(0) )
    {
      Debug.Log("Clicking");
      Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      spawnPosition.z = 0.0f;
      GameHandler.inventory.Remove(item);
    }
  }
}
