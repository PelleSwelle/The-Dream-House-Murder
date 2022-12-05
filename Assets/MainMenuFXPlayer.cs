using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFXPlayer : MonoBehaviour
{
    AudioSource source;
    public AudioClip bookSound, leafSound;
    void Awake() => source = GetComponent<AudioSource>();
    void OnEnable()
    {
        MainMenuController.onButtonPress += playLeafSound;
        MainMenuController.onOpen += playBookSound;
    }

    void OnDisable()
    {
        MainMenuController.onButtonPress -= playLeafSound;
        MainMenuController.onOpen -= playBookSound;
    }

    void playLeafSound() => source.PlayOneShot(leafSound);

    void playBookSound() => source.PlayOneShot(bookSound);

}
