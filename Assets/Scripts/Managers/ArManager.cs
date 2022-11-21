using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ArManager : MonoBehaviour
{
    public ConversationManager conversationManager;
    // public GameObject policePerson, boyfriend, mary, rea;
    public GameManager gameManager;
    public GameObject objectToSpawn;
    public Character currentCharacter;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose pose;
    private ARRaycastManager aRRaycastManager;
    private bool poseIsValid = false;
    private float initialDistance;
    private Vector3 initialScale;
    // public TipManager tipManager;
    public int charactersDone = 0;
    public Button acceptScaleButton;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        updateModelAndCharacter();
    }

    void Update()
    {

        // *** game mode ***
        if (currentCharacter.isPlaced && currentCharacter.isScaled)
        {
            deactivateAcceptButton();
            if (charactersDone == gameManager.characters.Count)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitData;
                    Physics.Raycast(ray, out hitData);

                    // print(hitData.collider.name);
                    foreach (Character character in gameManager.characters)
                    {
                        if (hitData.collider.name == character.model.name)
                        {
                            print($"hit character: {character.firstName}");
                            conversationManager.initConversation(character);
                        }
                    }
                }
            }
            else
                updateModelAndCharacter();
        }

        // *** placement
        if (!currentCharacter.isPlaced && poseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            placeObject(objectToSpawn, currentCharacter);

        // *** scaling ***
        if (currentCharacter.isPlaced && !currentCharacter.isScaled && Input.touchCount == 2)
        {
            activateAcceptButton();
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                return;

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = spawnedObject.transform.localScale;
                Debug.Log("Initial Disatance: " + initialDistance + "GameObject Name: "
                    + objectToSpawn.name); // Just to check in console
            }
            else // if touch is moved
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialDistance, 0))
                    return; // do nothing if it can be ignored where inital distance is very close to zero

                float factor = currentDistance / initialDistance;
                spawnedObject.transform.localScale = initialScale * factor; // scale multiplied by the factor we calculated
            }
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }


    void activateAcceptButton()
    {
        acceptScaleButton.gameObject.SetActive(true);
        acceptScaleButton.onClick.AddListener(() => acceptScale());
    }

    void deactivateAcceptButton()
    {
        acceptScaleButton.gameObject.SetActive(false);
    }

    /// <summary> set the current character to isScaled </summary>
    void acceptScale()
    {
        print("accept scale, moving on to next character");
        currentCharacter.isScaled = true;
    }

    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && poseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation);
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

    /// <summary> set SpawnedObject to the current Model and set character is placed </summary>
    /// <param name="model"></param>
    /// <param name="character"></param>
    void placeObject(GameObject model, Character character)
    {
        spawnedObject = Instantiate(model, pose.position, pose.rotation);
        character.model = spawnedObject;
        character.isPlaced = true;
    }

    /// <summary> update currentCharacter and the object to spawn</summary>
    void updateModelAndCharacter()
    {
        charactersDone += 1;
        currentCharacter = gameManager.characters.Find(x => x.isPlaced == false);
        objectToSpawn = gameManager.characters.Find(x => x.isPlaced == false).model;
        print($"model to place: {objectToSpawn.name} belongs to: {currentCharacter.firstName}");
    }

}
