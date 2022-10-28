using UnityEditor.Experimental.GraphView;
/// <summary>
/// A node in the dialogue editor, representing a sentence
/// </summary>
public class DialogueNode : Node
{
    public string GUID;

    public string dialogueText;

    public bool isEntryPoint = false;
}
