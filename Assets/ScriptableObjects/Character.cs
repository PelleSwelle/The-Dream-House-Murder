using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    public GameObject model;
    public bool hasBeenTalkedTo = false;
    public GameObject bioPage;
    public GameObject[] fields; // coupling

    // CHARACTER TRAITS

    // HARD FACTS
    public Sprite photo;
    public string firstName, middleName, lastName;
    public string nickName;
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
    public bool inARelationship;
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
    public CharactersPage charactersPage;
    public int index;
    // void OnValidate()
    // {
    //     openingLine = "what do you want?";
    //     lines = new string[]
    //     {
    //         "I don't know who you are",
    //         "I did not do it. I didn't even know the guy"
    //     };
    //     // TODO get the traits according to the charactername
    // }


}
