using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCombine : MonoBehaviour
{
  [System.Serializable]
  public class OnCombine : UnityEvent { }

  public Item itemToCombineWith;
  public OnCombine onCombine;
  public ItemDescription description;
  public bool deleteObject;
  public bool deleteItem;

  bool itemEnteredCollider = false;

  private void OnMouseEnter()
  {
    //Kolla zoomed item osv här kanske?
    if (MouseCursor.instance.currentDraggedItem == itemToCombineWith)
      itemEnteredCollider = true;
  }

  private void OnMouseExit()
  {
    itemEnteredCollider = false;
    MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
  }

  private void Update()
  {
    if (itemEnteredCollider && Input.GetMouseButtonUp(0))
    {
      MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
      onCombine.Invoke();

      if ( deleteItem )
      {
        description.ClearDescription();
      }
      if ( deleteObject )
      {
        Destroy(gameObject);
      }
    }
  }
}
