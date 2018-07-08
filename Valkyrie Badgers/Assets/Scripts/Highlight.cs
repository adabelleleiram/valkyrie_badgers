using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour {

  public Sprite sprite;
  public Sprite highlight;
  private SpriteRenderer spriteR;

  void Start()
  {
    spriteR = gameObject.GetComponent<SpriteRenderer>();
  }

  void OnMouseOver()
  {
    if ( spriteR.sprite != highlight )
      spriteR.sprite = highlight;
  }

  void OnMouseExit()
  {
    spriteR.sprite = sprite;
  }
}
