using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessengerTileCharacter : MonoBehaviour
{
    public Text text;
    public Image image;
    public void setText(Answer answer)
    {
        text.text = answer.sentence;
    }

    public void setImage(Character character)
    {
        image.sprite = character.photo;
    }
}
