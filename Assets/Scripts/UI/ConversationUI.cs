using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationUI : MonoBehaviour
{
    public UiHandler uiHandler;
    public Button[] questionButtons;

    public Button exitButton;
    public Text answerField;
    public Image characterImage;
    public Character character;
    public Conversation conversation;
    public ConversationManager conversationManager;
    public GameObject notification;

    void Start()
    {
        setOnClickListeners();
        notification.SetActive(false);
    }

    /// <summary>
    /// sets the on clicklisteners on the question buttons
    /// </summary>
    void setOnClickListeners()
    {
        questionButtons[0].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[0]));
        questionButtons[1].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[1]));
        questionButtons[2].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[2]));
        questionButtons[3].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[3]));
        exitButton.onClick.AddListener(() => conversationManager.endConversation());
    }

    /// <summary>
    /// sets the image for the character on the UI
    /// </summary>
    /// <param name="character"></param>
    public void setCharacterImage(Character character)
    {
        characterImage.sprite = character.photo;
    }

    /// <summary>
    /// populates the answer text for the answer
    /// </summary>
    /// <param name="answer"></param>
    public void updateAnswerField(Answer answer)
    {
        answerField.text = answer.line;
    }

    /// <summary>
    /// populates the players text fields with the given questions
    /// </summary>
    /// <param name="questions"></param>
    public void updateQuestionButtons(Question[] questions)
    {
        if (questions.Length > 4)
            throw new System.Exception("there were more than 4 questions");

        else if (questions.Length < 4)
            throw new System.Exception("There were less than 4 questions");

        else
            for (int i = 0; i < questionButtons.Length; i++)
            {
                questionButtons[i].GetComponentInChildren<Text>().text = questions[i].line;
            }
    }

    /// <summary>
    /// Show a notification with the given message for a given delay
    /// </summary>
    /// <param name="message"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public IEnumerator showNotification(string message, float delay)
    {
        notification.GetComponent<Text>().text = message;
        notification.SetActive(true);
        yield return new WaitForSeconds(delay);
        notification.SetActive(false);
    }
}