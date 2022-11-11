using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public bool metBefore = false;
    public GameObject characterModel;
    public Character character;
    public CharactersPage charactersPage;

    /// <summary>
    /// collection of functions to run, when the player meets the character for the first time.
    /// </summary>
    public void onFirstConversation()
    {
        this.character.hasBeenTalkedTo = true;
        // add the character to the 
        charactersPage.add(this.character);
    }

    public void OnMouseDown()
    {
        print($"clicked on character {this.name}");
    }
}
