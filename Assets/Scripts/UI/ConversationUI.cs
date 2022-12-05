using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConversationUI : MonoBehaviour
{
    public Animator animator;
    public GameObject exitButton;
    public Text answerField;
    public Image characterImage;
    public Text nameField;
    public ConversationManager conversationManager;
    public GameObject notification;
    public bool isOpen;
    public GameObject questionPrefab;
    public Transform questionsParent;
    public GameObject notebookButton;

    void Start()
    {
        exitButton.GetComponentInChildren<Button>().onClick.AddListener(() => conversationManager.leaveConversation());
        notification.SetActive(false);
        isOpen = false;
    }


    public void toggleUI()
    {
        if (!isOpen)
            animator.Play("openConversation");
        else if (isOpen)
            animator.Play("closeConversation");

        isOpen = !isOpen;
    }

    public void updateCharacterFields(Character character)
    {
        setCharacterImage(character);
        setCharacterName(character);
    }

    public void setCharacterImage(Character character)
        => characterImage.sprite = character.photo;

    public void setCharacterName(Character character)
        => nameField.text = character.firstName;

    public void updateAnswerField(Answer answer)
    {
        answerField.text = answer.sentence;
        answer.hasBeenSaid = true;
    }

    public void displayOpeningLine(Character character)
        => answerField.text = character.openingLine;

    public void displayNothingToSay(Character character)
        => answerField.text = character.nothingToSayLine;

    public void updateQuestionButtons()
    {
        clearQuestions();

        if (getAvailableQuestions().Count == 0)
        {
            print("no more questions");
            questionsParent.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
        }
        else
        {
            exitButton.gameObject.SetActive(false);
            questionsParent.gameObject.SetActive(true);

            foreach (Question q in getAvailableQuestions())
            {
                GameObject questionButton = GameObject.Instantiate(questionPrefab, Vector3.zero, Quaternion.identity, questionsParent);
                setQuestionButtonTextAndHandler(questionButton, q);
            }
        }
    }

    void setQuestionButtonTextAndHandler(GameObject button, Question question)
    {
        button.GetComponentInChildren<Text>().text = question.sentence;

        button.GetComponentInChildren<Button>().onClick.AddListener(()
            => conversationManager.askQuestion(question, conversationManager.currentConversationCharacter));
    }

    void clearQuestions()
    {
        foreach (Transform child in questionsParent.transform)
            GameObject.Destroy(child.gameObject);
    }



    List<Question> getAvailableQuestions() => conversationManager.currentlyAvailableQuestions;

    public IEnumerator showNotification(string message, float delay)
    {
        notification.GetComponent<Text>().text = message;
        notification.SetActive(true);
        yield return new WaitForSeconds(delay);
        notification.SetActive(false);
    }
}