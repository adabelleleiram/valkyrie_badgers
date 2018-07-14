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
    public static MusicHandler musicHandler;
    public static SoundHandler soundHandler;

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
            musicHandler = GetComponentInChildren<MusicHandler>();
            soundHandler = GetComponentInChildren<SoundHandler>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
