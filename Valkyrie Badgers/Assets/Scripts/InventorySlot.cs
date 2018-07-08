using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public Item item;

    public Image icon;
    public Image active;
    public ItemDescription description;
    public MouseCursor mouseCursor;


    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnMouseDown()
    {
        if (item)
        {
            Debug.Log("OnMouseDown");
            mouseCursor.SetCursor(mouseCursor.itemCursor, item.icon);
            icon.enabled = false;
            ShowItemDescription();
        }
    }

    public void OnMouseUp()
    {
        if(item)
        {
            Debug.Log("OnMouseUp");

            mouseCursor.SetCursor(mouseCursor.defaultCursor);
            icon.enabled = true;

        }
    }

    public void OnClick()
    {
        if (item)
        {
            ShowItemDescription();
            Debug.Log("OnClick");

        }
    }

    public void ShowItemDescription()
    {
        if (!item)
            return;

        active.enabled = true;
        description.SetDescription(this);
    }

    public void Deactivate()
    {
        active.enabled = false;
    }
}