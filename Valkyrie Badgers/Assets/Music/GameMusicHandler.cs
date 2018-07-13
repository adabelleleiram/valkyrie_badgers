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
    }

    #endregion

    MusicLooper musicLooper;

    LoopCollection currentLoopCollection = null;
    List<LoopState> loopStates = new List<LoopState>();

    LoopChange currentChange;
    Transition currentTransition = new Transition();

    public LoopCollection playingLoopCollection { get { return currentLoopCollection; } }


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

        Debug.Log("GameMusicHandler: Setting loop collection: " + aLoopCollection.ToString());

        if (currentLoopCollection == null)
        {
            currentLoopCollection = aLoopCollection;
            Debug.Log("GameMusicHandler: No previous loop collection");


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
            currentTransition.transitionBaseTrack = currentLoopCollection.loopBase.clip.length < aLoopCollection.loopBase.clip.length
                ? aLoopCollection.loopBase : currentLoopCollection.loopBase;

            Debug.Log("GameMusicHandler: Setting transition base track: " + currentTransition.transitionBaseTrack.ToString());

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
            if (currentTransition.tracksToRemove.Exists(x => x.track.loopTrack == aTrack))
                PlayNextCollectionTracks();
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
        Debug.Log("GameMusicHandler: PlayNextCollectionTracks");

        foreach (LoopState state in loopStates)
        {
            if(currentTransition.nextCollection.loopTracks.Exists(x => x == state.track))
            {
                if (state.track.weight == 1 && !state.isPlaying)
                    musicLooper.PlayTrack(state.track.loopTrack);
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
        foreach (LoopState state in loopStates)
        {
            if (!currentTransition.nextCollection.loopTracks.Exists(x => x.loopTrack == state.track.loopTrack))
            {
                currentTransition.tracksToRemove.Add(state);

                if (state.isPlaying || state.isChanging)
                {
                    tracksToStop = true;
                    musicLooper.StopTrack(state.track.loopTrack);
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
            RemoveTrack(currentTransition.tracksToRemove[i].track);
        }
        currentTransition.tracksToRemove.Clear();
    }

    #endregion

}
