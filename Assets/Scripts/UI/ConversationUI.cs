using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    public UiHandler uiHandler;
    public Button[] questionButtons;
    // CanvasGroups for the whole UI, the player and the character
    public CanvasGroup conversationGroup, playerGroup, characterGroup;
    public GameObject player, character;
    public Conversation conversation;

    void Start()
    {
        player = GameObject.Find("Player");
        questionButtons = getQuestionButtons();

        questionButtons[0].onClick.AddListener(askQuestion1);
        questionButtons[1].onClick.AddListener(askQuestion2);
        questionButtons[2].onClick.AddListener(askQuestion3);
        questionButtons[3].onClick.AddListener(askQuestion4);
        // CANVAS GROUPS
        setConversationInactive();
    }

    /// <summary>
    /// gets the buttons attached to the player panel in the onversation UI
    /// </summary>
    /// <returns>Array of buttons</returns>
    public Button[] getQuestionButtons()
    {
        // make an array to hold the text fields
        Button[] buttons = new Button[this.transform.GetChild(1).childCount];
        print("number of question buttons: " + this.transform.GetChild(1).childCount);
        // run through 
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = playerGroup.transform.GetChild(i).GetComponent<Button>();
        }
        return buttons;
    }

    /// <summary>
    /// populates the ConversationUI with the contents of the conversation
    /// </summary>
    /// <param name="questions"></param>
    /// <param name="answers"></param>
    public void populateConversation(string[] questions, string[] answers)
    {
        populatePlayerFields(questions);
        // TODO: implement the answers
    }
    /// <summary>
    /// populates the players text fields with the given questions
    /// </summary>
    /// <param name="questions"></param>
    public void populatePlayerFields(string[] questions)
    {
        // make an array for holding the buttons
        Button[] buttons = getQuestionButtons();

        // for each question, put that question into the corresponding buttons textfield
        for (int i = 0; i < questions.Length; i++)
        {
            questionButtons[i].transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = questions[i];
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

    public void setConversationActive()
    {
        conversationGroup.alpha = 1f;
        conversationGroup.blocksRaycasts = true;
    }
    public void setConversationInactive()
    {
        conversationGroup.alpha = 0f;
        conversationGroup.blocksRaycasts = false;
    }

    public void toggleConversation()
    {
        // if (!this.isOpen)
        // {
        //     setConversationActive();
        // }
        // else if (this.isOpen)
        // {
        //     setConversationInactive();
        // }
    }

}