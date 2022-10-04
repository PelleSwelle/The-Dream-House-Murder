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
    CharactersPanel charactersPanel;
    public UiComponent uiComponent;

    public string[] standardLines;


    void Start()
    {
        charactersPanel = GameObject.Find("CharactersPanel").GetComponent<CharactersPanel>();
        conversationPanel = GameObject.Find("ConversationPanel").GetComponent<CanvasGroup>();
        standardLines = new string[]
        {
            // TODO make these real lines, that actually further the story
            "Did you do it?",
            "Are you in a relationship with anyone?",
            "Do you have any bad habits?",
            "what are your hobbies?"
        };
    }

    void Update()
    {
        testInput();
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
            character.onFirstConversation();
        }
        Conversation conversation = new Conversation(_character);
        // pop up the conversation UI with an image of the character
        conversation.displayConversation();
    }
    void addCharacterToNotebook()
    {
        print("NOT IMPLEMENTED: addCharacterToNotebook");
    }

    /// <summary>
    /// calls the toggleNotebook method of Notebook
    /// </summary>
    void toggleNotebook()
    {
        this.notebook.toggleNotebook();
    }
    /// <summary>
    /// testing functionality since XR makes it difficult to test in development.
    /// </summary>
    void testInput()
    {
        // CONVERSATION WITH AVERAGE BOB
        if (Input.GetKeyDown(KeyCode.A))
        {
            // TODO find another way to get the character
            initiateConversation(GameObject.Find("averageBob"));
        }
        // CHARACTERS
        else if (Input.GetKeyDown(KeyCode.U))
        {
            this.uiComponent.goToPage(this.notebook.pages[0]);
        }
        // CLUES
        else if (Input.GetKeyDown(KeyCode.I))
        {
            this.uiComponent.goToPage(this.notebook.pages[1]);
        }
        // CONVERSATIONS
        else if (Input.GetKeyDown(KeyCode.O))
        {
            this.uiComponent.goToPage(this.notebook.pages[2]);
        }
        // BIO
        else if (Input.GetKeyDown(KeyCode.P))
        {
            this.uiComponent.goToPage(this.notebook.pages[3]);
        }

        // TOGGLE NOTEBOOK
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            this.toggleNotebook();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            charactersPanel.populateButton(GameObject.Find("averageBob").GetComponent<Character>());
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            charactersPanel.populateButton(GameObject.Find("shortKathy").GetComponent<Character>());
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            charactersPanel.populateButton(GameObject.Find("oldReginald").GetComponent<Character>());
        }
    }
}
