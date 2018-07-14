using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
  new public string name = "New Item";
  public Sprite icon = null;
  public bool isDefaultItem = false;
  public string description = "Desc";
  [HideInInspector]
  public int counter = 0;

  public void SetPosition( Vector2 position )
  {
    SetPosition(position);
  }
}
