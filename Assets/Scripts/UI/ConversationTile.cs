using UnityEngine;
using UnityEngine.UI;

public class ConversationTile : MonoBehaviour
{
    public Text text;
    public Character character;

    public void updateText(Answer answer)
    {
        text.text = answer.sentence;
    }
}
