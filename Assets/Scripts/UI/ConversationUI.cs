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
    public Conversation conversation;
    public ConversationManager conversationManager;
    public GameObject notification;
    public bool isOpen;
    public GameObject questionPrefab;
    public Transform questionsParent;

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
        // questionButtons[0].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[0]));
        // questionButtons[1].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[1]));
        // questionButtons[2].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[2]));
        // questionButtons[3].onClick.AddListener(() => conversationManager.onChooseQuestion(conversationManager.currentlyAvailableQuestions[3]));
        exitButton.onClick.AddListener(() => conversationManager.leaveConversation());
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
            conversationManager.isInConversation = false;
        }
        else if (conversationObject.activeSelf)
        {
            conversationObject.SetActive(true);
            this.isOpen = false;
            conversationManager.isInConversation = true;
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


    /// <summary>
    /// populates the players text fields with the currently available questions
    /// </summary>
    /// <param name="questions"></param>
    public void updateQuestionButtons()
    {
        clearQuestions();
        foreach (Question question in getAvailableQuestions())
        {
            print("update ui with: " + question.sentence);
            // instantiate button and set parent
            GameObject questionButton = GameObject.Instantiate(questionPrefab, Vector3.zero, Quaternion.identity, questionsParent);
            // set text on button
            questionButton.GetComponentInChildren<Text>().text = question.sentence;
            // add onclick listener
            questionButton.GetComponent<Button>().onClick.AddListener(() => conversationManager.askQuestion(question, conversationManager.conversationPartner));
        }
    }


    void clearQuestions()
    {
        int numberOfQuestions = questionsParent.childCount;
        print("questionsParent.count: " + questionsParent.childCount);

        foreach (Transform child in questionsParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    List<Question> getAvailableQuestions()
    {
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
        notification.GetComponent<Text>().text = message;
        notification.SetActive(true);
        yield return new WaitForSeconds(delay);
        notification.SetActive(false);
    }
}