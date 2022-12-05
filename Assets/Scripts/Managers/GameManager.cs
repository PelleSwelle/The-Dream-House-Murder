using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endScreen;
    public Text endText;
    public GameObject charactersParent;
    public List<Character> characters;
    public GameObject cursor;
    public Character mary, james, officer, harry;
    public GameObject officerPrefab, maryPrefab, jamesPrefab, harryPrefab;
    public Sprite officerPhoto, maryPhoto, jamesPhoto, harryPhoto;
    public AudioSource musicSource;
    public AudioClip gameMusic;
    public GameMode currentMode;
    public Button acceptScaleButton, notebookButton, backToTitleButton;
    public ArManager arManager;
    public int charactersDone = 0;
    public TipManager tipManager;
    public CharacterHandler officerHandler, maryHandler, jamesHandler, harryHandler;
    public GameObject accusePanel;

    public void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }
    void Start()
    {
        arManager.tipManager.tutorialOverlay.SetActive(true);
        accusePanel.SetActive(false);
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
            _openingLine: "What do you want?",
            _gender: "male",
            _nothingToSay: "Shouldn't you talk to the officer first?"
        );
        officer = new Character(
            _firstName: "Officer",
            _photo: officerPhoto,
            _model: officerPrefab,
            _openingLine: "What do you need to know?",
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

        assignCharacterHandlers();
        cursor.SetActive(true);
        acceptScaleButton.onClick.AddListener(() => handleAccept());
        backToTitleButton.onClick.AddListener(() => goToTitleScreen());


        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.Play();

        setMode(GameMode.tutorialMode);
    }

    void goToTitleScreen()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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
            new Act(2, new repeatableConversation(Constants.officerAct2)),
            new Act(3, new SingleRunConversation(Constants.officerAct3))
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

    public bool act2IsOverForThree()
    {
        if (mary.acts[1].isFinished() && harry.acts[1].isFinished() && james.acts[1].isFinished())
        {
            print("three is finished");
            return true;
        }
        else
            return false;
    }

    public void setMode(GameMode _gameMode)
    {
        currentMode = _gameMode;
        acceptScaleButton.gameObject.SetActive(currentMode == GameMode.scalingMode);
        notebookButton.gameObject.SetActive(currentMode == GameMode.playMode);
    }

    void assignCharacterHandlers()
    {
        officerHandler.character = officer;
        jamesHandler.character = james;
        maryHandler.character = mary;
        harryHandler.character = harry;
    }

    public Character getAccusedCharacter()
    {
        Question choice = officer.acts[2].conversation.Questions.Find(x => x.ID.val1 == 2 && x.hasBeenSaid == true);
        if (choice.ID.val2 == 1) return mary;
        else if (choice.ID.val2 == 2) return james;
        else return harry;
    }

    void handleAccept()
    {
        arManager.currentCharacter.setScale();
        charactersDone += 1;

        if (charactersDone < characters.Count)
        {
            arManager.spawnedObject = null;
            arManager.updateModelAndCharacterToPlace();
            setMode(GameMode.placementMode);
        }
        else
            setMode(GameMode.playMode);

        tipManager.incrementTutorial();
    }

    public void endGame(Character character)
    {
        accusePanel.SetActive(false);

        if (character == harry)
        {
            endText.text = "You didn't catch the murderer. Play again to figure out who did it.";
        }
        else if (character == mary)
        {
            endText.text = "The murderer got away with the deed. Play again to find the murderer.";
        }
        else if (character == james)
        {
            endText.text = "You solved the murder, and won the game. Congratulations!";
        }
        endScreen.SetActive(true);
    }

}

public enum GameMode
{
    tutorialMode,
    placementMode,
    scalingMode,
    playMode
}
