using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescOnClick : MonoBehaviour {
  public string descIn;
  public ItemDescription desc;

  public void ShowDesc()
  {
    Debug.Log("here");
    desc.SetDescription(descIn);
  }
}
