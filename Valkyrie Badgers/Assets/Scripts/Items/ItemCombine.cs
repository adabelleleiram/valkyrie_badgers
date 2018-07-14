using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCombine : MonoBehaviour
{
    [System.Serializable]
    public class OnCombine : UnityEvent { }

    public Item itemToCombineWith;
    public int minRequiredCount = 1;
    public OnCombine onCombine;
    public ItemDescription description;
    public bool deactivateObject;
    public bool deleteItem;

    public static event UnityAction GlobalOnCombine;

    Item itemInCollider = null;

    private void OnMouseEnter()
    {
        itemInCollider = MouseCursor.instance.currentDraggedItem;
    }

    private void OnMouseExit()
    {
        itemInCollider = null;
    }

    private void Update()
    {
        if (itemInCollider == null || Input.GetMouseButtonUp(0) == false)
            return;

        if (itemInCollider == itemToCombineWith && minRequiredCount <= itemToCombineWith.counter)
        {
            onCombine.Invoke();
            GlobalOnCombine.Invoke();
            GameHandler.soundHandler.combine.Play();

            if (deleteItem)
            {
                GameHandler.inventory.Remove(itemToCombineWith);
                if (description != null)
                    description.ClearDescription();
            }
            if (deactivateObject)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            GameHandler.soundHandler.combineError.Play();
        }

        itemInCollider = null;
    }
}
