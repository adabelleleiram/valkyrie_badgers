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


    private void Start()
    {
        count = GameHandler.persistencyManager.GetCounterState(gameObject);
    }

    private void OnDestroy()
    {
        GameHandler.persistencyManager.SetCounterState(gameObject, count);
    }

    public void AddToCount()
    {
        ++count;
        if (finalCount == count)
            onReachedCount.Invoke();
    }
}
