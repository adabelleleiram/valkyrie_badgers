using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HasHidden : MonoBehaviour {

  [System.Serializable]
  public class IfEvent : UnityEvent { }
  public IfEvent ifEvent;

  public Item item;

  // Update is called once per frame
  void Update () {
		if ( GameHandler.inventory.hiddenItems.Contains(item) )
    {
      ifEvent.Invoke();
    }
	}
}
