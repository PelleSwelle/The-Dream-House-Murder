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
    private bool isOpen;

    // BUTTONS
    public Button toggleButton; // button for opening and closing the notebook
    public Button conversationsButton, cluesButton, CharactersButton;

    // the buttons in the character screen. 
    // TODO: should not be active before the person is met.
    public Button[] char1button, char2button, char3button, char4button, char5button, char6button;


    // canvas groups for controlling the alpha and interactability
    public CanvasGroup
        notebookGroup,
        buttonGroup,
        cluesGroup, conversationsGroup, charactersGroup,
        bioGroup;

    public CanvasGroup[] pages;
    public CanvasGroup[] bios;
    void OnValidate()
    {
        // buttons
        toggleButton = GameObject.Find("NotebookButton").GetComponent<Button>();
        conversationsButton = GameObject.Find("conversationsTab").GetComponent<Button>();
        CharactersButton = GameObject.Find("charactersTab").GetComponent<Button>();
        cluesButton = GameObject.Find("cluesTab").GetComponent<Button>();

        // canvas groups
        notebookGroup = this.GetComponent<CanvasGroup>();

        pages = new CanvasGroup[4];
        // TODO: there must be a smarter way to do this.
        pages[0] = cluesGroup;
        pages[1] = conversationsGroup;
        pages[2] = charactersGroup;
        pages[3] = bioGroup;
    }

    void Start()
    {
        // TODO isn't this declaring the same thing twice?
        Button toggleNotebookButton = toggleButton.GetComponent<Button>();
        Button openCharactersTab = CharactersButton.GetComponent<Button>();
        Button openCluesTab = cluesButton.GetComponent<Button>();
        Button openConversationsTab = conversationsButton.GetComponent<Button>();



        // EVENT LISTENERS ON BUTTONS
        toggleNotebookButton.onClick.AddListener(toggleNotebook);
        openCharactersTab.onClick.AddListener(() => goToPage(charactersGroup));
        openCluesTab.onClick.AddListener(() => goToPage(cluesGroup));
        openConversationsTab.onClick.AddListener(() => goToPage(conversationsGroup));



        // THE NOTEBOOK IS INITIALLY CLOSED
        setGroupInactive(notebookGroup);
        isOpen = false;
    }

    /// <summary>
    /// sets the canvas group component attached to the notebook
    /// to active or inactive
    /// </summary>
    public void toggleNotebook()
    {
        if (!isOpen)
        {
            setGroupActive(notebookGroup);
            goToPage(charactersGroup);
            isOpen = true;
        }
        else if (isOpen)
        {
            setGroupInactive(notebookGroup);
            isOpen = false;
        }
    }

    /// <summary>
    /// Navigation in the notebook
    /// opens a page in the notebook, while simultaneously closing the others
    /// </summary>
    /// <param name="page">CanvasGroup</param>
    public void goToPage(CanvasGroup page)
    {
        foreach (CanvasGroup _page in pages)
        {
            if (_page != page)
            {
                setGroupInactive(_page);
            }
            else if (_page == page)
            {
                setGroupActive(_page);
                print("opened: " + page.ToString());
            }
        }
    }

    // helper functions for toggling canvasgroups
    /// <summary>
    /// sets alpha to 1 and enables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    public void setGroupActive(CanvasGroup group)
    {
        // set the alpha all the way up, so it is visible
        group.alpha = 1f;
        // make the group interactable
        group.blocksRaycasts = true;
    }
    /// <summary>
    /// sets alpha to 0 and disables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    public void setGroupInactive(CanvasGroup group)
    {
        group.alpha = 0f; // make the group invisible
        group.blocksRaycasts = false; // make the group uninteractable
    }

}
