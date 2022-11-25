using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;
    public GameObject charactersParent;
    public List<Character> characters;
    public GameObject cursor, notebookButton;
    public Character mary, boyfriend, officer, rea;
    public ConversationUI conversationUI;
    public Notebook notebook;
    // public GameObject notebookObject, placementModeOverlay;
    public bool cursorIsVisible = true;

    // TODO: delete this on release
    // public Button debugButton;
    // public GameObject debugMenu;


    public GameObject officerPrefab, maryPrefab, boyfriendPrefab, reaPrefab;

    // public ConversationManager conversationManager;
    // public ARRaycastManager raycastManager;

    // public string[] hasHeard;
    public GameMode gameMode;

    // public int numberOfCharactersPlaced;

    public void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }
    void Start()
    {
        cursor.SetActive(true);
        // numberOfCharactersPlaced = 0;

        // ********* LOAD THE CHARACTERS *********
        mary = new Character("Mary", Resources.Load("maryPhoto") as Sprite, maryPrefab, "henmlo. Am Mary");
        boyfriend = new Character("Boyfriend", Resources.Load("boyfriendPhoto") as Sprite, boyfriendPrefab, "hep bup boyfriend");
        officer = new Character("Officer", Resources.Load("officerPhoto") as Sprite, officerPrefab, "hallo am oFfIcEr");
        rea = new Character("Rea", Resources.Load("reaPhoto") as Sprite, reaPrefab, "em real etstae");

        // assign the characters to their respective characterHandlers
        charactersParent.transform.GetChild(0).GetComponent<CharacterHandler>().character = officer;
        charactersParent.transform.GetChild(1).GetComponent<CharacterHandler>().character = boyfriend;
        charactersParent.transform.GetChild(2).GetComponent<CharacterHandler>().character = mary;
        charactersParent.transform.GetChild(3).GetComponent<CharacterHandler>().character = rea;

        characters = new List<Character> { mary, boyfriend, officer, rea };
    }

    /// <summary> fills the character conversations with newly instantiated questions and answers </summary>
}

public enum GameMode
{
    placementMode,
    scalingMode,
    playMode
}
