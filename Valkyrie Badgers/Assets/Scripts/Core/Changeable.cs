using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataChangable
{
  public DataChangable(string nameIn, bool activatedIn)
  {
    name = nameIn;
    activated = activatedIn;
    scene = SceneManager.GetActiveScene().buildIndex;
  }

  public string name = "";
  public bool activated = false;
  public int scene = 0;
}

public class Changeable : MonoBehaviour
{
  public List<DataChangable> list = new List<DataChangable>();

  public void ChangeActive(GameObject obj, bool active)
  {
    DataChangable data = new DataChangable(obj.name, active);
    list.Add(data);
  }

  public void UpdateState(GameObject obj)
  {
    int scene = SceneManager.GetActiveScene().buildIndex;
    foreach( DataChangable data in list )
    {
      if (data.name == obj.name && data.scene == scene )
      {
        obj.SetActive(data.activated);
      }
    }
  }
}