using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene_Valkyrie : MonoBehaviour
{
    public float fadeFromWhite;
    public float fadeToBlack;

    public float loopLengthMultiplier = 1;

    public SceneField nextScene;
    public LoopSequenceTrigger musicTrigger;

    public GameObject overlay;

    enum State
    {
        FadeFromWhite,
        ShowVideo,
        FadeToBlack
    }

    MusicHandler musicHandler;

    State state;

    float currentTimer;
    float loopLength;

    bool triggeredMusic = false;


    void Start()
    {
        musicHandler = GameHandler.musicHandler;

        loopLength = musicHandler.GetCurrentSequenceLength() * loopLengthMultiplier;

        SetState(State.FadeFromWhite);
    }

    void Update()
    {
        currentTimer += Time.deltaTime;

        switch (state)
        {
            case State.FadeFromWhite:
                UpdateFadeFromWhite();
                break;

            case State.ShowVideo:
                UpdateShowVideo();
                break;

            case State.FadeToBlack:
                UpdateFadeToBlack();
                break;
        }
    }

    void UpdateFadeFromWhite()
    {
        float alpha = 1.0f - Mathf.Clamp01(currentTimer / fadeFromWhite);
        overlay.GetComponent<Image>().color = new Vector4(1, 1, 1, alpha);

        if (alpha == 0)
            SetState(State.ShowVideo);
    }

    void UpdateShowVideo()
    {
        float loopTime = musicHandler.GetCurrentSequenceTime() % loopLength;
        if (loopLength - loopTime <= fadeToBlack)
            SetState(State.FadeToBlack);
    }

    void UpdateFadeToBlack()
    {
        float alpha = Mathf.Clamp01(currentTimer / fadeToBlack);
        overlay.GetComponent<Image>().color = new Vector4(0, 0, 0, alpha);

        if(!triggeredMusic && musicHandler.GetCurrentSequenceTime() > musicHandler.GetLoopFadeTime())
        {
            triggeredMusic = true;
            musicHandler.TriggerLoopSequence(musicTrigger);
        }

        if (alpha == 1)
        {
            GameHandler.sceneLoader.LoadScene(nextScene);
        }
    }

    void SetState(State aState)
    {
        state = aState;
        currentTimer = 0;
    }
}