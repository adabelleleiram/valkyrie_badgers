using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LockedObj : MonoBehaviour
{
  public bool locked = true;

  [System.Serializable]
  public class IfEvent : UnityEvent { }
  public IfEvent ifEvent;

  private void OnMouseUpAsButton()
  {
    if (!locked)
      ifEvent.Invoke();
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
    MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
    GameHandler.persistencyManager.SetUnlockedState(this);
  }

}
