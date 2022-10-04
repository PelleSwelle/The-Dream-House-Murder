using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool hasBeenTalkedTo;
    // TODO every character 'carries' their own biopage, which is then displayed in the players notebook.
    public GameObject bioPage;
    // where the traits will go to
    public GameObject[] fields;

    // CHARACTER TRAITS

    // HARD FACTS
    public Sprite photo;
    public string firstName, middleName, lastName;
    public string[] nickNames;
    public int dateOfBirth;
    public string nationality;

    public string description;
    public string longDescription;
    public string ethnicity;
    public string religion;
    public string sexuality;
    public string educationLevel;
    public string educationField;
    public string politicalLeaning;
    public string relationshipStatus;
    public GameObject partner;
    public string careerPath;
    public string[] positiveCharacteristics;
    public string[] negativeCharacteristics;
    public string diet;
    public string extrovert;
    public string stable;
    public string loyal;
    public string compassionate;
    public int iq;
    public string[] badHabits;
    public string[] hobbies;
    public string[] favoriteFoods;

    public string[] lines;
    public string openingLine;
    public GameObject notebook;
    public CharactersPanel charactersPanel;
    public int index;
    void OnValidate()
    {
        notebook = GameObject.Find("NotebookPanel");
        charactersPanel = GameObject.Find("CharactersPanel").GetComponent<CharactersPanel>();
        openingLine = "what do you want?";
        lines = new string[]
        {
            "I don't know who you are",
            "I did not do it. I didn't even know the guy"
        };
        // TODO get the traits according to the charactername
    }

    void Start()
    {
        // at the start of the game, the player has not talked to anyone.
        this.hasBeenTalkedTo = false;
    }

    /// <summary>
    /// collection of functions to run, when the player meets the character for the first time.
    /// </summary>
    public void onFirstConversation()
    {
        addToCharactersPanel(this);
    }
    public void setIndex(int index)
    {
        this.index = index;
    }

    public void OnMouseDown()
    {
        print("clicked on character");
    }

    /// <summary>
    /// run when the player meets the character
    /// adds the given character to the characters page in the notebook.
    /// </summary>
    /// <param name="character"></param>
    void addToCharactersPanel(Character character)
    {
        charactersPanel.populateButton(this);
        print($"added {character} to characters panel");
    }
}
