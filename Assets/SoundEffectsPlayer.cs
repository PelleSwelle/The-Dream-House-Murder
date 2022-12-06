using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    AudioSource source;
    public AudioClip pageSound, bookSound;

    void Awake() => source = GetComponent<AudioSource>();

    void OnEnable()
    {
        Notebook.onPageTurn += playPageSound;
        NotebookButton.onToggle += playBookSound;
    }
    void OnDisable()
    {
        Notebook.onPageTurn -= playPageSound;
        NotebookButton.onToggle -= playBookSound;
    }
    void playPageSound() => source.PlayOneShot(pageSound);
    void playBookSound() => source.PlayOneShot(bookSound);
}
