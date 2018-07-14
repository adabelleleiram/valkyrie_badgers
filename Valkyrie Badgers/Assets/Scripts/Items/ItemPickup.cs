using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    [System.Serializable]
    public class OnPickUp : UnityEvent { }

    public Item item;
    public OnPickUp onPickUp;
    public void OnMouseDown()
    {
        PickUp();
    }

    public void PickUp()
    {
        GameHandler.inventory.Add(item);
        gameObject.SetActive(false);
        onPickUp.Invoke();
        GameHandler.soundHandler.pickup.Play();
    }
}
