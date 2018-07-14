using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene001_MusicChanger : MonoBehaviour {

    public LoopSequenceTrigger trigger;
    public GameObject activeObject;

    private void Start()
    {
        if(activeObject.activeInHierarchy)
            GameHandler.musicHandler.TriggerLoopSequence(trigger);
    }

    public void TriggerGameMusic()
    {
        GameHandler.musicHandler.TriggerGameMusic();
    }
}
