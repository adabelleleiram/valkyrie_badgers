using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene_Skeleton : MonoBehaviour
{

    public GameObject wings;
    public GameObject white;

    public float minWingsFadeIn;
    public float wingsShowTime;
    public float minFadeToWhiteTime;

    public LoopSequenceTrigger musicTrigger;
    public SceneField nextScene;

    enum State
    {
        FadingInWings,
        ShowingWings,
        FadingToWhite
    }

    State state;
    float currentTimer;

    float wingFadeTime;
    float fadeToWhiteTime;
    bool musicTriggered = false;

    MusicHandler musicHandler;

    // Use this for initialization
    void Start()
    {
        musicHandler = GameHandler.musicHandler;

        float loopLength = musicHandler.GetCurrentSequenceLength();
        float currentLoopTime = musicHandler.GetCurrentSequenceTime();

        float minCutsceneTime = minWingsFadeIn + wingsShowTime + fadeToWhiteTime;

        float nrOfLoops = Mathf.Ceil((currentLoopTime + minCutsceneTime) / loopLength);
        float totalCutsceneTime = nrOfLoops * loopLength - currentLoopTime;

        float timeDiff = totalCutsceneTime - minCutsceneTime;

        wingFadeTime = minWingsFadeIn + (timeDiff * 0.5f);
        fadeToWhiteTime = minFadeToWhiteTime + (timeDiff * 0.5f);

        SetState(State.FadingInWings);
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        switch(state)
        {
            case State.FadingInWings:
                UpdateFadeInWings();
                break;

            case State.ShowingWings:
                UpdateShowingWings();
                break;

            case State.FadingToWhite:
                UpdateFadeToWhite();
                break;
        }
    }

    void UpdateFadeInWings()
    {
        float alpha = Mathf.Clamp01(currentTimer / wingFadeTime);

        wings.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, alpha);

        if (alpha == 1)
            SetState(State.ShowingWings);
    }

    void UpdateShowingWings()
    {
        if (currentTimer >= wingsShowTime)
            SetState(State.FadingToWhite);
    }

    void UpdateFadeToWhite()
    {
        float alpha = Mathf.Clamp01(currentTimer / fadeToWhiteTime);
        white.GetComponent<Image>().color = new Vector4(1, 1, 1, alpha);

        if(!musicTriggered)
        {
            float fadeTimeLeft = fadeToWhiteTime - currentTimer;
            if (fadeTimeLeft < musicHandler.GetCurrentSequenceLength() - musicHandler.GetLoopFadeTime())
            {
                musicTriggered = true;
                musicHandler.TriggerLoopSequence(musicTrigger);
            }
        }

        if (alpha == 1)
            GameHandler.sceneLoader.LoadScene(nextScene);

    }

    void SetState(State aState)
    {
        state = aState;
        currentTimer = 0;
    }
}
