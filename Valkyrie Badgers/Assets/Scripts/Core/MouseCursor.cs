using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
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
    public GameObject leftDoorCursor;
    public GameObject rightDoorCursor;
    public GameObject itemCursor;
    public GameObject zoomCursor;
    public GameObject combineCursor;

    [HideInInspector]
    public Item currentDraggedItem;

    GameObject currentCursor;
    GameObject nextCursor;

    private void Start()
    {
        Cursor.visible = false;
        SetCursor(defaultCursor);
    }

    public void DragItem(Item item)
    {
        currentDraggedItem = null;

        SetCursor(itemCursor, item.icon);

        currentDraggedItem = item;
        nextCursor = defaultCursor;
    }

    public void SetCursor(GameObject cursorObject, Sprite sprite = null)
    {
        if (currentDraggedItem)
        {
            nextCursor = cursorObject;
            return;
        }

        if (currentCursor)
        {
            currentCursor.SetActive(false);
            if (currentDraggedItem)
                currentDraggedItem = null;
        }

        currentCursor = cursorObject;
        currentCursor.SetActive(true);
        currentDraggedItem = null;

        if (sprite)
            currentCursor.GetComponent<Image>().sprite = sprite;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
        if (currentDraggedItem && Input.GetMouseButtonUp(0))
        {
            currentDraggedItem = null;
            SetCursor(nextCursor);
        }
    }
}
