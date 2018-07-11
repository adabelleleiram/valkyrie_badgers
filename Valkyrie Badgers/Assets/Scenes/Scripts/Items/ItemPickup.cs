using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public Item item;
  public void OnMouseDown()
  {
    Debug.Log("Pick up shovel");
    PickUp();
  }

  public void PickUp()
  {
    GameHandler.inventory.Add(item);
    GameHandler.changeables.ChangeActive(gameObject, false);
    Destroy(gameObject);
  }
}
