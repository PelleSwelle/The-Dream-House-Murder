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
    public bool hasMet = false;
    public string openingLine;
    public Sprite photo;
    public string firstName, middleName, lastName, description;
    public string gender;
    public string nothingToSayLine;
    public bool isPlaced = false, isScaled = false;

    public Character(string _firstName, Sprite _photo, GameObject _model, string _openingLine, string _gender, string _nothingToSay)
    {
        firstName = _firstName;
        photo = _photo;
        model = _model;

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

    public void setScale() => isScaled = true;
    public void scaleModel(Vector3 newScale) => model.transform.localScale = newScale;
    public void goToAct(int actNumber) => currentAct = acts[actNumber];

    public Question getLastAskedQuestion() => questionsAsked[questionsAsked.Count - 1];

    public Question getLastAskedQuestionFromCurrentAct() => currentAct.conversation.getLastAskedQuestion();
}
