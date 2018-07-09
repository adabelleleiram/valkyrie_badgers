using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCombine : MonoBehaviour {
    [System.Serializable]
    public class OnCombine : UnityEvent { }

    public Item itemToCombineWith;
    public OnCombine onCombine;
    public bool deleteItem;

    bool itemEnteredCollider = false;

    private void OnMouseEnter()
    {
    //Kolla zoomed item osv här kanske?
        if(MouseCursor.instance.currentDraggedItem == itemToCombineWith)
            itemEnteredCollider = true;
    }

    private void OnMouseExit()
    {
        itemEnteredCollider = false;
    }

    private void Update()
    {
        if (itemEnteredCollider && Input.GetMouseButtonUp(0))
        {
            onCombine.Invoke();

            GameHandler.inventory.Remove(itemToCombineWith);
            if ( deleteItem )
                Destroy(gameObject);
        }
    }
}
