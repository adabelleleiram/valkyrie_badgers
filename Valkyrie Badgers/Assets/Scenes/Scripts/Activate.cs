using UnityEngine;

public class Activate : MonoBehaviour {

  public bool setActive;
  public void OnMouseDown()
  {
    SetActive();
  }

  public void SetActive()
  {
    GameHandler.changeables.ChangeActive(gameObject, setActive);
    gameObject.SetActive(setActive);
  }
}
