using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class MusicLooper : MonoBehaviour
{
    [HideInInspector]
    public event UnityAction<LoopTrack> onLoopStarted;
    [HideInInspector]
    public event UnityAction<LoopTrack> onLoopStopped;

    public AudioMixerGroup mixer;

    public class PlayingTrack
    {
        public AudioSource audioSource;
        public LoopTrack loopTrack;

        public bool isPlaying { get { return audioSource.volume == targetVolume && targetVolume > 0; } }
        public bool isChanging { get { return fadeStarted || audioSource.volume != targetVolume; } }

        public float targetVolume = 0;

        public bool fadeStarted = false;
    }

    public List<PlayingTrack> activeTracks { get { return playingTracks; } }

    List<PlayingTrack> playingTracks = new List<PlayingTrack>();
    PlayingTrack baseTrack;

    const float bpm = 120;
    const float fadeTime = 60 / bpm * 2;  

    #region Calls

    public void AddTrack(LoopTrack aTrack)
    {
        PlayingTrack pt = new PlayingTrack();
        pt.audioSource = GetNewAudioSource(aTrack);
        pt.loopTrack = aTrack;

        playingTracks.Add(pt);
    }

    public void RemoveTrack(LoopTrack aTrack)
    {
        PlayingTrack pt = playingTracks.Find(x => x.loopTrack == aTrack);
        if (pt == null)
        {
            Debug.LogError("Trying to remove track that hasn't been added: " + aTrack.ToString());
            return;
        }

        if (pt == baseTrack)
        {
            Debug.LogError("Trying to remove base track [" + aTrack.ToString() + "], replace with another track first!");
            return;
        }

        Destroy(pt.audioSource);
        playingTracks.Remove(pt);
    }

    public void PlayTrack(LoopTrack aTrack)
    {
        PlayingTrack pt = playingTracks.Find(x => x.loopTrack == aTrack);
        if (pt == null)
        {
            Debug.LogError("Trying to play track that hasn't been added: " + aTrack.ToString());
            return;
        }

        pt.targetVolume = pt.loopTrack.volume;
    }

    public void StopTrack(LoopTrack aTrack)
    {
        PlayingTrack pt = playingTracks.Find(x => x.loopTrack == aTrack);
        if (pt == null)
        {
            Debug.LogError("Trying to stop track that hasn't been added: " + aTrack.ToString());
            return;
        }

        pt.targetVolume = 0;
    }

    public void SetBaseTrack(LoopTrack aTrack)
    {
        PlayingTrack pt = playingTracks.Find(x => x.loopTrack == aTrack);
        if (pt == null)
        {
            Debug.LogError("Trying to set base track that hasn't been added: " + aTrack.ToString());
            return;
        }

        baseTrack = pt;
    }

    #endregion

    private void Update()
    {
        for (int i = 0; i < playingTracks.Count; ++i)
        {
            PlayingTrack pt = playingTracks[i];
            if (pt.audioSource.volume != pt.targetVolume)
            {
                if (!pt.fadeStarted)
                {
                    if (pt.targetVolume == 0)
                    {
                        float timeDiff = baseTrack.loopTrack.clip.length - baseTrack.audioSource.time;
                        pt.fadeStarted = timeDiff <= fadeTime;
                    }
                    else
                    {
                        pt.fadeStarted = baseTrack.audioSource.time <= fadeTime;
                    }
                }

                if (pt.fadeStarted == true)
                {
                    float volumeFactor = 0;

                    if (pt.targetVolume == 0)
                    {
                        float timeDiff = baseTrack.loopTrack.clip.length - baseTrack.audioSource.time;
                        volumeFactor = Mathf.Clamp01(timeDiff / fadeTime);

                        if (volumeFactor * pt.loopTrack.volume > pt.audioSource.volume) //Wrapping
                            volumeFactor = 0;
                    }
                    else
                    {
                        volumeFactor = Mathf.Clamp01(baseTrack.audioSource.time / fadeTime);
                    }

                    pt.audioSource.volume = pt.loopTrack.volume * volumeFactor;
                }

                if (pt.audioSource.volume == pt.targetVolume)
                {
                    pt.fadeStarted = false;

                    if (pt.targetVolume > 0)
                        onLoopStarted.Invoke(pt.loopTrack);
                    else
                        onLoopStopped.Invoke(pt.loopTrack);
                }
            }
        }
    }


    #region Helper functions
    AudioSource GetNewAudioSource(LoopTrack aLoopTrack)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = aLoopTrack.clip;
        audioSource.volume = 0;
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = mixer;
        audioSource.time = baseTrack == null ? 0 : baseTrack.audioSource.time % aLoopTrack.clip.length;

        audioSource.Play();

        return audioSource;
    }

    public PlayingTrack GetPlayingTrackIfActive(LoopTrack aTrack)
    {
        return playingTracks.Find(x => x.loopTrack == aTrack);
    }

    #endregion
}
