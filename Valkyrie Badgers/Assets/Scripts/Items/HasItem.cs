using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HasItem : MonoBehaviour {

  [System.Serializable]
  public class IfEvent : UnityEvent { }
  public IfEvent ifEvent;

  public Item item;
  public bool defaultItem;

  // Update is called once per frame
  void Update () {
    bool runEvent = defaultItem && GameHandler.inventory.hiddenItems.Contains(item);
    runEvent |= !defaultItem && GameHandler.inventory.items.Contains(item);

    if ( runEvent )
    {
      Debug.Log("Doing it");
      ifEvent.Invoke();
    }
	}
}
