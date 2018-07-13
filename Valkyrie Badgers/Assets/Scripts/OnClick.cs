using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClick : MonoBehaviour {

    [System.Serializable]
    public class ClickEvent : UnityEvent { }

    public ClickEvent onClick;

    private void OnMouseUpAsButton()
    {
        Debug.Log("Clicking");
        onClick.Invoke();
    }
}
