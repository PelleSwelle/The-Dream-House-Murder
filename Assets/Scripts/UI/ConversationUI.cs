using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    public UiHandler uiHandler;
    public Button[] questionButtons;
    public Text answerField;
    public Image characterImage;
    public Character character;
    public Conversation conversation;

    void Start()
    {
        setOnClickListeners();
    }

    void setOnClickListeners()
    {
        questionButtons[0].onClick.AddListener(askQuestion1);
        questionButtons[1].onClick.AddListener(askQuestion2);
        questionButtons[2].onClick.AddListener(askQuestion3);
        questionButtons[3].onClick.AddListener(askQuestion4);
    }


    public void setCharacterImage(Character character)
    {
        characterImage.sprite = character.photo;
    }
    public void populateAnswer(string answer)
    {
        answerField.text = answer;
    }

    /// <summary>
    /// populates the ConversationUI with the contents of the conversation
    /// </summary>
    /// <param name="questions"></param>
    /// <param name="answers"></param>
    public void populateConversation(string[] questions, string answer)
    {
        populateQuestions(questions);
        // TODO: implement the answers
    }
    /// <summary>
    /// populates the players text fields with the given questions
    /// </summary>
    /// <param name="questions"></param>
    public void populateQuestions(string[] questions)
    {
        for (int i = 0; i < questionButtons.Length; i++)
        {
            questionButtons[i].GetComponentInChildren<Text>().text = questions[i];
        }
    }
    public void askQuestion1()
    {
        print("wup 1");
    }
    public void askQuestion2()
    {
        print("wup 2");

    }
    public void askQuestion3()
    {
        print("wup 3");

    }
    public void askQuestion4()
    {
        print("wup 4");

    }
}