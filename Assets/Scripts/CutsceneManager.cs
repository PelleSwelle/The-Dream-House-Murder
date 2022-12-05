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

    void Start() => skipButton.onClick.AddListener(() => skip());

    public void skip()
    {
        videoPlayer.Stop();
        skipButton.gameObject.SetActive(false);
        gameManager.setMode(GameMode.playMode);
        gameManager.activateUI();
    }

    public void playScene(int sceneNumber)
    {
        musicPlayer.stop();
        if (gameManager.isWithVideo)
        {
            skipButton.gameObject.SetActive(true);
            videoPlayer.clip = cutscenes[sceneNumber];
            videoPlayer.Play();

            videoPlayer.loopPointReached += onEndVideo;
            foreach (GameObject ui in uiElements)
                ui.SetActive(false);
        }
    }

    void onEndVideo(VideoPlayer videoPlayer)
    {
        gameManager.activateUI();
        musicPlayer.play();
    }
}
