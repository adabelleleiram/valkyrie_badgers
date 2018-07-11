using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour {

  public Item item;
  public GameObject obj;

	void Update () {

    if ( Input.GetMouseButtonUp(0))
    {
      GameHandler.changeables.ChangeActive(obj, true);
      obj.SetActive(true);
      MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
    }
  }
}
