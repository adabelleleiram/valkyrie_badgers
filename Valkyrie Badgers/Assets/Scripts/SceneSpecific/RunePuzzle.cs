using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunePuzzle : MonoBehaviour
{
    [System.Serializable]
    public class CompletedEvent : UnityEvent { }

    public List<Rune> runesListInOrder = new List<Rune>();
    public CompletedEvent onCompleted;

    void Start()
    {
        foreach (Rune rune in runesListInOrder)
        {
            rune.SetPuzzle(this);
            rune.SetActivated(false);
        }
    }

    public void OnRuneClicked(Rune aRune)
    {
        foreach(Rune rune in runesListInOrder)
        {
            if(rune == aRune)
            {
                rune.SetActivated(true);
                if (rune == runesListInOrder[runesListInOrder.Count - 1])
                    onCompleted.Invoke();
                break;
            }
            else if(rune.activeObject.activeSelf == false)
            {
                ResetAllRunes();
                break;
            }
        }
    }

    void ResetAllRunes()
    {
        foreach(Rune rune in runesListInOrder)
        {
            rune.SetActivated(false);
        }
    }
}
