using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombine : MonoBehaviour {

    Item itemToCombineWith;

    private void OnMouseEnter()
    {
        //Kolla zoomed item osv här kanske?

        itemToCombineWith = MouseCursor.instance.currentDraggedItem;      
    }

    private void OnMouseExit()
    {
        itemToCombineWith = null;
    }

    private void Update()
    {
        if (itemToCombineWith && Input.GetMouseButtonUp(0))
        {
            GameHandler.inventory.Remove(itemToCombineWith);
            Destroy(gameObject);
        }
    }
}
