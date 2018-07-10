using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour {

  public Item item;
  public void OnMouseDown()
  {
    MouseCursor.instance.DragItem(item);
    GameHandler.changeables.ChangeActive(gameObject, false);
    Destroy(gameObject);
    Debug.Log(gameObject.name);
  }
}
