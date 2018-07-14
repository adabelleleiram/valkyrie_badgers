using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour {

    [System.Serializable]
    public class CountReachedEvent : UnityEvent { }

    public int finalCount;
    public CountReachedEvent onReachedCount;

    int count = 0;
    public void AddToCount()
    {
        ++count;
        if (finalCount == count)
            onReachedCount.Invoke();
    }
}
