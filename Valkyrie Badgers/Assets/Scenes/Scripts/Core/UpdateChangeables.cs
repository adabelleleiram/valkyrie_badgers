using UnityEngine;

public class UpdateChangeables : MonoBehaviour
{
  void Start()
  {
    Debug.Log(message: "Updating: " + gameObject);
    GameHandler.changeables.UpdateState(gameObject);
  }
}