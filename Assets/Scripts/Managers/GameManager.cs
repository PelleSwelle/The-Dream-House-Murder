using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Characters")]
    public Sprite officerPhoto, maryPhoto, jamesPhoto, harryPhoto;

    public GameObject officerPrefab, maryPrefab, jamesPrefab, harryPrefab;

    public GameObject endScreen, charactersParent, cursor, accusePanel;
    public Text endText;

    [Header("Audio")]

    public GameMode currentMode;
    public Button acceptScaleButton, notebookButton, backToTitleButton, startGameButton;
    int charactersDone = 0;

    [Header("Sub managers")]
    public CutsceneManager cutsceneManager;
    public TipManager tipManager;
    public ArManager arManager;
    public CharacterHandler officerHandler, maryHandler, jamesHandler, harryHandler;

    public Character mary, james, officer, harry;
    public List<Character> characters;

    public bool isWithVideo;

    public void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }
    void Start()
    {
        // ******** CHANGE THIS FOR THE TWO VERSIONS ********
        isWithVideo = false;


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

        characters = new List<Character> { mary, james, officer, harry };
        foreach (Character c in characters)
            print(c.firstName);

        assignCharacterHandlers();
        cursor.SetActive(true);
        acceptScaleButton.onClick.AddListener(() => handleAccept());
        backToTitleButton.onClick.AddListener(() => goToTitleScreen());
        startGameButton.onClick.AddListener(() => startGame());


        setMode(GameMode.tutorialMode);
    }

    void startGame()
    {
        startGameButton.gameObject.SetActive(false);
        notebookButton.gameObject.SetActive(false);
        arManager.tipManager.tutorialOverlay.SetActive(false);

        if (isWithVideo)
            cutsceneManager.playScene(0);
    }

    public void activateUI() => notebookButton.gameObject.SetActive(true);

    void goToTitleScreen() => SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);


    void Update()
    {
        if (currentMode == GameMode.placementMode)
            arManager.placeOnTap();

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
        {
            setMode(GameMode.playMode);
            startGameButton.gameObject.SetActive(true);
        }

        tipManager.incrementTutorial();

    }

    public void endGame(Character character)
    {
        accusePanel.SetActive(false);
        if (isWithVideo)
            cutsceneManager.playScene(2);

        if (character == harry)
            endText.text = "You didn't catch the murderer. Play again to figure out who did it.";
        else if (character == mary)
            endText.text = "The murderer got away with the deed. Play again to find the murderer.";
        else if (character == james)
            endText.text = "You solved the murder, and won the game. Congratulations!";

        endScreen.SetActive(true);
    }

}

public enum GameMode
{
    tutorialMode, placementMode, scalingMode, playMode
}
