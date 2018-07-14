using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
  public SceneField scene;

  public void Load()
  {
    GameHandler.sceneLoader.LoadScene(scene);
  }
}
