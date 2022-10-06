using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    Character character;
    // Start is called before the first frame update
    void Awake()
    {
        character = ScriptableObject.CreateInstance<Character>();
    }

    /// <summary>
    /// collection of functions to run, when the player meets the character for the first time.
    /// </summary>
    public void onFirstConversation()
    {
        // addToCharactersPanel(this);
    }
    public void setIndex(int index)
    {
        // this.index = index;
    }

    public void OnMouseDown()
    {
        Debug.Log($"clicked on character {this.name}");
    }

    /// <summary>
    /// run when the player meets the character
    /// adds the given character to the characters page in the notebook.
    /// </summary>
    /// <param name="character"></param>
    void addToCharactersPanel(Character character)
    {
        // charactersPanel.populateButton(this);
        Debug.Log($"added {character} to characters panel");
    }
}
