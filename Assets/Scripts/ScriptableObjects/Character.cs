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
    public Conversation conversation;

    // *************** CHARACTER PROPERTIES ***************
    public bool hasBeenTalkedTo = false;
    public Sprite photo; // TODO: this
    public string firstName, middleName, lastName, description;
    public int dateOfBirth;

    // TODO: update this when character is placed
    public bool isPlaced = false, isScaled = false;

    public Character(string name, Sprite photo, GameObject model)
    {
        this.firstName = name;
        this.photo = photo;
        this.model = model;

        this.isPlaced = false;
        this.isScaled = false;
    }

    public void scaleModel(Vector3 newScale)
    {
        this.model.transform.localScale = newScale;
        Debug.Log("scaling character" + this.model.transform.localScale);
    }
}
