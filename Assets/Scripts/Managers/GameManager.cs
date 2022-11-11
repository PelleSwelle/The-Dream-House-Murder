using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject charactersParent;
    public List<Character> characters;
    public GameObject cursor, notebookButton;
    public Character mary, boyfriend, officer, rea;
    public ConversationUI conversationUI;
    public Notebook notebook;
    public GameObject notebookObject;
    public bool cursorIsVisible = true;

    // TODO: delete this on release
    public Button debugButton;
    public GameObject debugMenu;

    public ConversationManager conversationManager;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;
    public Plot plot;

    public string[] hasHeard;

    public int numberOfCharactersPlaced;
    public bool isInPlacementMode;

    // to be filled up during gameplay, as the player learns more and more about each person
    public string knownFacts;

    public void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }


    void Start()
    {
        cursor.SetActive(true);
        numberOfCharactersPlaced = 0;
        mary = new Character("Mary", Resources.Load("maryPhoto") as Sprite);
        boyfriend = new Character("Boyfriend", Resources.Load("boyfriendPhoto") as Sprite);
        officer = new Character("Officer", Resources.Load("officerPhoto") as Sprite);
        rea = new Character("Rea", Resources.Load("reaPhoto") as Sprite);
        characters = new List<Character> { mary, boyfriend, officer, rea };

        print($"number of characters instantiated: {characters.Count}");
    }

    void Update()
    {
        // bool allCharactersPlaced = numberOfCharactersPlaced == characters.Count;

        // // ************* PLACEMENT MODE *************
        // if (!allCharactersPlaced)
        // {
        //     isInPlacementMode = true;

        //     if (cursorIsVisible)
        //     {
        //         updateCursor();
        //     }
        //     // check wether any UI is open
        //     if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //     {
        //         placeCharacter(characters[numberOfCharactersPlaced + 1]);
        //     }
        //     isInPlacementMode = false;
        // }

        // // ************* GAME MODE *************
        // else
        // {
        //     // game mode
        // }
    }


    void placeCharacter(Character character)
    {
        if (cursorIsVisible)
        {
            GameObject.Instantiate(character.model, transform.position, transform.rotation);
        }
        else
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            if (hits.Count > 0)
            {
                GameObject.Instantiate(character.model, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
        numberOfCharactersPlaced++;
    }

    void updateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(.5f, .5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
