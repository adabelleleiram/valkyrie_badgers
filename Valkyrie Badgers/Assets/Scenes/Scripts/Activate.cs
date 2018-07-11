using UnityEngine;

public class Activate : MonoBehaviour {

  public void OnMouseDown()
  {
    GameHandler.changeables.ChangeActive(gameObject, true);
    gameObject.SetActive(true);
  }
}
