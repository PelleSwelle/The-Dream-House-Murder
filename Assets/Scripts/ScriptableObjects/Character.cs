using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character
{
    // *************** GAME REFERENCES ***************
    public GameObject model;
    public List<Act> acts;
    public Act currentAct;
    public List<Question> questionsAsked;

    // *************** CHARACTER PROPERTIES ***************
    public bool hasBeenTalkedTo = false;
    public string openingLine;
    public Sprite photo; // TODO: this
    public string firstName, middleName, lastName, description;
    public string gender;
    // int index = 0;

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

        acts = new List<Act>();

        questionsAsked = new List<Question>();
    }

    public void loadActs(Act act1, Act act2)
    {
        acts.Add(act1);
        acts.Add(act2);
        currentAct = acts[0];
    }

    public void setScale()
    {
        isScaled = true;
    }


    public void scaleModel(Vector3 newScale)
    {
        this.model.transform.localScale = newScale;
        Debug.Log("scaling character" + this.model.transform.localScale);
    }

    public void enterSecondAct()
    {
        Act secondAct = acts[1];
        currentAct = secondAct;
    }

    public void enterThirdAct()
    {
        currentAct = acts[2];
    }

    public Question getLastAskedQuestion()
    {
        return questionsAsked[questionsAsked.Count - 1];
    }
}
