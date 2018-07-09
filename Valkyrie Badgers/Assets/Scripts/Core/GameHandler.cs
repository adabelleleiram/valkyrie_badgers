using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Public Variables
    public static Inventory inventory;

    #endregion
    #region Singleton
    public static GameHandler instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            inventory = GetComponent<Inventory>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
