using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  #region Singleton
  public static Player instance;

  void Awake()
  {
    if (instance != null)
    {
      Debug.Log("More than one instance of Player");
      return;
    }

    Debug.Log("Will we do this?");
    instance = this;

    DontDestroyOnLoad(this.gameObject);
  }
  #endregion

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
      Inventory.instance.Remove(item);
    }
  }
}
