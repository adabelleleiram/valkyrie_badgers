using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockedObj : MonoBehaviour
{
  public int nextSceneIndex;
  public bool locked = false;

  private void Start()
  {
   // GameHandler.changeables.UpdateUnlocked(this);
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
