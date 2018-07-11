using UnityEngine;

public class Reveal : MonoBehaviour {

  public GameObject obj;
	void OnMouseDown()
  {
    obj.SetActive(true);
    GameHandler.changeables.ChangeActive(obj, true);
  }
}
