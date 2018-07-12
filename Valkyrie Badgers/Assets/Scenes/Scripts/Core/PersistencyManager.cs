using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistencyManager : MonoBehaviour
{
    Dictionary<string, bool> activeStates = new Dictionary<string, bool>();

    public void SetActiveState(GameObject anObject)
    {
        activeStates[GetObjectID(anObject)] = anObject.activeInHierarchy;
    }

    public bool GetActiveState(GameObject anObject, bool aDefault)
    {
        string id = GetObjectID(anObject);
        if (!activeStates.ContainsKey(id))
            activeStates.Add(id, aDefault);

        return activeStates[id];
    }

    string GetObjectID(GameObject anObject)
    {
        return SceneManager.GetActiveScene().name.ToString() + "_" + anObject.name;
    }
}