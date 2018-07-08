using UnityEngine;

public class ItemPickup : MonoBehaviour {

  public Item item;
  public void OnMouseDown()
  {
    PickUp();
  }

	public void PickUp()
  {
    Debug.Log(message: "Adding item " + item.name);
    Inventory.instance.Add(item);
    Destroy(gameObject);
  }
}
