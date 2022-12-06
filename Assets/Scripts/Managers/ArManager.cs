using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ArManager : MonoBehaviour
{
    public GameObject harryPrefab, maryPrefab, jamesPrefab, officerPrefab;
    [HideInInspector] public List<GameObject> characters;
    public Button notebookButton;
    public ConversationManager conversationManager;
    public GameManager gameManager;

    private GameObject currentObject;
    public GameObject spawnedObject;
    public Character currentCharacter;

    public GameObject placementIndicator;
    Vector3 initialScale;
    public Notebook notebook;
    public ConversationUI conversationUI;

    private Pose pose;
    private ARRaycastManager aRRaycastManager;

    private bool poseIsValid = false;
    private float initialScaleDistance;
    private float initialRotateDistance;
    private Quaternion initialRotation;
    private Vector2 initialRotatePoint;
    public TipManager tipManager;
    int numberOfCharactersPlaced = 0;
    public GameObject startGameButton;
    public CutsceneManager cutsceneManager;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        updateModelAndCharacterToPlace();
        characters = new List<GameObject>() { officerPrefab, jamesPrefab, harryPrefab, maryPrefab };
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    ICharacter getCharacterFromRaycastHit(RaycastHit hit)
    {
        ICharacter characterHit = hit.collider.GetComponent<ICharacter>();
        print($"character hit: {characterHit.firstName}");
        // ICharacter character = characters.Find(x => x.GetComponent<ICharacter>().firstName == hit.collider.GetComponent<ICharacter>().firstName);
        return characterHit;
    }

    public void placeOnTap()
    {
        bool isReadyToPlace = poseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (isReadyToPlace)
        {
            tipManager.setButtonText(tipManager.instructionLines[3]);
            placeObject(currentObject);
        }
    }

    public void initConversationOnTap()
    {
        bool noUiOpen = !notebook.isOpen && !conversationUI.isOpen;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && noUiOpen)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hitData;
            Physics.Raycast(ray, out hitData);

            ICharacter characterToTalkTo = getCharacterFromRaycastHit(hitData);
            conversationManager.initConversation(characterToTalkTo);
        }
    }

    public void scaleOnPinch()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);


            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                return;

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialScaleDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = spawnedObject.transform.localScale;
            }
            else // if touch is moved
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialScaleDistance, 0))
                    return;

                float factor = currentDistance / initialScaleDistance;
                spawnedObject.transform.localScale = initialScale * factor; // scale multiplied by the factor we calculated
            }
        }
    }

    public void rotateOnDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Ended)
                return;

            if (touch.phase == TouchPhase.Began)
            {
                initialRotatePoint = touch.position;
                initialRotation = spawnedObject.transform.rotation;
            }
            else // if touch is moved
            {
                float currentDistance = Vector2.Distance(touch.position, initialRotatePoint);

                if (Mathf.Approximately(initialScaleDistance, 0))
                    return;

                float factor = currentDistance / initialRotateDistance;
                spawnedObject.transform.Rotate(0f, -touch.deltaPosition.x, 0f);
            }
        }
    }

    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && poseIsValid)
        {
            placementIndicator.SetActive(true);
            // Pose newPose = new Pose(pose.position, new Quaternion(100, 200, 100, 0));
            placementIndicator.transform.SetPositionAndRotation(pose.position, Quaternion.Euler(-90, 0, 0));
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        poseIsValid = hits.Count > 0;
        if (poseIsValid)
            pose = hits[0].pose;
    }

    void placeObject(GameObject character)
    {
        // Pose newPose = new Pose(pose.position, new Quaternion(0, 200, 0, 0));
        spawnedObject = Instantiate(character, pose.position, pose.rotation);
        spawnedObject = character;
        character.GetComponent<ICharacter>().isPlaced = true;

        gameManager.setMode(GameMode.scalingMode);
        numberOfCharactersPlaced++;
    }

    public void updateModelAndCharacterToPlace()
    {
        currentCharacter = gameManager.characters.Find(x => x.isPlaced == false);
        currentObject = gameManager.characters.Find(x => x.isPlaced == false).model;

        tipManager.numberOfCharactersText.text = $"Currently placing: {currentCharacter.firstName}, {numberOfCharactersPlaced + 1} of 4.";
    }
}
