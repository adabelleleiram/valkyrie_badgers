using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMusicPlayer : MonoBehaviour
{
    public List<LoopSequence> loopSequences;

    MusicLooper musicLooper;

    List<LoopTrack> currentTracksForSequence = new List<LoopTrack>();
    List<LoopTrack> currentTracksToRemove = new List<LoopTrack>();

    LoopSequence currentLoopSequence;

    private void Start()
    {
        musicLooper = GetComponent<MusicLooper>();
    }

    public void TriggerMusicSequence(LoopSequenceTrigger aTrigger)
    {
        LoopSequence loopSequence = loopSequences.Find(x => x.sequences.Exists(y => y.trigger == aTrigger));
        if (loopSequences == null)
            return;

        List<MusicLooper.PlayingTrack> playingTracks = musicLooper.activeTracks;
        currentTracksForSequence.Clear();
        currentTracksToRemove.Clear();

        AddSequenceTracks(loopSequence, aTrigger);
        musicLooper.SetBaseTrack(loopSequence.baseLoop);
        currentLoopSequence = loopSequence;

        foreach (MusicLooper.PlayingTrack playingTrack in playingTracks)
        {
            if (!currentTracksForSequence.Exists(x => x == playingTrack.loopTrack))
            {
                currentTracksToRemove.Add(playingTrack.loopTrack);
                musicLooper.StopTrack(playingTrack.loopTrack);
            }
        }

        if (currentTracksToRemove.Count == 0)
            PlaySequenceTracks();
        else
            musicLooper.onLoopStopped += OnLoopStopped;
    }

    void OnLoopStopped(LoopTrack aTrack)
    {
        if (currentTracksToRemove.Exists(x => x == aTrack))
        {
            currentTracksToRemove.Remove(aTrack);
            musicLooper.RemoveTrack(aTrack);

            if (currentTracksToRemove.Count == 0)
            {
                PlaySequenceTracks();
                musicLooper.onLoopStopped -= OnLoopStopped;
            }
        }
    }

    void AddSequenceTracks(LoopSequence aSequence, LoopSequenceTrigger aTrigger)
    {
        List<MusicLooper.PlayingTrack> playingTracks = musicLooper.activeTracks;

        foreach (LoopSequence.Sequence sequence in aSequence.sequences)
        {
            foreach (LoopTrack track in sequence.loops)
            {
                if (!playingTracks.Exists(x => x.loopTrack == track))
                    musicLooper.AddTrack(track);
                currentTracksForSequence.Add(track);
            }

            if (sequence.trigger == aTrigger)
                break;
        }
    }

    void PlaySequenceTracks()
    {
        foreach (LoopTrack track in currentTracksForSequence)
        {
            musicLooper.PlayTrack(track);
        }
    }

    public float GetCurrentSequenceLength()
    {
        if (currentLoopSequence == null)
            return 0;

        return currentLoopSequence.baseLoop.clip.length;
    }

    public float GetCurrentSequenceTime()
    {
        if (currentLoopSequence == null)
            return 0;

        MusicLooper.PlayingTrack playingTrack = musicLooper.activeTracks.Find(x => x.loopTrack == currentLoopSequence.baseLoop);
        if (playingTrack == null)
            return 0;

        return playingTrack.audioSource.time;
    }
}
