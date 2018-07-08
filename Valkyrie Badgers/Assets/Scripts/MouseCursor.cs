using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour {

    public static MouseCursor instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public GameObject defaultCursor;
    public GameObject itemCursor;

    GameObject currentCursor;

    private void Start()
    {
        Cursor.visible = false;
        SetCursor(defaultCursor);
    }

    public void DragItem(Item item)
    {
        SetCursor(itemCursor, item.icon);
    }

    public void SetCursor(GameObject cursorObject, Sprite sprite = null)
    {
        if (currentCursor)
            currentCursor.SetActive(false);

        currentCursor = cursorObject;
        currentCursor.SetActive(true);

        if (sprite)
            currentCursor.GetComponent<Image>().sprite = sprite;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
