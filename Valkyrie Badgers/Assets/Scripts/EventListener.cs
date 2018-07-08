using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventListener : MonoBehaviour {

  int clickRangeDoors = 100;
  void Update () {
		if (Input.GetMouseButtonDown(0))
    {
      float mousePosx = Input.mousePosition.x;
      int scene = SceneManager.GetActiveScene().buildIndex;
      if (mousePosx < clickRangeDoors && scene > 0 )
      {
        SceneManager.LoadScene(scene - 1);
      }
      else if (mousePosx > Screen.width - clickRangeDoors && scene + 1 < SceneManager.sceneCount )
      {
        SceneManager.LoadScene(scene + 1);
      }
    }
	}
}
