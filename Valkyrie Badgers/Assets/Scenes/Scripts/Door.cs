using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public SceneField nextScene;
    public bool locked = false;

    private void OnMouseUpAsButton()
    {
        if (!locked)
            GameHandler.sceneLoader.LoadScene(nextScene);
    }

    public void Unlock()
    {
        locked = false;
    }

    private void OnMouseEnter()
    {
        //ändra mus-ikon (kolla locked)
    }
}
