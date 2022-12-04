using UnityEngine;
using UnityEngine.UI;

public class MessengerTilePlayer : MonoBehaviour
{
    public Text text;
    public void setText(Question question)
    {
        text.text = question.sentence;
    }
}
