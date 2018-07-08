using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public int nextSceneIndex;
    public bool locked = false;

    private void Start()
    {
        //Persistence för dörrar (så att den inte är låst när man kommer tillbaka)
    }

    private void OnMouseEnter()
    {
        //ändra mus-ikon (kolla locked)
    }

    private void OnMouseUpAsButton()
    {
        if (!locked)
            SceneManager.LoadScene(nextSceneIndex);
    }
}
