using UnityEngine;
using System.Collections;

public class ClickPlayAnimation : MonoBehaviour 
{
  public string triggerName;

  void OnMouseDown()
  {
    Animator anim = GetComponent<Animator>();
    anim.SetTrigger(triggerName);
  }
}