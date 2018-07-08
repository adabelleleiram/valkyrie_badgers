﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventListener : MonoBehaviour {

  int ClickRangeDoors = 100;
  void Update () {
		if (Input.GetMouseButtonDown(0))
    {
      float MousePosx = Input.mousePosition.x;
      int Scene = SceneManager.GetActiveScene().buildIndex;
      if ( MousePosx < ClickRangeDoors && Scene > 0 )
      {
        SceneManager.LoadScene(Scene - 1);
      }
      else if ( MousePosx > Screen.width - ClickRangeDoors && Scene + 1 < SceneManager.sceneCount )
      {
        SceneManager.LoadScene(Scene + 1);
      }
    }
	}
}
