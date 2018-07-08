using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {

    public GameObject defaultCursor;

    GameObject currentCursor;

    private void Start()
    {
        Cursor.visible = false;
        SetCursor(defaultCursor);
    }

    public void SetCursor(GameObject cursorObject)
    {
        if (currentCursor)
            currentCursor.SetActive(false);

        currentCursor = cursorObject;
        currentCursor.SetActive(true);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
