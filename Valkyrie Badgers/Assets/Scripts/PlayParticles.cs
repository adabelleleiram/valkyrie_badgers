using UnityEngine;
using System.Collections;

public class PlayParticles : MonoBehaviour 
{
  public string triggerName;

  void OnMouseDown()
  {
    Animator anim = GetComponent<Animator>();
    anim.SetTrigger(triggerName);
  }
}