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

  public string name;
  public bool activated = false;
  public int scene = -1;
}

public class Unlocked
{
  public Unlocked(string nameIn)
  {
    name = nameIn;
    scene = SceneManager.GetActiveScene().buildIndex;
  }

  public string name;
  public int scene = -1;
}

public class Changeable : MonoBehaviour
{
  List<DataChangable> list = new List<DataChangable>();
  List<Unlocked> unlockedItems = new List<Unlocked>();
  public int numUnlocked = 0;

  public void ChangeActive(GameObject obj, bool active)
  {
    DataChangable data = new DataChangable(obj.name, active);
    list.Add(data);
  }

  public void Unlock(GameObject obj)
  {
    Unlocked item = new Unlocked(obj.name);
    unlockedItems.Add(item);
    numUnlocked++;
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

  public void UpdateUnlocked(LockedObj obj)
  {
    int scene = SceneManager.GetActiveScene().buildIndex;
    foreach (Unlocked unlocked in unlockedItems)
    {
      if (unlocked.name == obj.name && unlocked.scene == scene)
      {
        obj.locked = false;
      }
    }
  }
}