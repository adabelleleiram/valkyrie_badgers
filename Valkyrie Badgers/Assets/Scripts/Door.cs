using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
  public SceneField nextScene;
  public bool locked = false;
  public bool left = false;

  private void OnMouseUpAsButton()
  {
    if (!locked)
      GameHandler.sceneLoader.LoadScene(nextScene);
    Debug.Log("test");
  }
  private void OnMouseEnter()
  {
    if (!locked)
    {
      if (left)
        MouseCursor.instance.SetCursor(MouseCursor.instance.leftDoorCursor);
      else
        MouseCursor.instance.SetCursor(MouseCursor.instance.rightDoorCursor);
    }
  }

  void OnMouseExit()
  {
    MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
  }

  public void Unlock()
  {
    locked = false;
  }

  private void Awake()
  {
    GameHandler.sceneLoader.onNewSceneLoading += OnNewSceneLoading;
  }

  private void Start()
  {
    locked = GameHandler.persistencyManager.GetLockedState(gameObject, locked);
  }

  private void OnDestroy()
  {
    GameHandler.sceneLoader.onNewSceneLoading -= OnNewSceneLoading;
  }

  void OnNewSceneLoading()
  {
     GameHandler.persistencyManager.SetUnlockedState(this);
  }
}
