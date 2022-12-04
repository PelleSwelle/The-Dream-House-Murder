using UnityEngine;
using UnityEngine.UI;

public class ConversationTile : MonoBehaviour
{
    public Text text;
    public Character character;

    public void updateText(Answer answer)
    {
        // this.transform.GetChild(2).GetComponent<Text>().text = answer.sentence;
        text.text = answer.sentence;
    }
}
