using UnityEngine;
using UnityEngine.UI;

public class CombineDropItem : MonoBehaviour
{
  public Item item;
  public GameObject obj;
  public GameObject destroy;

  public void Drop()
  {
    GameHandler.changeables.ChangeActive(obj, true);
    obj.SetActive(true);
    MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);

    Destroy(destroy);
    GameHandler.changeables.ChangeActive(destroy, false);
  }
}
