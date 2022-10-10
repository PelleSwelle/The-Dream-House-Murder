using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public ConversationUI conversationUiHandler;
    public GameObject conversationUi;
    public Character character;
    [SerializeField] Conversation[] conversations;

    void Start()
    {
        deactivateUI();
    }

    void toggleConversationUi()
    {
        if (conversationUi.activeSelf)
        {
            conversationUi.SetActive(false);
        }
        else if (!conversationUi.activeSelf)
        {
            conversationUi.SetActive(true);
        }
    }
    void activateUI()
    {
        conversationUi.SetActive(true);
    }
    void deactivateUI()
    {
        conversationUi.SetActive(false);
    }



    public void initConversation(Character character)
    {

        Conversation conversation = ScriptableObject.CreateInstance<Conversation>();
        // TODO: use conversation to hold all the data.
        conversation.character = character;
        print($"started a conversation with {character}");

        activateUI();
        populateAnswer($"Hello there. I am {character.firstName}");
        setCharacterImage(character);
        populateQuestions();
    }

    void populateQuestions()
    {
        conversationUiHandler.populateQuestions(standardLines);
    }
    void setCharacterImage(Character character)
    {
        conversationUiHandler.setCharacterImage(character);
    }
    void populateAnswer(string answer)
    {
        conversationUiHandler.populateAnswer(answer);
    }

    string[] standardLines = new string[]
    {
        // TODO make these real lines, that actually further the story
        "Did you do it?",
        "Are you in a relationship with anyone?",
        "Do you have any bad habits?",
        "what are your hobbies?"
    };
}
