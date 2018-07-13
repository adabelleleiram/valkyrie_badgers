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

    public static event UnityAction GlobalOnCombine;


    bool itemEnteredCollider = false;

    private void OnMouseEnter()
    {
        if (MouseCursor.instance.currentDraggedItem == itemToCombineWith)
        {
            MouseCursor.instance.SetCursor(MouseCursor.instance.combineCursor);
            itemEnteredCollider = true;
        }
    }

    private void OnMouseExit()
    {
        if (gameObject == deleteObject)
        {
            itemEnteredCollider = false;
            MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
        }
    }

    private void Update()
    {
        if (itemEnteredCollider && Input.GetMouseButtonUp(0))
        {
            MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
            onCombine.Invoke();
            GlobalOnCombine.Invoke();

            if (deleteItem)
            {
                GameHandler.inventory.Remove(itemToCombineWith);
                description.ClearDescription();
            }
            if (deleteObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
