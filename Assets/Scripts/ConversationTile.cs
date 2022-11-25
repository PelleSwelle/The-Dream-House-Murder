using UnityEngine;
using UnityEngine.UI;

public class ConversationTile : MonoBehaviour
{
    public void updateAnswer(Answer answer)
    {
        this.transform.GetChild(2).GetComponent<Text>().text = answer.sentence;
    }
}
