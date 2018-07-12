using UnityEngine;
using UnityEngine.Events;

public class OnChangeScene : MonoBehaviour
{
  [System.Serializable]
  public class DestroyEvent : UnityEvent { }

  public DestroyEvent onDestroy;
  bool clicked = false;

  private void OnDestroy()
  {
    if (clicked)
    {
      onDestroy.Invoke();
      GameHandler.persistencyManager.SetActiveState(gameObject);
    }
  }

  public void SetClicked()
  {
    clicked = true;
  }
}
