using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicHandler : MonoBehaviour
{
    #region Subclasses

    class LoopChange
    {
        public LoopChange(LoopTrack aTrack, bool aStart)
        {
            track = aTrack;
            play = aStart;
        }

        public LoopTrack track;
        public bool play = true;
    }

    class Transition
    {
        public enum State
        {
            StartingTransitionTracks,
            StoppingPrevious,
            WaitToStartNextLoops,
            StartingNextLoops,
            Done
        };

        public State state = State.Done;
        public LoopCollection nextCollection;

        public LoopTrack transitionBaseTrack;

        public List<LoopTrack> tracksToRemove = new List<LoopTrack>();
    }

    #endregion

    MusicLooper musicLooper;

    LoopCollection currentLoopCollection = null;

    LoopChange currentChange;
    Transition currentTransition = new Transition();

    public LoopCollection playingLoopCollection { get { return currentLoopCollection; } }


    void Start()
    {
        musicLooper = GetComponent<MusicLooper>();
        musicLooper.onLoopStarted += OnLoopStarted;
        musicLooper.onLoopStopped += OnLoopStopped;
    }

    private void Update()
    {
        if(currentTransition.state == Transition.State.WaitToStartNextLoops)
        {
            if (musicLooper.timeInLoop > musicLooper.loopFadeTime)
                PlayNextCollectionTracks();
        }
    }

    public void SetLoopCollection(LoopCollection aLoopCollection)
    {
        if (currentLoopCollection == aLoopCollection
            || currentTransition.state != Transition.State.Done)
            return;

        Debug.Log("GameMusicHandler: Setting loop collection: " + aLoopCollection.ToString());

        List<MusicLooper.PlayingTrack> playingTracks = musicLooper.activeTracks;

        if (playingTracks.Count == 0)
        {
            currentLoopCollection = aLoopCollection;
            Debug.Log("GameMusicHandler: No previous loop collection");


            foreach (LoopCollection.Track track in aLoopCollection.loopTracks)
            {
                AddTrack(track.loopTrack);
                if (track.weight == 1)
                    musicLooper.PlayTrack(track.loopTrack);
            }

            musicLooper.SetBaseTrack(aLoopCollection.loopBase);
        }
        else
        {
            currentTransition.tracksToRemove.Clear();
            currentChange = null;

            currentTransition.nextCollection = aLoopCollection;
            currentTransition.transitionBaseTrack = aLoopCollection.loopBase;
            if(currentLoopCollection != null 
                && currentLoopCollection.loopBase.clip.length < aLoopCollection.loopBase.clip.length)
                currentTransition.transitionBaseTrack = currentLoopCollection.loopBase;


            Debug.Log("GameMusicHandler: Setting transition base track: " + currentTransition.transitionBaseTrack.ToString());

            bool hasTransitionTrack = false;

            foreach (LoopCollection.Track track in aLoopCollection.loopTracks)
            {
                MusicLooper.PlayingTrack playingTrack = playingTracks.Find(x => x.loopTrack == track.loopTrack);
                if (playingTrack == null)
                {
                    AddTrack(track.loopTrack);
                }
                else if (!playingTrack.isPlaying)
                {
                    musicLooper.PlayTrack(track.loopTrack);
                    hasTransitionTrack = true;
                    Debug.Log("GameMusicHandler: Found transition track: " + track.ToString());
                }
            }

            if (hasTransitionTrack)
            {
                currentTransition.state = Transition.State.StartingTransitionTracks;
                Debug.Log("GameMusicHandler: State: " + Transition.State.StartingTransitionTracks.ToString());

            }
            else
            {
                StopTracksToRemove();
            }
        }
    }

    #region Callbacks

    void OnLoopStarted(LoopTrack aTrack)
    {
        if (currentChange != null && aTrack == currentChange.track)
            currentChange = null;

        if (currentTransition.state == Transition.State.StartingTransitionTracks)
        {
            StopTracksToRemove();
        }
        else if (currentTransition.state == Transition.State.StartingNextLoops
            && currentTransition.nextCollection.loopBase == aTrack)
        {
            CompleteTransition();
        }
    }

    void OnLoopStopped(LoopTrack aTrack)
    {
        if (currentChange != null && aTrack == currentChange.track)
            currentChange = null;

        if (currentTransition.state == Transition.State.StoppingPrevious)
        {
            if (currentTransition.tracksToRemove.Exists(x => x == aTrack))
                currentTransition.state = Transition.State.WaitToStartNextLoops;
        }
    }

    #endregion

    #region Calls
    public void TriggerMusicVariation()
    {
        if (currentChange != null || currentTransition.state != Transition.State.Done)
            return;

        List<LoopChange> potentialChanges = new List<LoopChange>();

        float highestChangeWeight = 0;
        int highestChangeIndex = -1;

        List<MusicLooper.PlayingTrack> playingTracks = musicLooper.activeTracks;
        for(int i = 0; i < playingTracks.Count; ++i)
        {
            MusicLooper.PlayingTrack playingTrack = playingTracks[i];

            float random = Random.Range(0.0f, 1.0f);
            float weight = currentLoopCollection.loopTracks.Find(x => x.loopTrack == playingTrack.loopTrack).weight;

            bool shouldPlay = random <= weight;
            if (playingTrack.isPlaying != shouldPlay)
            {
                potentialChanges.Add(new LoopChange(playingTrack.loopTrack, shouldPlay));
            }
            else //Guarantee a change
            {
                float weightToChange = playingTrack.isPlaying ? 1 - weight : weight;
                if (weightToChange > highestChangeWeight)
                {
                    highestChangeWeight = weightToChange;
                    highestChangeIndex = i;
                }
            }

        }

        if (potentialChanges.Count > 0)
        {
            int index = Random.Range(0, potentialChanges.Count);
            LoopChange change = potentialChanges[index];

            AddTrackChange(change.track, change.play);
        }
        else if (highestChangeIndex != -1)
        {
            MusicLooper.PlayingTrack track = playingTracks[highestChangeIndex];
            AddTrackChange(track.loopTrack, !track.isPlaying);
        }
    }

    #endregion

    #region Helpers
    void AddTrackChange(LoopTrack aTrack, bool aShouldPlay)
    {
        AddTrackChange(new LoopChange(aTrack, aShouldPlay));
    }

    void AddTrackChange(LoopChange aChange)
    {
        if (aChange.play)
            musicLooper.PlayTrack(aChange.track);
        else
            musicLooper.StopTrack(aChange.track);

        currentChange = aChange;
    }

    void AddTrack(LoopTrack aTrack)
    {
        musicLooper.AddTrack(aTrack);
    }

    void RemoveTrack(LoopTrack aTrack)
    {
        musicLooper.RemoveTrack(aTrack);
    }

    void PlayNextCollectionTracks()
    {
        Debug.Log("GameMusicHandler: PlayNextCollectionTracks");

        List<MusicLooper.PlayingTrack> playingTracks = musicLooper.activeTracks;
        foreach (MusicLooper.PlayingTrack playingTrack in playingTracks)
        {
            LoopCollection.Track track = currentTransition.nextCollection.loopTracks.Find(
                x => x.loopTrack == playingTrack.loopTrack);
            if (track != null)
            {
                if (track.weight == 1 && !playingTrack.isPlaying)
                    musicLooper.PlayTrack(playingTrack.loopTrack);
            }   
        }

        currentTransition.state = Transition.State.StartingNextLoops;
        Debug.Log("GameMusicHandler: State: " + Transition.State.StartingNextLoops.ToString());

    }

    void StopTracksToRemove()
    {
        Debug.Log("GameMusicHandler: Stopping tracks to remove");

        musicLooper.SetBaseTrack(currentTransition.transitionBaseTrack);

        bool tracksToStop = false;
        List<MusicLooper.PlayingTrack> playingTracks = musicLooper.activeTracks;
        foreach (MusicLooper.PlayingTrack playingTrack in playingTracks)
        {
            if (!currentTransition.nextCollection.loopTracks.Exists(x => x.loopTrack == playingTrack.loopTrack))
            {
                currentTransition.tracksToRemove.Add(playingTrack.loopTrack);

                if (playingTrack.isPlaying || playingTrack.isChanging)
                {
                    tracksToStop = true;
                    musicLooper.StopTrack(playingTrack.loopTrack);
                }
            }
        }

        currentTransition.state = Transition.State.StoppingPrevious;
        Debug.Log("GameMusicHandler: State: " + Transition.State.StoppingPrevious.ToString());

        if (!tracksToStop)
            PlayNextCollectionTracks();
    }

    void CompleteTransition()
    {
        Debug.Log("GameMusicHandler: CompleteTransition");

        currentTransition.state = Transition.State.Done;
        currentLoopCollection = currentTransition.nextCollection;
        musicLooper.SetBaseTrack(currentLoopCollection.loopBase);

        for (int i = currentTransition.tracksToRemove.Count - 1; i >= 0; --i)
        {
            RemoveTrack(currentTransition.tracksToRemove[i]);
        }
        currentTransition.tracksToRemove.Clear();
    }

    #endregion

}
