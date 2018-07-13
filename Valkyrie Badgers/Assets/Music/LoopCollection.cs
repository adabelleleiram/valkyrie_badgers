using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loop Collection", menuName = "Music/Loop Collection")]
public class LoopCollection : ScriptableObject
{
    [System.Serializable]
    public class Track
    {
        public Track(LoopTrack aLoopTrack)
        {
            loopTrack = aLoopTrack;
        }

        public LoopTrack loopTrack;

        [Range(0.0f, 1.0f)]
        public float weight = 1.0f;
    }

    public List<Track> loopTracks = new List<Track>();
    public LoopTrack loopBase;
}