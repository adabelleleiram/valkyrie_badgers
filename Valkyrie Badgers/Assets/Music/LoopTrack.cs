using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loop Track", menuName = "Music/Loop Track")]
public class LoopTrack : ScriptableObject
{
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
}