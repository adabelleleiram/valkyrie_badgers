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
        Debug.Log("Scene loaded for object " + gameObject.name);
        gameObject.SetActive(GameHandler.persistencyManager.GetActiveState(gameObject, activeOnStart));
    }

    private void OnDestroy()
    {
        GameHandler.sceneLoader.onNewSceneLoading -= OnNewSceneLoading;
    }

    void OnNewSceneLoading()
    {
        Debug.Log("Switching scene for object " + gameObject.name);
        GameHandler.persistencyManager.SetActiveState(gameObject);
    }

}
