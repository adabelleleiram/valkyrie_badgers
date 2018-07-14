using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescOnClick : MonoBehaviour {
  public string descIn;
  public ItemDescription desc;

  private void OnMouseUpAsButton()
  {
    SetDesc();
  }

  public void SetDesc()
  {
    desc.SetDescription(descIn);
  }
}
