using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {

    public Door door;
	
    public void Unlock()
    {
        door.locked = false;
    }
}
