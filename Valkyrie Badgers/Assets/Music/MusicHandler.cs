using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    [System.Serializable]
    public class ScenesForLoop
    {
        public LoopCollection loopCollection;
        public List<SceneField> scenes;
    }

    public List<ScenesForLoop> scenesForLoops = new List<ScenesForLoop>();

    public LoopSequenceTrigger startSequence;

    GameMusicHandler gameMusicHandler;
    SequenceMusicPlayer sequenceMusicPlayer;

    bool sequencedMusic;

    public bool playingSequencedMusic { get { return sequencedMusic;  } }

    void Start()
    {
        gameMusicHandler = GetComponent<GameMusicHandler>();
        sequenceMusicPlayer = GetComponent<SequenceMusicPlayer>();

        GameHandler.sceneLoader.onNewSceneLoading += OnSceneChange;

        GameHandler.inventory.OnItemPickUp += (item => TriggerMusicVariation());
        ItemCombine.GlobalOnCombine += TriggerMusicVariation;

        TriggerLoopSequence(startSequence);
    }

    public void TriggerLoopSequence(LoopSequenceTrigger aTrigger)
    {
        sequenceMusicPlayer.TriggerMusicSequence(aTrigger);
        sequencedMusic = true;
    }

    public void TriggerGameMusic()
    {
        sequencedMusic = false;
        PlaySceneMusic(SceneManager.GetActiveScene().name);
    }

    void OnSceneChange(SceneField aScene)
    {
        if (!sequencedMusic)
        {
            PlaySceneMusic(aScene.SceneName);
        }
    }

    void PlaySceneMusic(string aSceneName)
    {
        ScenesForLoop loop = scenesForLoops.Find(x => x.scenes.Exists(y => y.SceneName == aSceneName));
        if (loop != null)
        {
            if (gameMusicHandler.playingLoopCollection != loop.loopCollection)
                gameMusicHandler.SetLoopCollection(loop.loopCollection);
            else
                gameMusicHandler.TriggerMusicVariation();
        }
    }

    void TriggerMusicVariation()
    {
        if(!sequencedMusic)
        {
            gameMusicHandler.TriggerMusicVariation();
        }
    }
}
