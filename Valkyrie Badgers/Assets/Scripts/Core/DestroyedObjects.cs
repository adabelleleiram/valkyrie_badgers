using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DestroyedObjects
{
  static List<GameObject> objs = new List<GameObject>();
  public static void Add(GameObject obj)
  {
     objs.Add(obj);
  }
}
