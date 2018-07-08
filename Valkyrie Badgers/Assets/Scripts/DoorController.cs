using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

  public void OnMouseDown()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
