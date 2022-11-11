using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The characters page is where the information about the different characters are gathered. 
/// it is a page of four tiles, one for each character to talk to. each get filled out, as they are met throughout the game.
/// </summary>
public class CharactersPage : MonoBehaviour
{
    public GameManager gameManager;
    public Button[] tiles; // The buttons representing a character on the page.
    public Sprite placeholderSprite; // sprite to display if the person has not been met yet.
    public Notebook notebook;
    public GameObject bioPage; // The bio page. When cllicked on a character button, bio is populated with the given character
    public Character[] characters; // array of characters in the game
    public Button bioExitButton; // the exit button on the biopage

    public GameObject charactersParent; // parent object for all of the characters.
    void OnValidate()
    {
    }
    void Start()
    {
        // four buttons = four characters, each with an onclick leading to their biopage
        tiles[0].onClick.AddListener(() => displayBio(getCharacterFromButton(tiles[0])));
        tiles[1].onClick.AddListener(() => displayBio(getCharacterFromButton(tiles[1])));
        tiles[2].onClick.AddListener(() => displayBio(getCharacterFromButton(tiles[2])));
        tiles[3].onClick.AddListener(() => displayBio(getCharacterFromButton(tiles[3])));
    }

    /// <summary>
    /// opens the bio page and populates it with the given character taken from the index given.
    /// </summary>
    /// <param name="index"></param>
    public void displayBio(Character _character)
    {
        bioPage.SetActive(true);
        bioPage.GetComponent<BioPage>().populateBio(_character);
        notebook.goToPage(notebook.bioPage, true);
        // TODO: check wether the character is met. If not, pop up says: you have not met this person yet.
    }

    // FIXME: returns wrong character
    Character getCharacterFromButton(Button button)
    {
        Character character = null;

        // get the text from the button
        string name = button.transform.GetChild(1).GetComponent<Text>().text;
        print($"text on the button: {name}");

        foreach (Transform characterTransform in charactersParent.transform)
        {
            if (name == characterTransform.gameObject.GetComponent<CharacterHandler>().character.firstName)
            {
                character = characterTransform.gameObject.GetComponent<CharacterHandler>().character;
            }
        }
        print($"button contained: {character.firstName}");
        return character;
    }

    /// <summary>
    /// populate a button in the characters page with the given character
    /// called by addToCharacters()
    /// </summary>
    /// <param name="character"></param>
    public void populateButton(Button button, Character character)
    {
        button.transform.GetChild(0).GetComponent<Image>().sprite = character.photo;
        button.GetComponentInChildren<Text>().text = character.firstName;
        button.GetComponent<CharacterTile>().onPopulate();
        // print($"populated button: {button} with character: {character.firstName}");
    }

    /// <summary>
    /// to be called from outside the class.
    /// add a character to the characters page
    /// </summary>
    /// <param name="character"></param>
    public void add(Character character)
    {
        print($"added {character} to characters screen");
        populateButton(getNextAvailableButton(), character);
    }

    /// <summary>
    /// loops through the buttons on the characters page to find on with no character in it.
    /// </summary>
    /// <returns></returns>
    public Button getNextAvailableButton()
    {
        Button button = tiles[0];

        foreach (Button tile in this.tiles)
        {
            string tileName = tile.transform.GetChild(1).GetComponent<Text>().text;
            if (tileName == "NAME")
            {
                button = tile;
            }
        }
        return button;
    }
}
