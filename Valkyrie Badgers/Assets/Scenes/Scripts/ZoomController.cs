using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
  public GameObject zoomObject;
  public Image image;
  public Sprite sprite;
  public void OnMouseDown()
  {
    GameHandler.changeables.ChangeActive(zoomObject, true);
    zoomObject.SetActive(true);
    image.sprite = sprite;
  }

  public void OnClose()
  {
    zoomObject.SetActive(false);
  }
}
