using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// represention of a CONTINUOUS conversation between the player and a character.
/// managed by the ConversationManager
/// No need to include a character, because the conversationis implemented in the character class.
/// </summary>
public class Conversation
{
    public List<Question> questions;
    public List<Answer> answers;
    public UiHandler conversationUiHandler; // generic ui methods

    public Conversation(List<Question> qList, List<Answer> aList)
    {
        this.questions = qList;
        this.answers = aList;
    }
}
