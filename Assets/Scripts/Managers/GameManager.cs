using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject charactersParent;
    public List<Character> characters;
    public GameObject cursor;
    public Character mary, james, officer, harry;
    public GameObject officerPrefab, maryPrefab, jamesPrefab, harryPrefab;
    public Sprite officerPhoto, maryPhoto, jamesPhoto, harryPhoto;
    public AudioSource musicSource;
    public AudioClip gameMusic;
    public GameMode currentMode;
    public Button acceptScaleButton, notebookButton;
    public ArManager arManager;
    public int charactersDone = 0;

    public void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }
    void Start()
    {
        mary = new Character(
            _firstName: "Mary",
            _photo: maryPhoto,
            _model: maryPrefab,
            _openingLine: "*sob* What?",
            _gender: "female",
            _nothingToSay: "I think, you should talk to your colleague first."
        );
        james = new Character(
            _firstName: "James",
            _photo: jamesPhoto,
            _model: jamesPrefab,
            _openingLine: "Who are you? What do you want?",
            _gender: "male",
            _nothingToSay: "Shouldn't you talk to the officer first?"
        );
        officer = new Character(
            _firstName: "Officer",
            _photo: officerPhoto,
            _model: officerPrefab,
            _openingLine: "I'm glad you could get here so fast. What do you need to know?",
            _gender: "male",
            _nothingToSay: "I don't really have anything more to tell you at the moment."
        );
        harry = new Character(
            _firstName: "Harry",
            _photo: harryPhoto,
            _model: harryPrefab,
            _openingLine: "What is it?",
            _gender: "male",
            _nothingToSay: "I am not going to give you the entire story. Go get the basics from the officer."
        );

        loadActs();

        characters = new List<Character> { mary, james, officer, harry };
        print("loaded characters: " + characters.Count);

        assignCharacterHandlers();
        foreach (Character c in characters)
            print(c.firstName);
        cursor.SetActive(true);
        acceptScaleButton.onClick.AddListener(() => handleAccept());
        // numberOfCharactersPlaced = 0;


        musicSource.clip = gameMusic;
        musicSource.Play();

        setMode(GameMode.tutorialMode);
    }

    void loadActs()
    {
        mary.loadActs(
            new Act(1, new SingleRunConversation(Constants.maryQuestions)),
            new Act(2, new SingleRunConversation(Constants.maryAct2))
        );
        james.loadActs(
            new Act(1, new SingleRunConversation(Constants.jamesQuestions)),
            new Act(2, new SingleRunConversation(Constants.jamesAct2))
        );
        officer.loadActs(
            new Act(1, new SingleRunConversation(Constants.officerQuestions)),
            new Act(2, new repeatableConversation(Constants.officerAct2))
        );
        harry.loadActs(
            new Act(1, new SingleRunConversation(Constants.harryQuestions)),
            new Act(2, new SingleRunConversation(Constants.harryAct2))
        );
    }

    void Update()
    {
        if (currentMode == GameMode.placementMode)
        {
            arManager.placeOnTap();
        }

        else if (currentMode == GameMode.scalingMode)
        {
            arManager.rotateOnDrag();
            arManager.scaleOnPinch();
        }

        else if (currentMode == GameMode.playMode)
        {
            arManager.tipManager.tutorialOverlay.SetActive(false);

            arManager.initConversationOnTap();
        }
    }

    public bool actIsOverForAll()
    {
        return mary.currentAct.isFinished()
        && officer.currentAct.isFinished()
        && harry.currentAct.isFinished()
        && james.currentAct.isFinished();
    }

    public void setMode(GameMode _gameMode)
    {
        currentMode = _gameMode;
        print($"Entered: {currentMode}");
        acceptScaleButton.gameObject.SetActive(currentMode == GameMode.scalingMode);
        notebookButton.gameObject.SetActive(currentMode == GameMode.playMode);
    }

    void assignCharacterHandlers()
    {
        charactersParent.transform.GetChild(0).GetComponent<CharacterHandler>().character = officer;
        charactersParent.transform.GetChild(1).GetComponent<CharacterHandler>().character = james;
        charactersParent.transform.GetChild(2).GetComponent<CharacterHandler>().character = mary;
        charactersParent.transform.GetChild(3).GetComponent<CharacterHandler>().character = harry;
    }

    void handleAccept()
    {
        arManager.currentCharacter.setScale();
        charactersDone += 1;

        if (charactersDone < characters.Count)
        {
            arManager.updateModelAndCharacterToPlace();
            setMode(GameMode.placementMode);
        }
        else
            setMode(GameMode.playMode);
    }
}

public enum GameMode
{
    tutorialMode,
    placementMode,
    scalingMode,
    playMode
}
