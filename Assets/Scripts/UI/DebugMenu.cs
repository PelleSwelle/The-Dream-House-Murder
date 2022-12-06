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
    public Text maryAct, jamesAct, officerAct, harryAct;

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

    void Update()
    {
        maryAct.text = $"Mary act: {gameManager.mary.currentAct.actNumber}";
        harryAct.text = $"Harry act: {gameManager.harry.currentAct.actNumber}";
        jamesAct.text = $"James act: {gameManager.james.currentAct.actNumber}";
        officerAct.text = $"Officer act: {gameManager.officer.currentAct.actNumber}";
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

        personButtons[0].onClick.AddListener(() => conversationManager.initConversation(GameObject.Find("Mary").GetComponent<ICharacter>()));
        personButtons[1].onClick.AddListener(() => conversationManager.initConversation(GameObject.Find("Harry").GetComponent<ICharacter>()));
        personButtons[2].onClick.AddListener(() => conversationManager.initConversation(GameObject.Find("Officer").GetComponent<ICharacter>()));
        personButtons[3].onClick.AddListener(() => conversationManager.initConversation(GameObject.Find("James").GetComponent<ICharacter>()));
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
