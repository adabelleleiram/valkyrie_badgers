using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistencyManager : MonoBehaviour
{
    class GameObjectID
    {
        public GameObjectID(GameObject aGameObject)
        {
            id = SceneManager.GetActiveScene().buildIndex.ToString() + aGameObject.name;
        }
        string id;
    }
    class SaveGame
    {
        public Dictionary<GameObjectID, bool> activeState; 
        public Dictionary<GameObjectID, bool> lockedState; 
        public Dictionary<GameObjectID, Vector3> positionState;
    }
	

}
