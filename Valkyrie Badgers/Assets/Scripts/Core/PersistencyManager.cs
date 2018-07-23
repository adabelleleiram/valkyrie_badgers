using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistencyManager : MonoBehaviour
{
    Dictionary<string, bool> activeStates = new Dictionary<string, bool>();
    Dictionary<string, bool> unlockedDoors = new Dictionary<string, bool>();
    Dictionary<string, int> counterStates = new Dictionary<string, int>();
    Dictionary<string, bool> clickedStates = new Dictionary<string, bool>();
    Dictionary<string, bool> hasItemStates = new Dictionary<string, bool>();


    public bool shownLogo = false;

    public void SetActiveState(GameObject anObject)
    {
        activeStates[GetObjectID(anObject)] = anObject.activeSelf;
    }

    public void SetUnlockedState(Door door)
    {
        unlockedDoors[GetObjectID(door.gameObject)] = door.locked;
    }

    public void SetUnlockedState(LockedObj anObject)
    {
        unlockedDoors[GetObjectID(anObject.gameObject)] = anObject.locked;
    }

    public bool GetActiveState(GameObject anObject, bool aDefault)
    {
        string id = GetObjectID(anObject);
        if (!activeStates.ContainsKey(id))
            activeStates.Add(id, aDefault);

        return activeStates[id];
    }

    public bool GetLockedState(GameObject anObject, bool aDefault)
    {
        string id = GetObjectID(anObject);
        if (!unlockedDoors.ContainsKey(id))
            unlockedDoors.Add(id, aDefault);
        return unlockedDoors[id];
    }

    public void SetCounterState(GameObject anObject, int aCounter)
    {
        counterStates[GetObjectID(anObject)] = aCounter;
    }

    public int GetCounterState(GameObject anObject)
    {
        string id = GetObjectID(anObject);
        if (!counterStates.ContainsKey(id))
            counterStates.Add(id, 0);

        return counterStates[id];
    }

    public void SetClickedState(GameObject anObject, bool aClicked)
    {
        clickedStates[GetObjectID(anObject)] = aClicked;
    }

    public bool GetClickedState(GameObject anObject)
    {
        string id = GetObjectID(anObject);
        if (!clickedStates.ContainsKey(id))
            clickedStates.Add(id, false);

        return clickedStates[id];
    }

    public void SetHasItem(GameObject anObject, bool aState)
    {
        hasItemStates[GetObjectID(anObject)] = aState;
    }

    public bool GetHasItem(GameObject anObject)
    {
        string id = GetObjectID(anObject);
        if (!hasItemStates.ContainsKey(id))
            hasItemStates.Add(id, false);

        return hasItemStates[id];
    }

    string GetObjectID(GameObject anObject)
    {
        return SceneManager.GetActiveScene().name.ToString() + "_" + anObject.name;
    }
}