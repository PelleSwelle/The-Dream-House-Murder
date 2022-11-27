using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour
{
    public GameObject tutorialOverlay;
    public Button tipButton;

    public string planesInstructions = "move your phone around, and white dots should start to appear on the ground around you...";
    public string placementInstructions = "Aim the icon at the spot, where you want to place your first character, and tap the screen";
    public string scalingInstructions = "If the character is not the right size, pinch with two fingers. If It looks good, press ACCEPT SCALE";
    Text buttonText;

    void Start()
    {
        buttonText = tipButton.GetComponentInChildren<Text>();
        setText(planesInstructions);

        tipButton.onClick.AddListener(() => click());
    }


    public void setText(string text)
    {
        buttonText.text = text;
    }

    void click()
    {
        if (buttonText.text == planesInstructions)
            setText(placementInstructions);

    }
}
