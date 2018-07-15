using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItem : MonoBehaviour {

  public Item item;
	public void Remove()
  {
    Debug.Log("Remove");
    GameHandler.inventory.Remove(item);
  }
}
