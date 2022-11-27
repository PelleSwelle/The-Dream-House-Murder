using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConversationUI : MonoBehaviour
{
    public GameObject conversationObject;

    public Button exitButton;
    public Text answerField;
    public Image characterImage;
    public Character character;
    public ConversationManager conversationManager;
    public GameObject notification;
    public bool isOpen;
    public GameObject questionPrefab;
    public Transform questionsParent;

    void Start()
    {
        exitButton.onClick.AddListener(() => conversationManager.leaveConversation());
        notification.SetActive(false);
    }

    /// <summary>
    /// toggles the active state of the conversation UI
    /// </summary>
    void toggleUI()
    {
        if (conversationObject.activeSelf)
        {
            conversationObject.SetActive(false);
            this.isOpen = true;
            // conversationManager.isInConversation = false;
        }
        else if (conversationObject.activeSelf)
        {
            conversationObject.SetActive(true);
            this.isOpen = false;
            // conversationManager.isInConversation = true;
        }
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
        answerField.text = answer.sentence;
        answer.hasBeenSaid = true;
    }

    public void displayOpeningLine(Character character)
    {
        answerField.text = character.openingLine;
    }


    /// <summary>
    /// populates the players text fields with the currently available questions
    /// </summary>
    /// <param name="questions"></param>
    public void updateQuestionButtons()
    {
        clearQuestions();

        foreach (Question q in getAvailableQuestions())
        {
            // instantiate button and set parent
            GameObject questionButton = GameObject.Instantiate(questionPrefab, Vector3.zero, Quaternion.identity, questionsParent);

            // set text on button
            questionButton.GetComponentInChildren<Text>().text = q.sentence;

            // add onclick listener
            questionButton.GetComponent<Button>().onClick.AddListener(()
                => conversationManager.askQuestion(q, conversationManager.talkPartner));
        }
    }


    /// <summary>
    /// clears the question boxes.
    /// </summary>
    void clearQuestions()
    {
        int numberOfQuestions = questionsParent.childCount;

        foreach (Transform child in questionsParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }


    List<Question> getAvailableQuestions()
    {
        print($"UI received {conversationManager.currentlyAvailableQuestions.Count} questions");
        return conversationManager.currentlyAvailableQuestions;
    }


    /// <summary>
    /// Show a notification with the given message for a given delay
    /// </summary>
    /// <param name="message"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public IEnumerator showNotification(string message, float delay)
    {
        print($"notification showing: {message}");
        notification.GetComponent<Text>().text = message;
        notification.SetActive(true);
        yield return new WaitForSeconds(delay);
        notification.SetActive(false);
    }
}