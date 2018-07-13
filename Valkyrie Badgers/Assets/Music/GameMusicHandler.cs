using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicHandler : MonoBehaviour
{
    #region Subclasses

    class LoopState
    {
        public LoopState(LoopCollection.Track aTrack)
        {
            track = aTrack;
        }

        public LoopCollection.Track track;
        public bool isPlaying = false;
        public bool isChanging = false;
    }

    class LoopChange
    {
        public LoopChange(LoopCollection.Track aTrack, bool aStart)
        {
            track = aTrack;
            play = aStart;
        }

        public LoopCollection.Track track;
        public bool play = true;
    }

    class Transition
    {
        public enum State
        {
            StartingTransitionTracks,
            StoppingPrevious,
            StartingNextLoops,
            Done
        };

        public State state = State.Done;
        public LoopCollection nextCollection;

        public LoopTrack transitionBaseTrack;

        public List<LoopState> tracksToRemove = new List<LoopState>();
        public int tracksToStop = 0;
    }

    #endregion

    MusicLooper musicLooper;

    LoopCollection currentLoopCollection = null;
    List<LoopState> loopStates = new List<LoopState>();

    LoopChange currentChange;
    Transition currentTransition = new Transition();

    void Start()
    {
        musicLooper = GetComponent<MusicLooper>();
        musicLooper.onLoopStarted += OnLoopStarted;
        musicLooper.onLoopStopped += OnLoopStopped;
    }

    public void SetLoopCollection(LoopCollection aLoopCollection)
    {
        if (currentLoopCollection == aLoopCollection
            || currentTransition.state != Transition.State.Done)
            return;

        if (currentLoopCollection == null)
        {
            currentLoopCollection = aLoopCollection;

            foreach (LoopCollection.Track track in aLoopCollection.loopTracks)
            {
                AddTrack(track);
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
            currentTransition.tracksToStop = 0;
            currentTransition.transitionBaseTrack = currentLoopCollection.loopBase.clip.length < aLoopCollection.loopBase.clip.length
                ? aLoopCollection.loopBase : currentLoopCollection.loopBase;


            bool hasTransitionTrack = false;

            foreach (LoopCollection.Track track in aLoopCollection.loopTracks)
            {
                LoopState state = loopStates.Find(x => x.track.loopTrack == track.loopTrack);
                if (state == null)
                {
                    AddTrack(track);
                }
                else if (!state.isPlaying)
                {
                    musicLooper.PlayTrack(track.loopTrack);
                    hasTransitionTrack = true;
                }
            }

            if (hasTransitionTrack)
            {
                currentTransition.state = Transition.State.StartingTransitionTracks;
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
        LoopState state = loopStates.Find(x => x.track.loopTrack == aTrack);

        if (state != null)
        {
            state.isPlaying = true;
            state.isChanging = false;
        }

        if (currentChange != null && aTrack == currentChange.track.loopTrack)
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
        LoopState state = loopStates.Find(x => x.track.loopTrack == aTrack);
        if (state != null)
        {
            state.isPlaying = false;
            state.isChanging = false;
        }

        if (currentChange != null && aTrack == currentChange.track.loopTrack)
            currentChange = null;

        if (currentTransition.state == Transition.State.StoppingPrevious)
        {
            LoopState trackState = currentTransition.tracksToRemove.Find(x => x.track.loopTrack == aTrack);
            if (trackState != null)
            {
                --currentTransition.tracksToStop;
                if (currentTransition.tracksToStop == 0)
                    PlayNextCollectionTracks();
            }
        }
    }

    #endregion

    #region Calls
    public void TriggerMusicChange()
    {
        if (currentChange != null || currentTransition.state != Transition.State.Done)
            return;

        List<LoopChange> potentialChanges = new List<LoopChange>();

        float highestChangeWeight = 0;
        int highestChangeIndex = -1;

        for (int i = 0; i < loopStates.Count; ++i)
        {
            LoopState state = loopStates[i];

            float random = Random.Range(0.0f, 1.0f);
            bool shouldPlay = random <= state.track.weight;
            if (state.isPlaying != shouldPlay)
            {
                potentialChanges.Add(new LoopChange(state.track, shouldPlay));
            }
            else //Guarantee a change
            {
                float weightToChange = state.isPlaying ? 1 - state.track.weight : state.track.weight;
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
            LoopState state = loopStates[highestChangeIndex];

            AddTrackChange(state.track, !state.isPlaying);
        }
    }

    #endregion

    #region Helpers
    void AddTrackChange(LoopCollection.Track aTrack, bool aShouldPlay)
    {
        AddTrackChange(new LoopChange(aTrack, aShouldPlay));
    }

    void AddTrackChange(LoopChange aChange)
    {
        if (aChange.play)
            musicLooper.PlayTrack(aChange.track.loopTrack);
        else
            musicLooper.StopTrack(aChange.track.loopTrack);

        currentChange = aChange;
        loopStates.Find(x => x.track == aChange.track).isChanging = true;
    }

    void AddTrack(LoopCollection.Track aTrack)
    {
        musicLooper.AddTrack(aTrack.loopTrack);
        loopStates.Add(new LoopState(aTrack));
    }

    void RemoveTrack(LoopCollection.Track aTrack)
    {
        musicLooper.RemoveTrack(aTrack.loopTrack);
        loopStates.RemoveAt(loopStates.FindIndex(x => x.track == aTrack));
    }

    void PlayNextCollectionTracks()
    {
        foreach (LoopState state in loopStates)
        {
            if(currentTransition.nextCollection.loopTracks.Exists(x => x == state.track))
            {
                if (state.track.weight == 1 && !state.isPlaying)
                    musicLooper.PlayTrack(state.track.loopTrack);
            }   
        }

        currentTransition.state = Transition.State.StartingNextLoops;
    }

    void StopTracksToRemove()
    {
        musicLooper.SetBaseTrack(currentTransition.transitionBaseTrack);

        foreach (LoopState state in loopStates)
        {
            if (!currentTransition.nextCollection.loopTracks.Exists(x => x.loopTrack == state.track.loopTrack))
            {
                currentTransition.tracksToRemove.Add(state);

                if (state.isPlaying || state.isChanging)
                {
                    ++currentTransition.tracksToStop;
                    musicLooper.StopTrack(state.track.loopTrack);
                }
            }
        }

        currentTransition.state = Transition.State.StoppingPrevious;

        if (currentTransition.tracksToStop == 0)
            PlayNextCollectionTracks();
    }

    void CompleteTransition()
    {
        currentTransition.state = Transition.State.Done;
        currentLoopCollection = currentTransition.nextCollection;
        musicLooper.SetBaseTrack(currentLoopCollection.loopBase);

        for (int i = currentTransition.tracksToRemove.Count - 1; i >= 0; --i)
        {
            RemoveTrack(currentTransition.tracksToRemove[i].track);
        }
        currentTransition.tracksToRemove.Clear();
    }

    #endregion

}
