using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character
{
    // *************** GAME REFERENCES ***************
    public GameObject notebook;
    public CharactersPage charactersPage;
    public GameObject model;
    public GameObject biopage;
    public GameObject[] fields;
    // public Conversation conversation;
    public List<Question> questions;

    // *************** CHARACTER PROPERTIES ***************
    public bool hasBeenTalkedTo = false;
    public string openingLine;
    public Sprite photo; // TODO: this
    public string firstName, middleName, lastName, description;
    public int dateOfBirth;
    public string gender;

    public bool isPlaced = false, isScaled = false;

    public Character(string firstName, Sprite photo, GameObject model, string _openingLine, string gender)
    {
        this.firstName = firstName;
        this.photo = photo;
        this.model = model;

        this.isPlaced = false;
        this.isScaled = false;
        this.openingLine = _openingLine;
        this.gender = gender;
    }

    public void scaleModel(Vector3 newScale)
    {
        this.model.transform.localScale = newScale;
        Debug.Log("scaling character" + this.model.transform.localScale);
    }

    public Question getLastAskedQuestion()
    {
        return questions.FindLast(x => x.hasBeenSaid == true);
    }

    public Question getQuestionByID(int val1, int val2 = 0, int val3 = 0)
    {
        return questions.Find(x => x.ID.val1 == val1 && x.ID.val2 == val2 && x.ID.val3 == val3);
    }

    public Question getFirstQuestion()
    {
        return questions.Find(x => x.ID.val1 == 1);
    }
}
