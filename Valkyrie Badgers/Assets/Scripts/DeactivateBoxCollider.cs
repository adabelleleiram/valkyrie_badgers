using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateBoxCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
    gameObject.GetComponent<Collider>().enabled = false;
  }

  public void Revert()
  {
    gameObject.GetComponent<Collider>().enabled = true;
  }
}
