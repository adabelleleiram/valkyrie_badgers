using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSequenceTrigger : MonoBehaviour
{
    [System.Serializable]
    public class ItemRequirement
    {
        public Item item;
        public int count = 1;

        [HideInInspector]
        public bool completed = false;
    };

    public LoopSequenceTrigger firstLoopTrigger;
    public List<ItemRequirement> itemRequirements = new List<ItemRequirement>();

    public LoopSequenceTrigger secondLoopTrigger;
    public List<SceneField> sceneRequirements = new List<SceneField>();

	// Use this for initialization
	void Start ()
    {
        GameHandler.inventory.OnItemPickUp += OnItemPickUp;
    }

    private void OnDestroy()
    {
    }

    void OnItemPickUp(Item anItem)
    {
        ItemRequirement requirement = itemRequirements.Find(x => x.item == anItem);
        if (requirement == null)
            return;

        if(!requirement.completed && requirement.count <= anItem.counter)
        {
            requirement.completed = true;

            bool completed = true;
            foreach(ItemRequirement ir in itemRequirements)
            {
                completed &= ir.completed;
            }

            if(completed)
            {
                GameHandler.musicHandler.TriggerLoopSequence(firstLoopTrigger);

                GameHandler.inventory.OnItemPickUp -= OnItemPickUp;
                GameHandler.sceneLoader.onNewSceneLoading += OnSceneChange;
                CheckSceneRequirement(SceneManager.GetActiveScene().name);
            }
        }
    }

    void OnSceneChange(SceneField aScene)
    {
        CheckSceneRequirement(aScene.SceneName);
    }

    void CheckSceneRequirement(string aSceneName)
    {
        if (sceneRequirements.Exists(x => x.SceneName == aSceneName))
        {
            GameHandler.musicHandler.TriggerLoopSequence(secondLoopTrigger);

            GameHandler.sceneLoader.onNewSceneLoading -= OnSceneChange;
            Destroy(this);
        }
    }
}
