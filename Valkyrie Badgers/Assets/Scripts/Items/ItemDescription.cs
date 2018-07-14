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
    text.text = "";
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
    return true;
  }

  public void ClearDescription()
  {
    text.text = "";
    if ( currentItem != null )
      currentItem = null;
  }
}
