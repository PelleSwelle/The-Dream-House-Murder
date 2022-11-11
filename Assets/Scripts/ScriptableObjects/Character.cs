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
    public bool hasBeenTalkedTo = false;
    public Conversation conversation;

    // *************** CHARACTER PROPERTIES ***************
    public Sprite photo; // TODO: this
    public string firstName, middleName, lastName, description;
    public int dateOfBirth;

    // TODO: update this when character is placed
    public bool isPlaced;

    public Character(string name, Sprite photo)
    {
        this.firstName = name;
        this.photo = photo;
    }

    public void printCharacter()
    {
        Debug.Log(
            $"**************{this.firstName}**************\n Talked to before: {this.hasBeenTalkedTo}"
        );
    }
}
