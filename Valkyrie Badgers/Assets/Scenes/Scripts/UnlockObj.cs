using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockObj : MonoBehaviour
{
  public LockedObj obj;

  public void Unlock()
  {
    obj.locked = false;
    GameHandler.changeables.Unlock(obj.gameObject);
  }
}
