using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene_Sky : MonoBehaviour
{
    public float fadeFromBlack;
    public float videoShowTime;
    public LoopSequenceTrigger musicTrigger;
    public float orionFadeTime;

    public GameObject overlay;
    public Sprite creditsBg;

    public float minCreditsTime;
    public Credits credits;

    public SceneField mainMenu;

    enum State
    {
        FadeFromBlack,
        PlayVideo,
        FadeOutOrion,
        Credits
    }

    State state;
    float currentTimer;

    float creditsTime;
    bool musicTriggered = false;

    MusicHandler musicHandler;

    private void Start()
    {
        musicHandler = GameHandler.musicHandler;

        SetState(State.FadeFromBlack);
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;

        switch(state)
        {
            case State.FadeFromBlack:
                UpdateFadeFromBlack();
                break;

            case State.PlayVideo:
                UpdatePlayVideo();
                break;

            case State.FadeOutOrion:
                UpdateFadeOutOrion();
                break;

            case State.Credits:
                UpdateCredits();
                break;
        }
    }

    void UpdateFadeFromBlack()
    {
        float alpha = 1.0f - Mathf.Clamp01(currentTimer / fadeFromBlack);
        overlay.GetComponent<Image>().color = new Vector4(0, 0, 0, alpha);

        if (alpha == 0)
            SetState(State.PlayVideo);
    }

    void UpdatePlayVideo()
    {
        if(currentTimer >= videoShowTime)
        {
            musicHandler.TriggerLoopSequence(musicTrigger);
            overlay.GetComponent<Image>().sprite = creditsBg;
            SetState(State.FadeOutOrion);
        }
    }

    void UpdateFadeOutOrion()
    {
        float alpha = Mathf.Clamp01(currentTimer / orionFadeTime);
        overlay.GetComponent<Image>().color = new Vector4(1, 1, 1, alpha);

        if (alpha == 1.0f)
        {
            float loopLength = musicHandler.GetCurrentSequenceLength();
            float currentLoopTime = musicHandler.GetCurrentSequenceTime();

            float minimumLoops = currentLoopTime < musicHandler.GetLoopFadeTime() ? 1 : 2;

            float nrOfLoops = Mathf.Max(minimumLoops, Mathf.Ceil((currentLoopTime + minCreditsTime) / loopLength));
            creditsTime = nrOfLoops * loopLength - currentLoopTime;

            credits.StartCredits(creditsTime);

            SetState(State.Credits);
            musicHandler.TriggerLoopSequence(musicTrigger);
        }
    }

    void UpdateCredits()
    {
        if(!musicTriggered && currentTimer >= creditsTime - musicHandler.GetLoopFadeTime())
        {
            musicTriggered = true;
            musicHandler.TriggerLoopSequence(musicHandler.startSequence);
        }

        if (currentTimer >= creditsTime)
        {
            GameHandler.sceneLoader.LoadScene(mainMenu);
        }
    }

    void SetState(State aState)
    {
        state = aState;
        currentTimer = 0;
    }
}
