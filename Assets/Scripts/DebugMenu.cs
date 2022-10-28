using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DebugMenu : MonoBehaviour
{
    public GameObject charactersParent;
    public Button debugToggle;
    public GameObject debugMenu, characterSelect, objects;
    public Button talkBtn, pickupBtn, generateBtn;
    public Button[] personButtons;
    public ConversationManager conversationManager;
    public Character[] characters;


    void OnValidate()
    {
        for (int i = 0; i < 0; i++)
        {
            // setting the text on the buttons
            personButtons[i].GetComponentInChildren<Text>().text = charactersParent.transform.GetChild(i).name;
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

        personButtons[0].onClick.AddListener(() => talkTo(characters[0]));
        personButtons[1].onClick.AddListener(() => talkTo(characters[1]));
        personButtons[2].onClick.AddListener(() => talkTo(characters[2]));
        personButtons[3].onClick.AddListener(() => talkTo(characters[3]));
        personButtons[4].onClick.AddListener(() => talkTo(characters[4]));
        personButtons[5].onClick.AddListener(() => talkTo(characters[5]));
    }

    void generatePlot()
    {
    }

    void talkTo(Character character)
    {
        // get the character in assets folder
        // string[] results = AssetDatabase.FindAssets(character.ToString());
        conversationManager.initConversation(character);
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
        print("debug menu toggle");
        if (debugMenu.activeSelf)
        {
            debugMenu.SetActive(false);
        }
        else if (!debugMenu.activeSelf)
        {
            debugMenu.SetActive(true);
            // bottom.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
