using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
  new public string name = "New Item";
  public Sprite icon = null;
  public bool isDefaultItem = false;
  public string description = "Desc";

  public void Use()
  {
    Debug.Log("Using Item");
  }

  public void SetPosition( Vector2 position )
  {
    SetPosition(position);
  }
}
