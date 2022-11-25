using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: maybe delete this?
public class CharacterHandler : MonoBehaviour
{
    public Character character;
    public CharactersPage charactersPage;

    public GameObject conversationTile;

    void Update()
    {
        // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     print($"touch position: {touch.position}, character: {this.character.firstName}");
        // }
    }

    /// <summary>
    /// collection of functions to run, when the player meets the character for the first time.
    /// </summary>
    // public void onFirstConversation()
    // {
    //     this.character.hasBeenTalkedTo = true;
    //     // add the character to the 
    //     charactersPage.add(this.character);
    // }

    // public void OnMouseDown()
    // {
    //     print($"clicked on character {this.name}");
    // }

    // public Character getCharacter()
    // {
    //     return this.character;
    // }
}
