using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ArManager : MonoBehaviour
{
    public ConversationManager conversationManager;
    public GameManager gameManager;

    private GameObject objectToSpawn;
    private GameObject spawnedObject;
    private Character currentCharacter;

    public GameObject placementIndicator;
    Vector3 initialScale;

    private Pose pose;
    private ARRaycastManager aRRaycastManager;

    private bool poseIsValid = false;
    private float initialDistance;
    public TipManager tipManager;

    public int charactersDone = 0;

    public Button acceptScaleButton;

    // TODO: the marker is not showing for some reason.

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        updateModelAndCharacterToPlace();
        gameManager.gameMode = GameMode.placementMode;
    }

    void Update()
    {
        // ***** game mode *****
        if (!currentCharacter.isPlaced)
            gameManager.gameMode = GameMode.placementMode;

        else if (currentCharacter.isPlaced && !currentCharacter.isScaled)
            gameManager.gameMode = GameMode.scalingMode;

        else if (currentCharacter.isPlaced && currentCharacter.isScaled)
        {
            if (charactersDone != gameManager.characters.Count)
                updateModelAndCharacterToPlace();
            else
                gameManager.gameMode = GameMode.playMode;
        }

        // ********* INPUT HANDLING *********
        bool isInPlacementMode = gameManager.gameMode == GameMode.placementMode;
        bool isInScalingMode = gameManager.gameMode == GameMode.scalingMode;
        bool isInPlayMode = gameManager.gameMode == GameMode.playMode;

        if (isInPlacementMode)
        {
            tipManager.setText(tipManager.planesInstructions);
            deactivateScaleAcceptButton();

            placeOnTap();
        }

        else if (isInScalingMode)
        {
            tipManager.setText(tipManager.scalingInstructions);
            activateAcceptScaleButton();

            scaleOnPinch();
        }

        else if (isInPlayMode)
        {
            deactivateScaleAcceptButton();

            initConversationOnTap();

        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    Character getCharacterFromRaycastHit(RaycastHit hit)
    {
        Character character = gameManager.characters.Find(x => x.model.name == hit.collider.name);
        return character;
    }

    void placeOnTap()
    {
        bool isReadyToPlace = poseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (isReadyToPlace)
            placeObject(objectToSpawn, currentCharacter);
    }

    void initConversationOnTap()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hitData;
            Physics.Raycast(ray, out hitData);

            Character characterToTalkTo = getCharacterFromRaycastHit(hitData);
            conversationManager.initConversation(characterToTalkTo);
        }
    }

    void scaleOnPinch()
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
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = spawnedObject.transform.localScale;
            }
            else // if touch is moved
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialDistance, 0))
                    return;

                float factor = currentDistance / initialDistance;
                spawnedObject.transform.localScale = initialScale * factor; // scale multiplied by the factor we calculated
            }
        }
    }

    void activateAcceptScaleButton()
    {
        acceptScaleButton.gameObject.SetActive(true);
        acceptScaleButton.onClick.AddListener(() => currentCharacter.setScale());
    }

    void deactivateScaleAcceptButton()
    {
        acceptScaleButton.gameObject.SetActive(false);
    }

    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && poseIsValid)
        {
            placementIndicator.SetActive(true);
            Pose newPose = new Pose(pose.position, new Quaternion(0, 200, 0, 0));
            placementIndicator.transform.SetPositionAndRotation(pose.position, newPose.rotation);
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
        Pose newPose = new Pose(pose.position, new Quaternion(0, 200, 0, 0));
        spawnedObject = Instantiate(model, newPose.position, newPose.rotation);
        character.model = spawnedObject;
        character.isPlaced = true;
    }

    /// <summary> update currentCharacter and the object to spawn</summary>
    void updateModelAndCharacterToPlace()
    {
        charactersDone += 1;
        currentCharacter = gameManager.characters.Find(x => x.isPlaced == false);
        objectToSpawn = gameManager.characters.Find(x => x.isPlaced == false).model;
    }

}
