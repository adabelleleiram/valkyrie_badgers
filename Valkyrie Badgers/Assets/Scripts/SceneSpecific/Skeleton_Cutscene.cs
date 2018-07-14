using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Cutscene : MonoBehaviour
{
    public LoopSequenceTrigger musicTrigger;
    public SceneField finalScene;

    public List<GameObject> wings = new List<GameObject>();

    public float minFeatherFadeInTime;
    public float wingShowTime;
    public float fadeToWhiteTime;
    
	public void TriggerCutscene()
    {
        GameHandler.musicHandler.TriggerLoopSequence(musicTrigger);
    }
}
