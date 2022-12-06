using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{
    AudioSource source;
    public AudioClip[] maleClips, femaleClips;
    void Awake() => source = GetComponent<AudioSource>();
    void OnEnable() => ConversationManager.onSpeak += playRandomVoiceClip;
    void OnDisable() => ConversationManager.onSpeak -= playRandomVoiceClip;
    private void playRandomVoiceClip(ICharacter character)
    {
        source.Stop();

        if (character.gender == "female")
            playRandomFemaleVoiceClip();
        else if (character.gender == "male")
            playRandomMaleVoiceClip();
    }

    public void playRandomFemaleVoiceClip()
    {
        int i = Random.Range(0, femaleClips.Length);
        source.PlayOneShot(femaleClips[i]);
    }

    public void playRandomMaleVoiceClip()
    {
        int i = Random.Range(0, maleClips.Length);
        source.PlayOneShot(maleClips[i]);
    }
}
