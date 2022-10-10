using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public bool metBefore = false;
    public GameObject characterModel;
    public Character character;

    /// <summary>
    /// collection of functions to run, when the player meets the character for the first time.
    /// </summary>
    public void onFirstConversation()
    {
        print($"met character: {this.name} for the first time");
    }

    public void onTalkedTo()
    {
        if (metBefore)
        {
            // Do met before conversation
        }
        else
        {
            // Do first conversation
        }
    }

    public void OnMouseDown()
    {
        print($"clicked on character {this.name}");
    }

    /// <summary>
    /// run when the player meets the character
    /// adds the given character to the characters page in the notebook.
    /// </summary>
    /// <param name="character"></param>
    void addToCharactersPanel(Character character)
    {
        // charactersPanel.populateButton(this);
        print($"added {character} to characters panel");
    }
}
