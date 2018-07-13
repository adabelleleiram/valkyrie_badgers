using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

  private void OnMouseEnter()
  {
    MouseCursor.instance.SetCursor(MouseCursor.instance.zoomCursor);
  }
  void OnNewSceneLoading()
  {
    MouseCursor.instance.SetCursor(MouseCursor.instance.defaultCursor);
  }
}
