using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollbutton : MonoBehaviour {

  public ScrollRect myScrollRect;
  public GameObject itemsHolder;
  // Use this for initialization
  public void Right () {
    myScrollRect.horizontalNormalizedPosition = Mathf.Clamp01( myScrollRect.horizontalNormalizedPosition + 1f/3f ) + 1f;
  }

  // Update is called once per frame
  public void Left () {
    myScrollRect.horizontalNormalizedPosition = Mathf.Clamp01( myScrollRect.horizontalNormalizedPosition - 1f/3f );
  }
}
