﻿using System.Collections;
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


    bool itemEnteredCollider = false;

    private void OnMouseEnter()
    {
        if (MouseCursor.instance.currentDraggedItem == itemToCombineWith && minRequiredCount <= itemToCombineWith.counter)
        {
            MouseCursor.instance.SetCursor(MouseCursor.instance.combineCursor);
            itemEnteredCollider = true;
        }
    }

    private void OnMouseExit()
    {
        itemEnteredCollider = false;
    }

    private void Update()
    {
        if (itemEnteredCollider && Input.GetMouseButtonUp(0))
        {
            MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
            onCombine.Invoke();
            GlobalOnCombine.Invoke();
            itemEnteredCollider = false;

            if (deleteItem)
            {
                GameHandler.inventory.Remove(itemToCombineWith);
                if(description != null)
                    description.ClearDescription();
            }
            if (deactivateObject)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
