using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public void OnMouseDown()
    {
        PickUp();
    }

    public void PickUp()
    {
        Debug.Log(message: "Adding item " + item.name);
        GameHandler.inventory.Add(item);
        Destroy(gameObject);
    }
}
