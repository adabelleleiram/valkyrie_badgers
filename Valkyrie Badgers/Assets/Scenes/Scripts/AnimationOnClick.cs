using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnClick : MonoBehaviour {

  public string trigger;
	// Update is called once per frame

	void OnMouseDown () {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger(trigger);
  }
}
