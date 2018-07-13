using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
  public SceneField scene;

  public void Load()
  {
    Debug.Log("Loading scene");
    GameHandler.sceneLoader.LoadScene(scene);
  }
}
