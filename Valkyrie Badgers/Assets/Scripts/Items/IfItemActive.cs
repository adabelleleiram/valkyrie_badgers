using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfItemActive : MonoBehaviour {

    [System.Serializable]
    public class ItemActiveEvent : UnityEvent { }

    public GameObject anObject;
    public ItemActiveEvent onItemActivated;

    bool activated = false;

	void Update () {
		if(!activated && anObject.activeSelf)
        {
            activated = true;
            onItemActivated.Invoke();
        }
	}
}
