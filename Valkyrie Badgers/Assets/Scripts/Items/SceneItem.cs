using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour {

  public ItemDescription description;
  public Item item;

  void OnMouseDown()
  {
    description.SetDescription(item);
  }
}
