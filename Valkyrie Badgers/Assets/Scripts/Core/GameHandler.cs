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
    public static GameMusicHandler gameMusicHandler;

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
            gameMusicHandler = GetComponentInChildren<GameMusicHandler>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
