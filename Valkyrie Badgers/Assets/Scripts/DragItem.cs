using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour {

  public Item item;
  public void OnMouseDown()
  {
    MouseCursor.instance.DragItem(item);
    Destroy(gameObject);
    Debug.Log(MouseCursor.instance.currentDraggedItem.name);
  }
}
