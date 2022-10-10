using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player
public class Player : MonoBehaviour
{
    // the player has an instance of the notebook
    public Notebook notebook;
    // list of characters known
    public Character[] charactersKnown;
    CanvasGroup conversationPanel;
    CharactersPage charactersPanel;
    public UiHandler uiHandler;

    public string[] standardLines;


    void Start()
    {

    }

    /// <summary>
    /// Start a conversation with a character
    /// </summary>
    /// <param name="character"></param>
    void initiateConversation(GameObject _character)
    {
        // get the character component from the GameObject
        Character character = _character.GetComponent<Character>();

        // check wether they have been talked to before
        if (!character.hasBeenTalkedTo)
        {
            // TODO: this is done in the notebook, remove this
            addCharacterToNotebook();
            // character.onFirstConversation();
        }
        // Conversation conversation = new Conversation(_character);
        // pop up the conversation UI with an image of the character
        // conversation.displayConversation();
    }
    void addCharacterToNotebook()
    {
        print("NOT IMPLEMENTED: addCharacterToNotebook");
    }
}
