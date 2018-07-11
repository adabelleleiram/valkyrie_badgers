using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
  Item currentItem;
  Text text;

  private void Start()
  {
    text = GetComponentInChildren<Text>();
    text.enabled = false;
  }

  public void SetDescription(string desc)
  {
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
    text.enabled = true;
    return true;
  }

  public void ClearDescription()
  {
    text.enabled = false;
    if ( currentItem != null )
      currentItem = null;
  }
}
