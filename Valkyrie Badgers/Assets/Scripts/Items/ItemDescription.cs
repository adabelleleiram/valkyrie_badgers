using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    Item currentItem;
    Text text;

    public Item itemDescribed { get { return currentItem; } }

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = "";
    }

    public void SetDescription(string desc)
    {
        ClearDescription();
        text.text = desc;
    }

    public bool SetDescription(Item item)
    {
        if (item == currentItem)
            return true;

        if (currentItem)
        {
            ClearDescription();
            return false;
        }

        currentItem = item;
        text.text = currentItem.description;
        return true;
    }

    public void ClearDescription()
    {
        text.text = "";
        currentItem = null;
    }
}
