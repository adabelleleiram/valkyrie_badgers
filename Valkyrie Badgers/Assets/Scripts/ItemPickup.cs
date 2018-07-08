using UnityEngine;

public class ItemPickup : MonoBehaviour {

  public void OnMouseDown()
  {
    PickUp();
  }

	public void PickUp()
  {
    Debug.Log("Picking up item");
    Destroy(gameObject);
  }
}
