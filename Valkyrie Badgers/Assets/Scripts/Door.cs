using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public SceneField nextScene;
    public bool locked = false;
    public bool left = false;

    [System.Serializable]
    public class UnlockEvent : UnityEvent { }

    public UnlockEvent onUnlock;

    private void OnMouseUpAsButton()
    {
        if (!locked)
        {
            GameHandler.soundHandler.door.Play();
            GameHandler.sceneLoader.LoadScene(nextScene);
        }
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
        onUnlock.Invoke();
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

    void OnNewSceneLoading(SceneField aScene)
    {
        MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
        GameHandler.persistencyManager.SetUnlockedState(this);
    }
}
