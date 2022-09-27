using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersPanel : MonoBehaviour
{
    public int numberOfCharacters = 6;
    public Button[] characterButtons;
    public Sprite placeholderSprite;
    Notebook notebook;
    public int numberOfButtons;
    public GameObject bioPage;
    public Button bioExitButton;
    // fields in the biopage to be filled at runtime
    public GameObject bioImageField, bioNameField, bioNicknameField, bioDOBField, bioOccupationField, bioDescriptionField, bioFavFoodsField, bioReligionField;

    void OnValidate()
    {
        bioPage = GameObject.Find("bioPage");
        notebook = GameObject.Find("NotebookPanel").GetComponent<Notebook>(); //get notebook
        characterButtons = new Button[numberOfCharacters]; // array of buttons
        numberOfButtons = this.transform.childCount;
        for (int i = 0; i < numberOfButtons; i++) // fill the array of buttons
        {
            // put all of the buttons, that are children of this gameobject into the array
            characterButtons[i] = this.transform.GetChild(i).GetComponent<Button>();
        }
    }
    void Start()
    {
        // TODO: maybe attach listeners, when the tile is populated?
        // onclick methods for the buttons on the charactersPanel
        characterButtons[0].onClick.AddListener(() => goToCharacterBio(0));
        characterButtons[1].onClick.AddListener(() => goToCharacterBio(1));
        characterButtons[2].onClick.AddListener(() => goToCharacterBio(2));
        characterButtons[3].onClick.AddListener(() => goToCharacterBio(3));
        characterButtons[4].onClick.AddListener(() => goToCharacterBio(4));
        characterButtons[5].onClick.AddListener(() => goToCharacterBio(5));

        bioExitButton.onClick.AddListener(() => notebook.goToPage(notebook.charactersGroup));
    }

    /// <summary>
    /// opens the bio page and populates it with the given character taken from the index given.
    /// </summary>
    /// <param name="characterIndex"></param>
    public void goToCharacterBio(int characterIndex)
    {
        print($"clicked button: {characterIndex}");
        // TODO: check wether the character is met. If not, pop up says: you have not met this person yet.

        Character character = getCharacterFromIndex(characterIndex); // get the character at the given index
        populateBio(character); // populate the biopage with the given character
        notebook.goToPage(notebook.bioGroup); // display the biopage
    }

    // TODO: not done yet

    /// <summary>
    /// populate a button in the characters page with the given character
    /// </summary>
    /// <param name="character"></param>
    public void populateButton(Character character)
    {
        Button buttonToPopulate = getNextAvailableButton(); // get a button to populate
        buttonToPopulate.transform.GetChild(0).GetComponent<Image>().sprite = character.photo; // set the image of the button
        buttonToPopulate.transform.GetChild(1).GetComponent<Text>().text = character.name; // set the text of the button
        character.index = getButtonIndex(character); // set the index of the character to index of the button
    }

    /// <summary>
    /// finds the next slot on the characterpage that can be populated
    /// </summary>
    /// <returns>Button (next available)</returns>
    public Button getNextAvailableButton()
    {
        Button availableButton = characterButtons[0]; // variable to hold the result
        for (int i = 0; i < characterButtons.Length; i++)
        {
            Sprite characterImage = characterButtons[i].transform.GetChild(0).GetComponent<Image>().sprite; // get the image component
            if (characterImage == placeholderSprite)
            {
                availableButton = characterButtons[i]; // return the first button that has a placeholder sprite

            }
        }
        return availableButton;
    }

    /// <summary>
    /// reads the contents of the button at the given index and returns the character referenced
    /// </summary>
    /// <param name="index"></param>
    /// <returns>character at that index</returns>
    public Character getCharacterFromIndex(int index)
    {
        // get the name from the button
        string characterName = characterButtons[index].transform.GetChild(1).GetComponent<Text>().text;
        // find the character with that name
        Character character = GameObject.Find(characterName).GetComponent<Character>();
        return character;
    }

    /// <summary>
    /// finds the index of the button holding the given character
    /// </summary>
    /// <param name="character"></param>
    /// <returns>int index of button</returns>
    public int getButtonIndex(Character character)
    {
        int buttonIndex = new int();
        for (int i = 0; i < characterButtons.Length; i++)
        {
            string buttonCharacter = characterButtons[i].transform.GetChild(1).GetComponent<Text>().text;
            if (buttonCharacter == character.name)
            {
                buttonIndex = i;
            }
        }
        print($"index of the button for {character} is {buttonIndex}");
        print($"{character}'s index taken from class is {character.index}");
        return buttonIndex;
    }

    public void populateBio(Character character)
    {
        // get all the neccesary fields
        bioImageField.GetComponent<Image>().sprite = character.photo; // image
        bioNameField.GetComponent<TMPro.TextMeshProUGUI>().text = character.name; // name
        // TODO: should only have one nickname
        bioNicknameField.GetComponent<TMPro.TextMeshProUGUI>().text = character.nickNames[0]; // nickname
        bioDOBField.GetComponent<TMPro.TextMeshProUGUI>().text = character.dateOfBirth.ToString(); // date of birth
        bioOccupationField.GetComponent<TMPro.TextMeshProUGUI>().text = character.careerPath; // occupation
        bioDescriptionField.GetComponent<TMPro.TextMeshProUGUI>().text = character.description; // description
        bioFavFoodsField.GetComponent<TMPro.TextMeshProUGUI>().text = character.favoriteFoods[0]; // favorite foods
        bioReligionField.GetComponent<TMPro.TextMeshProUGUI>().text = character.religion; // religion
    }
}
