using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
    public ZoomOverlay zoomOverlay;
    public GameObject zoomedObject;

    public void OnMouseDown()
    {
        zoomOverlay.ZoomInOnObject(zoomedObject);
    }

    
}
