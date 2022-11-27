using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character
{
    // *************** GAME REFERENCES ***************
    public GameObject model;
    public List<Question> questionsInCurrentAct;
    public Act[] acts;
    public Act currentAct;
    public List<Question> questionsAsked;

    // *************** CHARACTER PROPERTIES ***************
    public bool hasBeenTalkedTo = false;
    public string openingLine;
    public Sprite photo; // TODO: this
    public string firstName, middleName, lastName, description;
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

        acts = new Act[2];

        questionsAsked = new List<Question>();

        if (firstName == "Mary")
        {
            acts[0] = new Act(1, Constants.maryQuestions);
            acts[1] = new Act(2, Constants.maryQuestions);

        }
        else if (firstName == "James")
        {
            acts[0] = new Act(1, Constants.jamesQuestions);
            acts[1] = new Act(2, Constants.jamesAct2);
        }
        else if (firstName == "Officer")
        {
            acts[0] = new Act(1, Constants.officerQuestions);
            acts[1] = new Act(2, Constants.officerAct2);
        }
        else if (firstName == "Harry")
        {
            acts[0] = new Act(1, Constants.harryQuestions);
            acts[1] = new Act(2, Constants.harryAct2);
        }
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

    public void goToNextAct()
    {
        currentAct = acts[1];
    }

    public Question getLastAskedQuestion()
    {
        return questionsAsked[questionsAsked.Count - 1];
    }

    public Question getLastAskedQuestionFromCurrentAct()
    {
        return questionsInCurrentAct.FindLast(x => x.hasBeenSaid == true);
    }


    public Question getFirstQuestionInCurrentAct()
    {
        return questionsInCurrentAct.Find(x => x.ID.val1 == 1);
    }
}
