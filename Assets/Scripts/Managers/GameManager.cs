using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cursor;
    public Character[] charactersMet;
    public UiHandler notebookHandler;
    public GameObject notebook;
    public bool cursorIsVisible = true;

    // TODO: delete this on release
    public Button debugButton;
    public GameObject debugMenu;
    public Conversation[] conversations; // conversations that has happened 
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;

    public void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }

    void Start()
    {
        cursor.SetActive(true);
    }

    void Update()
    {
        handleTouch();
    }
    void handleTouch()
    {
        if (cursorIsVisible)
        {
            updateCursor();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (cursorIsVisible)
            {
                print("cursor is visible");
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else
            {
                print("something something raycast manager");
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }
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

    public void spawnCharacter()
    {
        throw new System.NotImplementedException("Not implemented");
    }

    void testInput()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // spawnCharacter()
        // }
        // CONVERSATION WITH AVERAGE BOB
        if (Input.GetKeyDown(KeyCode.A))
        {
            // TODO find another way to get the character
            // initiateConversation(GameObject.Find("averageBob"));
        }
        // CHARACTERS
        else if (Input.GetKeyDown(KeyCode.U))
        {
            // this.uiHandler.goToPage(this.notebook.pages[0]);
        }
        // CLUES
        else if (Input.GetKeyDown(KeyCode.I))
        {
            // this.uiHandler.goToPage(this.notebook.pages[1]);
        }
        // CONVERSATIONS
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // this.uiHandler.goToPage(this.notebook.pages[2]);
        }
        // BIO
        else if (Input.GetKeyDown(KeyCode.P))
        {
            // this.uiHandler.goToPage(this.notebook.pages[3]);
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // this.toggleNotebook();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            // charactersPanel.populateButton(GameObject.Find("averageBob").GetComponent<Character>());
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            // charactersPanel.populateButton(GameObject.Find("shortKathy").GetComponent<Character>());
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            // charactersPanel.populateButton(GameObject.Find("oldReginald").GetComponent<Character>());
        }
    }
}
