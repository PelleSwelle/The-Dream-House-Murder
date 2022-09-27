using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates characters with randomized:
///     names, ages, occupations
/// </summary>
public class CharacterCreator : MonoBehaviour
{
    private int numberOfCharacters;
    // this number reflects the amount of names in each of the arrays of names
    private int numberOfNames = 3;
    private int[] ages;
    private string[] genders = new string[] { "Female", "Male", "NonBinary" };
    private string[] maleNames = new string[] { "Hank", "Simon, MAJED" };

    private string[] femaleNames = new string[] { "Mona", "Cathrine", "Victoria" };
    private string[] nonBinaryNames = new string[] { "Sasha", "Kim", "Remy" };
    private string[] occupations = new string[] { "Teacher", "Carpenter", "Web-developer", "Doctor", "nurse", "Life-guard", "Student", "Unemployed" };

    void OnValidate()
    {
        // only for testing
    }
    /// <summary>
    /// generates a random gender between female, male and non-binary
    /// </summary>
    /// <returns>string</returns>
    string generateGender()
    {
        int seedValue = Random.Range(0, genders.Length);
        string characterGender = genders[seedValue];
        return characterGender;
    }

    /// <summary>
    /// generates a name from a given list of names for the given gender
    /// </summary>
    /// <param name="gender"></param>
    /// <returns>string</returns>
    string generateName(string gender)
    {
        // finding the correct array of names to choose from
        string[] namesToChooseFrom = new string[numberOfNames];
        if (gender == "female")
        {
            namesToChooseFrom = femaleNames;
        }
        else if (gender == "male")
        {
            namesToChooseFrom = maleNames;
        }
        else if (gender == "nonBinary")
        {
            namesToChooseFrom = nonBinaryNames;
        }

        // generate a random number, and get that index in the array
        int seedValue = Random.Range(0, namesToChooseFrom.Length);
        string characterName = namesToChooseFrom[seedValue];

        return characterName;
    }

    /// <summary>
    /// generates a random occupation from a list of occupations
    /// </summary>
    /// <returns>string</returns>
    string generateOccupation()
    {
        int seedValue = Random.Range(0, occupations.Length);
        string characterOccupation = occupations[seedValue];
        return characterOccupation;
    }
    // Character createCharacter()
    // {
    //     // instantiate the character
    //     Character character = new Character();
    //     // give the character characteristics
    //     character.gender = generateGender();
    //     character.personName = generateName(character.gender);
    //     character.occupation = generateOccupation();
    //     print(character);
    //     return character;
    // }
    void Start()
    {
        // createCharacter();
    }

    void removeFromList()
    {

    }
}
