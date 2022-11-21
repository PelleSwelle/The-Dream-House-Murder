using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour
{
    public GameObject tutorialOverlay;
    public Button tipButton;
    public Button acceptButton;

    private string page1text = "You are now in placement mode.\nThere are four characters in this game. Before the game can commence,\nyou will need to place them in your surroundings.\n click to progress";
    private string page2text = "move your phone around, and white dots should start to appear on the ground around you...";
    private string page3text = "place your characters as you see fit. It is recommended to spread them out a bit to be able to use the space around you.";
    Text buttonText;

    void Start()
    {
        buttonText = tipButton.GetComponentInChildren<Text>();
        setTipText(page1text);

        tipButton.onClick.AddListener(() => progressTutorial());
    }


    public void setTipText(string text)
    {
        buttonText.text = text;
    }

    /// <summary>
    /// moves from one part of the tutorial text to the next, if at last one, closes the window
    /// </summary>
    void progressTutorial()
    {
        // Sorry. was going fast
        if (buttonText.text == page1text) { setTipText(page2text); }
        else if (buttonText.text == page2text) { setTipText(page3text); }
        else if (buttonText.text == page3text) { tipButton.gameObject.SetActive(false); }
    }
}
