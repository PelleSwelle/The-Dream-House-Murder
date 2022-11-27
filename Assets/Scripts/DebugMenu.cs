using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DebugMenu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject charactersParent;
    public Button debugToggle;
    public GameObject debugMenu, characterSelect, objects;
    public Button talkBtn, pickupBtn;
    public Button[] personButtons;
    public ConversationManager conversationManager;
    public CharactersPage charactersPage;


    void OnValidate()
    {
        for (int i = 0; i < 0; i++)
        {
            // setting the text on the buttons
            personButtons[i].GetComponentInChildren<Text>().text = gameManager.characters[i].firstName;
        }
    }


    void Start()
    {
        debugMenu.SetActive(false);
        characterSelect.SetActive(false);

        setOnClickListeners();
    }

    void setOnClickListeners()
    {
        // whole menu
        debugToggle.GetComponent<Button>().onClick.AddListener(() => toggleDebug());

        // first layer
        Button talkButton = talkBtn.GetComponent<Button>();
        talkButton.onClick.AddListener(() => talk());

        Button pickupButton = pickupBtn.GetComponent<Button>();
        pickupButton.onClick.AddListener(() => pickup());

        personButtons[0].onClick.AddListener(() => conversationManager.initConversation(gameManager.mary));
        personButtons[1].onClick.AddListener(() => conversationManager.initConversation(gameManager.james));
        personButtons[2].onClick.AddListener(() => conversationManager.initConversation(gameManager.officer));
        personButtons[3].onClick.AddListener(() => conversationManager.initConversation(gameManager.harry));
    }

    void talk()
    {
        objects.SetActive(false);
        characterSelect.SetActive(true);
    }
    void pickup()
    {
        characterSelect.SetActive(false);
        objects.SetActive(true);
    }


    void toggleDebug()
    {
        if (debugMenu.activeSelf)
            debugMenu.SetActive(false);
        else if (!debugMenu.activeSelf)
            debugMenu.SetActive(true);
    }
}
