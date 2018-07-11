using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOverlay : MonoBehaviour {

    [HideInInspector]
    public GameObject currentZoomedObject;

    public void ZoomInOnObject(GameObject zoomObject)
    {
        if (currentZoomedObject)
            currentZoomedObject.SetActive(false);

        currentZoomedObject = zoomObject;

        currentZoomedObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        currentZoomedObject.SetActive(false);
    }
}
