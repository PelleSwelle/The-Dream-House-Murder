using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    public VideoClip[] cutScenes;
    public VideoPlayer videoPlayer;

    public void playScene(int clipNumber)
    {
        print("Playing video");
        videoPlayer.clip = cutScenes[clipNumber];
        videoPlayer.Play();
    }
}
