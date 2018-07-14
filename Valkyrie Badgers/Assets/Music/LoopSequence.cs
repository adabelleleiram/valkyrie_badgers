using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loop Sequence", menuName = "Music/Loop Sequence")]
public class LoopSequence : ScriptableObject
{
    [System.Serializable]
    public class Sequence
    {
        public LoopSequenceTrigger trigger;
        public List<LoopTrack> loops = new List<LoopTrack>(); 
    }

    public List<Sequence> sequences;
    public LoopTrack baseLoop;
}