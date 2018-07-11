using UnityEngine;
using System.Collections;

public class ClickPlayAnimation : MonoBehaviour 
{
  public string triggerName;

  void OnMouseDown()
  {
    Debug.Log("Here");
    Animator anim = GetComponent<Animator>();
    anim.SetTrigger(triggerName);
  }
}