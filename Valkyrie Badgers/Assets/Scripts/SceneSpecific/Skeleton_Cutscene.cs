using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Cutscene : MonoBehaviour
{
    public LoopSequenceTrigger musicTrigger;
    
	public void TriggerCutscene()
    {
        GameHandler.musicHandler.TriggerLoopSequence(musicTrigger);
    }
}
