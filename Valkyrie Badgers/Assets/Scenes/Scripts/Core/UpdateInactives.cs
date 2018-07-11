using UnityEngine;

public class UpdateInactives : MonoBehaviour {

  public GameObject obj;

  void Start()
  {
    Debug.Log(message: "Updating: " + obj);
    GameHandler.changeables.UpdateState(obj);
  }
}
