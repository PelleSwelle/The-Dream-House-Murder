using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    // *************** GAME REFERENCES ***************
    public GameObject notebook;
    public CharactersPage charactersPage;
    public int index;
    public GameObject model;
    public GameObject bioPage;
    public GameObject[] fields; // coupling

    // *************** CHARACTER PROPERTIES ***************
    public Sprite photo;
    public int dateOfBirth, iq;
    public bool inARelationship;
    public GameObject partner;
    public string nickName, firstName, middleName, lastName,
                  compassionate, loyal, stable, extrovert,
                  longDescription, description,
                  careerPath, politicalLeaning, educationField, educationLevel,
                  diet, sexuality, religion, ethnicity, nationality;
    public string[] hobbies, favoriteFoods, badHabits, negativeCharacteristics, positiveCharacteristics;

    public string whereWasFound;

    public bool hasBeenTalkedTo = false;
    public List<Subject> hasBeenTalkedToAbout;

    // ************ WHAT THE PERSON KNOWS (set in the inspector) ************
    // public bool knowsWeapon, knowsLocation,
    //             knowsTimeOfDay, wasAtTheScene,
    //             knowsVictim, knowsMurderer,
    //             knowsWhoIsMurderer, knowsMotive,
    //             knowsWhoWitnessed, sawItHappen;
    public Subject[] knowsAbout;


    // TODO: this should probably not be strings
    public Subject[] liesAbout;
    public Answer[] lines;
    public bool isWitness; // check this to see wether the character should have a story to tell about what happened

    public bool isMurderer;
    /// <summary>
    /// Console log different values contained in the player. primarily used in conversation
    /// </summary>
    public void printState()
    {
        Debug.Log(
            $"**************{this.nickName}**************\n Talked to before: {this.hasBeenTalkedTo} \n Has been talked to about: {this.hasBeenTalkedToAbout} \n"
        );
    }
}
