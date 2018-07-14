using UnityEngine;
using UnityEngine.Events;

public class IfItemEvent : MonoBehaviour
{
  [System.Serializable]
  public class IfEvent : UnityEvent { }

  public IfEvent ifEvent;
  public Item item1;
  public Item item2;

  void Update()
  {
    if (GameHandler.inventory.items.Contains(item1) && GameHandler.inventory.items.Contains(item2) )
    {
      ifEvent.Invoke();
    }
  }
}
