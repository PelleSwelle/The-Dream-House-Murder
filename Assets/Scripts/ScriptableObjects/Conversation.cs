using UnityEngine;

/// <summary>
/// represention of a conversation between the player and the given character
/// managed by the ConversationManager
/// </summary>
public class Conversation
{
    public string characterOpeningLine;
    public Character character;
    public Question[] questions;
    public Answer[] answers; // TODO: these sshould come from the character
    public UiHandler conversationHandler;
    public int numberOfQuestionsAsked;
    public int numberOfAnswers;


    public Question currentQuestion;
    public Answer currentAnswer;

    public Conversation(Character _character)
    {
        this.character = _character;
    }
}
