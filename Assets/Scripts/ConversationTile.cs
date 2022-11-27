using UnityEngine;
using UnityEngine.UI;

public class ConversationTile : MonoBehaviour
{
    public Character character;
    public void updateText(Answer answer)
    {
        this.transform.GetChild(2).GetComponent<Text>().text = answer.sentence;
    }
}
