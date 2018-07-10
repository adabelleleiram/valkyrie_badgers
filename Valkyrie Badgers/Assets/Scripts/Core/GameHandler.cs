using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
  #region Public Variables
  public static Inventory inventory;
  public static Changeable changeables;

  #endregion
  #region Singleton
  public static GameHandler gameHandler;

  void Awake()
  {
    if (gameHandler == null)
    {
      gameHandler = this;
      inventory = GetComponent<Inventory>();
      changeables = GetComponent<Changeable>();
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }
  #endregion
}
