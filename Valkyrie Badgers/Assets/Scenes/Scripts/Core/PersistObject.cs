using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistObject : MonoBehaviour
{
  public bool persistActive = true;
  public bool activeOnStart = true;

  private void Awake()
  {
    GameHandler.sceneLoader.onNewSceneLoading += OnNewSceneLoading;
  }

  private void Start()
  {
    if (persistActive)
      gameObject.SetActive(GameHandler.persistencyManager.GetActiveState(gameObject, activeOnStart));
  }

  private void OnDestroy()
  {
    GameHandler.sceneLoader.onNewSceneLoading -= OnNewSceneLoading;
  }

  void OnNewSceneLoading()
  {
    if (persistActive)
      GameHandler.persistencyManager.SetActiveState(gameObject);
  }
}
