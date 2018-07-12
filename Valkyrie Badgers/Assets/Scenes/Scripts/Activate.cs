using UnityEngine;

public class Activate : MonoBehaviour {

  public bool setActive;
  public void OnMouseDown()
  {
    SetActive();
  }

  public void SetActive()
  {
    gameObject.SetActive(setActive);
  }
}
