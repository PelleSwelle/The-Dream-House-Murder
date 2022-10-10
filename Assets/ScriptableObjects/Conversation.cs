using UnityEngine;

/// <summary>
/// The conversation class is where all references to the character and the lines are.
/// the conversationUI is simply where it is displayed
/// all the ui stuff is called from here
/// </summary>
[CreateAssetMenu(menuName = "Conversation/Conversation")]
public class Conversation : ScriptableObject
{
    public string characterOpeningLine;
    public Character character;
    public string[] questions;
    public string[] answers;
    public UiHandler conversationHandler;
}
