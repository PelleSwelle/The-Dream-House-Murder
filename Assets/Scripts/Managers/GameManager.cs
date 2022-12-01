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
        mary = new Character("Mary", maryPhoto, maryPrefab, "*sob* What?", "female");
        james = new Character("James", jamesPhoto, jamesPrefab, "Who are you? What do you want?", "male");
        officer = new Character("Officer", officerPhoto, officerPrefab, "What can I help you with?", "male");
        harry = new Character("Harry", harryPhoto, harryPrefab, "What is it?", "male");

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
