using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    public Item item;
    public GameObject obj;

    public void Drop()
    {
        obj.SetActive(true);
        MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
    }
}
