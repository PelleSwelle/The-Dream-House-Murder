using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public GameObject conversationUi;
    public Character character;
    public Player player;
    [SerializeField] Conversation[] conversations;


    void toggleConversationUi()
    {
        if (conversationUi.activeInHierarchy)
        {
            conversationUi.SetActive(false);
        }
        else if (!conversationUi.activeInHierarchy)
        {
            conversationUi.SetActive(true);
        }
    }

    public void initConversation(Character character)
    {
        Conversation conversation = ScriptableObject.CreateInstance<Conversation>();
        // conversation.answers
        print($"started a conversation with {character}");
    }
}
