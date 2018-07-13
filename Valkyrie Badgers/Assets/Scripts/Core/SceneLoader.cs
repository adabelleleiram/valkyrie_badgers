using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public event UnityAction onNewSceneLoading;

    public void LoadScene(SceneField aScene)
    {
    Debug.Log("here changing");
        SceneManager.LoadScene(aScene);
        if(onNewSceneLoading != null)
            onNewSceneLoading.Invoke();
    }
}
