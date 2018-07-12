using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    #region Public Variables
    public static Inventory inventory;
    public static PersistencyManager persistencyManager;
    public static SceneLoader sceneLoader;

    #endregion
    #region Singleton
    public static GameHandler gameHandler;

    void Awake()
    {
        if (gameHandler == null)
        {
            gameHandler = this;
            inventory = GetComponent<Inventory>();
            persistencyManager = GetComponent<PersistencyManager>();
            sceneLoader = GetComponent<SceneLoader>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
