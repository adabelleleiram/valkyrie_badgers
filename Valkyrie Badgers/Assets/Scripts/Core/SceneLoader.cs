using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public event UnityAction onNewSceneLoading;

    public void LoadScene(SceneField aScene)
    {
        SceneManager.LoadScene(aScene);
        if(onNewSceneLoading != null)
            onNewSceneLoading.Invoke();
    }
}
