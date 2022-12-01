using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour
{
    public GameObject tutorialOverlay;
    public Button tipButton;
    private int instructionIndex;
    public GameManager gameManager;

    public List<string> instructionLines = new List<string>()
    {
        "move your phone around, and white dots should start to appear on the ground around you... Tap this panel when you see them",
        "It is encouraged to walk around the room and place the characters spaced apart. The game will keep track of their position. Tap this panel to continue",
        "Aim the icon at the spot, where you want to place your first character, and tap the screen",
        "If the character is not the right size, pinch with two fingers. If It looks good, press ACCEPT SCALE"
    };
    private Text buttonText;

    void Start()
    {
        instructionIndex = 0;
        buttonText = tipButton.GetComponentInChildren<Text>();
        setButtonText(instructionLines[instructionIndex]);

        tipButton.onClick.AddListener(() => incrementTutorial());
    }


    public void setButtonText(string text)
    {
        buttonText.text = text;
    }

    void incrementTutorial()
    {
        instructionIndex += 1;
        setButtonText(instructionLines[instructionIndex]);
        if (buttonText.text == instructionLines[2])
        {
            gameManager.setMode(GameMode.placementMode);
        }
    }
}
