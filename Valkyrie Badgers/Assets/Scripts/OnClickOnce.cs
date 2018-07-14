using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClickOnce : MonoBehaviour
{
    [System.Serializable]
    public class ClickEvent : UnityEvent { }

    public ClickEvent onClick;
    bool clicked;

    private void Start()
    {
        clicked = GameHandler.persistencyManager.GetClickedState(gameObject);
    }

    private void OnDestroy()
    {
        GameHandler.persistencyManager.SetClickedState(gameObject, clicked);
    }

    private void OnMouseUpAsButton()
    {
        if (!clicked)
        {
            clicked = true;
            onClick.Invoke();
        }
    }
}
