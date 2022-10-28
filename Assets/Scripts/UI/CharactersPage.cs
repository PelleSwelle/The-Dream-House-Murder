using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersPage : MonoBehaviour
{
    public Button[] tiles;
    public Sprite placeholderSprite;
    public Notebook notebook;
    public GameObject bioPage;
    public Character[] characters;
    public Button bioExitButton;

    public GameObject charactersParent;
    void OnValidate()
    {
    }
    void Start()
    {
        tiles[0].onClick.AddListener(() => goToBio(getCharacterFromButton(tiles[0])));
        tiles[1].onClick.AddListener(() => goToBio(getCharacterFromButton(tiles[1])));
        tiles[2].onClick.AddListener(() => goToBio(getCharacterFromButton(tiles[2])));
        tiles[3].onClick.AddListener(() => goToBio(getCharacterFromButton(tiles[3])));
        tiles[4].onClick.AddListener(() => goToBio(getCharacterFromButton(tiles[4])));
        tiles[5].onClick.AddListener(() => goToBio(getCharacterFromButton(tiles[5])));

        // TODO: do this somewhere else
        foreach (Character character in characters)
        {
            character.hasBeenTalkedTo = false;
        }
    }

    /// <summary>
    /// opens the bio page and populates it with the given character taken from the index given.
    /// </summary>
    /// <param name="index"></param>
    public void goToBio(Character _character)
    {
        bioPage.SetActive(true);
        bioPage.GetComponent<BioPage>().populateBio(_character);
        notebook.goToPage(bioPage);
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
            if (name == characterTransform.gameObject.GetComponent<CharacterHandler>().character.nickName)
            {
                character = characterTransform.gameObject.GetComponent<CharacterHandler>().character;
            }
        }
        print($"button contained: {character.name}");
        return character;
    }

    /// <summary>
    /// populate a button in the characters page with the given character
    /// </summary>
    /// <param name="character"></param>
    public void populateButton(Button button, Character character)
    {
        button.transform.GetChild(0).GetComponent<Image>().sprite = character.photo;
        button.GetComponentInChildren<Text>().text = character.nickName;
        button.GetComponent<CharacterTile>().onPopulate();
    }

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
