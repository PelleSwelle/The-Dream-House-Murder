using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The conversation class is where all references to the character and the lines are.
/// the conversationUI is simply where it is displayed
/// all the ui stuff is called from here
/// </summary>
public class Conversation
{
    public string characterOpeningLine;
    Character character;
    Player player;
    ConversationUI ui;

    public string[] questions;
    public string[] answers;
    public Conversation(GameObject _character)
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        character = _character.GetComponent<Character>();
        questions = player.standardLines;
        answers = character.lines;

        // populate UI
        ui = GameObject.Find("ConversationPanel").GetComponent<ConversationUI>();
        ui.populateConversation(this.questions, this.answers);
        displayConversation();
    }

    public void displayConversation()
    {
        ui.setConversationActive();
    }
}
