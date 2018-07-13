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

    GameMusicHandler gameMusicHandler;

    bool sceneBasedMusic = true;


    void Start()
    {
        gameMusicHandler = GetComponent<GameMusicHandler>();
        GameHandler.sceneLoader.onNewSceneLoading += OnSceneChange;

        // TEMP
        OnSceneChange(scenesForLoops[0].scenes[0]);
        // TEMP

    }

    void OnSceneChange(SceneField aScene)
    {
        if (sceneBasedMusic)
        {
            ScenesForLoop loop = scenesForLoops.Find(x => x.scenes.Exists(y => y.SceneName == aScene.SceneName));
            if(loop != null)
            {
                if (gameMusicHandler.playingLoopCollection == loop.loopCollection)
                    gameMusicHandler.TriggerMusicVariation();
                else
                    gameMusicHandler.SetLoopCollection(loop.loopCollection);
            }
        }
    }
}
