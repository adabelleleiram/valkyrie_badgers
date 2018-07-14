using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour {

  public Item item;
	public void Get()
  {
    GameHandler.inventory.Add(item);
  }
}
