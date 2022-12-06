using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] cutscenes;
    public Button skipButton;
    public GameManager gameManager;
    public MusicPlayer musicPlayer;
    public GameObject[] uiElements;
    public ConversationUI conversationUi;
    public int numberOfScenesPlayed = 0;

    void Start() => skipButton.onClick.AddListener(() => skip());

    public void skip()
    {
        videoPlayer.Stop();
        skipButton.gameObject.SetActive(false);
        gameManager.setMode(GameMode.playMode);
        gameManager.activateUI();
        toggleForegroundElements(true);
        musicPlayer.play();
        numberOfScenesPlayed++;
        conversationUi.openUI();
    }

    public void playScene(int sceneNumber)
    {
        if (gameManager.isWithVideo)
        {
            conversationUi.closeUI();
            musicPlayer.stop();
            skipButton.gameObject.SetActive(true);
            videoPlayer.clip = cutscenes[sceneNumber];
            videoPlayer.Play();

            videoPlayer.loopPointReached += onEndVideo;
            toggleForegroundElements(false);
        }
        else return;
    }

    void toggleForegroundElements(bool open)
    {
        foreach (GameObject ui in uiElements)
            ui.SetActive(open);
    }

    void onEndVideo(VideoPlayer videoPlayer) => skip();
}
