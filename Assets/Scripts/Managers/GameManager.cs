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
    public Character mary, james, officer, harry;
    public ConversationUI conversationUI;
    public Notebook notebook;
    public bool cursorIsVisible = true;
    public GameObject officerPrefab, maryPrefab, jamesPrefab, harryPrefab;
    public Sprite officerPhoto, maryPhoto, jamesPhoto, harryPhoto;


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
        mary = new Character("Mary", maryPhoto, maryPrefab, "henmlo. Am Mary", "female");
        james = new Character("Boyfriend", jamesPhoto, jamesPrefab, "hep bup boyfriend", "male");
        officer = new Character("Officer", officerPhoto, officerPrefab, "hallo am oFfIcEr", "male");
        harry = new Character("Rea", harryPhoto, harryPrefab, "em real etstae", "male");

        // assign the characters to their respective characterHandlers
        charactersParent.transform.GetChild(0).GetComponent<CharacterHandler>().character = officer;
        charactersParent.transform.GetChild(1).GetComponent<CharacterHandler>().character = james;
        charactersParent.transform.GetChild(2).GetComponent<CharacterHandler>().character = mary;
        charactersParent.transform.GetChild(3).GetComponent<CharacterHandler>().character = harry;

        characters = new List<Character> { mary, james, officer, harry };
    }
}

public enum GameMode
{
    placementMode,
    scalingMode,
    playMode
}
