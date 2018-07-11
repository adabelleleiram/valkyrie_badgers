using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnClick : MonoBehaviour {

  public string animObj;
	// Update is called once per frame

  void Start()
  {
    Animator anim = GetComponent<Animator>();
    anim.StopPlayback();
  }
	void OnMouseDown () {
    Animator anim = GetComponent<Animator>();
    anim.Play(animObj);
  }
}
