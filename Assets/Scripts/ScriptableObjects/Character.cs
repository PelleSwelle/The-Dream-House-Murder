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
    public string nothingToSayLine;
    public bool isPlaced = false, isScaled = false;

    public Character(string _firstName, Sprite _photo, GameObject _model, string _openingLine, string _gender, string _nothingToSay)
    {
        firstName = _firstName;
        photo = _photo;
        model = _model;

        isPlaced = false;
        isScaled = false;
        openingLine = _openingLine;
        gender = _gender;
        nothingToSayLine = _nothingToSay;

        acts = new List<Act>();

        questionsAsked = new List<Question>();
    }

    public void loadActs(Act act1, Act act2, Act act3 = null)
    {
        acts.Add(act1);
        acts.Add(act2);
        acts.Add(act3);
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

    public void goToAct(int actNumber)
    {
        actNumber--;
        currentAct = acts[actNumber];
        Debug.Log($"{firstName} entered act {currentAct.actNumber}");
    }

    public Question getLastAskedQuestion()
    {
        return questionsAsked[questionsAsked.Count - 1];
    }
}
