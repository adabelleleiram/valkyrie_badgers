using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool sequencedMusic = false;

    void Start()
    {
        gameMusicHandler = GetComponent<GameMusicHandler>();
        sequenceMusicPlayer = GetComponent<SequenceMusicPlayer>();

        GameHandler.sceneLoader.onNewSceneLoading += OnSceneChange;

        GameHandler.inventory.OnItemPickUp += TriggerMusicVariation;
        ItemCombine.GlobalOnCombine += TriggerMusicVariation;

        // TEMP
        OnSceneChange(scenesForLoops[0].scenes[0]);
        // TEMP
    }

    void OnSceneChange(SceneField aScene)
    {
        if (!sequencedMusic)
        {
            ScenesForLoop loop = scenesForLoops.Find(x => x.scenes.Exists(y => y.SceneName == aScene.SceneName));
            if(loop != null)
            {
                if (gameMusicHandler.playingLoopCollection != loop.loopCollection)
                    gameMusicHandler.SetLoopCollection(loop.loopCollection);
                else
                    gameMusicHandler.TriggerMusicVariation();
            }
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
