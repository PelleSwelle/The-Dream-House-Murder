using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour
{
    public GameObject tutorialOverlay;
    public Button tipButton;
    private int instructionIndex;
    public Text numberOfCharactersText;
    public GameManager gameManager;

    public List<string> instructionLines;
    private Text buttonText;

    void Start()
    {
        instructionLines = new List<string>()
        {
            "Move your phone around, focusing on the ground. You will start to see white dots appear. Tap this message, when they do.",
            "It is encouraged to walk around the room and place the characters spaced apart. The game will keep track of their position. Tap this panel to continue",
            "Aim the icon at the spot, where you want to place your first character, and tap the screen",
            "You can now rotate and scale the character to your own liking. If It looks good, press ACCEPT SCALE AND ROTATION."
        };
        instructionIndex = 0;
        buttonText = tipButton.GetComponentInChildren<Text>();
        setButtonText(instructionLines[instructionIndex]);

        tipButton.onClick.AddListener(() => incrementTutorial());
    }


    public void setButtonText(string text)
    {
        buttonText.text = text;
    }

    public void incrementTutorial()
    {
        instructionIndex += 1;
        if (instructionIndex > instructionLines.Count)
            tipButton.gameObject.SetActive(false);
        else
            setButtonText(instructionLines[instructionIndex]);

        if (buttonText.text == instructionLines[2])
            gameManager.setMode(GameMode.placementMode);
    }
}
