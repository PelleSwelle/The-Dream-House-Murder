using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The Notebook class contains the player's current knowledge
/// this includes:
///     character sheet
///     conversation log
///     clues log
/// </summary>
public class Notebook : MonoBehaviour
{
    // TODO: the bio page should show all the stuff about the character.

    [SerializeField]

    // BUTTONS
    public Button conversationsButton, cluesButton, CharactersButton;

    // the buttons in the character screen. 
    // TODO: should not be active before the person is met.
    public Button[] char1button, char2button, char3button, char4button, char5button, char6button;


    // canvas groups for controlling the alpha and interactability
    public GameObject
        notebook,
        buttonGroup,
        cluesPage, conversationsPage, charactersPage,
        bioPage;

    public GameObject[] pages;
    public GameObject[] bios;
    void OnValidate()
    {

        // canvas groups
        notebook = this.gameObject;

        pages = new GameObject[4] { cluesPage, conversationsPage, charactersPage, bioPage };
    }

    void Start()
    {
        // TODO isn't this declaring the same thing twice?
        Button openCharactersTab = CharactersButton.GetComponent<Button>();
        Button openCluesTab = cluesButton.GetComponent<Button>();
        Button openConversationsTab = conversationsButton.GetComponent<Button>();



        // EVENT LISTENERS ON BUTTONS
        openCharactersTab.onClick.AddListener(() => goToPage(charactersPage));
        openCluesTab.onClick.AddListener(() => goToPage(cluesPage));
        openConversationsTab.onClick.AddListener(() => goToPage(conversationsPage));


    }


    /// <summary>
    /// Navigation in the notebook
    /// opens a page in the notebook, while simultaneously closing the others
    /// </summary>
    /// <param name="page">CanvasGroup</param>
    public void goToPage(GameObject page)
    {
        foreach (GameObject _page in pages)
        {
            if (_page != page)
            {
                _page.SetActive(false);
                // setGroupInactive(_page);
            }
            else if (_page == page)
            {
                _page.SetActive(true);
                // setGroupActive(_page);
                print("opened: " + page.ToString());
            }
        }
    }

    // helper functions for toggling canvasgroups
    /// <summary>
    /// sets alpha to 1 and enables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    // public void setGroupActive(CanvasGroup group)
    // {
    //     // set the alpha all the way up, so it is visible
    //     group.alpha = 1f;
    //     // make the group interactable
    //     group.blocksRaycasts = true;
    // }
    /// <summary>
    /// sets alpha to 0 and disables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    // public void setGroupInactive(CanvasGroup group)
    // {
    //     group.alpha = 0f; // make the group invisible
    //     group.blocksRaycasts = false; // make the group uninteractable
    // }

}
