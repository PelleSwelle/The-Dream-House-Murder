using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // public UiHandler mainMenuHandler;
    // getting all of the child pages
    public GameObject titlePage, settingsPage, aboutPage, creditsPage, helpPage;
    public Button playButton, settingsButton, aboutButton, creditsButton, helpButton;
    public GameObject backObject;
    public List<GameObject> pages;

    void Start()
    {
        pages = new List<GameObject> { titlePage, settingsPage, aboutPage, creditsPage, helpPage };

        // onclick listeners
        playButton.onClick.AddListener(() => enterGame());
        settingsButton.onClick.AddListener(() => goToPage(settingsPage));
        aboutButton.onClick.AddListener(() => goToPage(aboutPage));
        creditsButton.onClick.AddListener(() => goToPage(creditsPage));
        helpButton.onClick.AddListener(() => goToPage(helpPage));
        // back button
        backObject.GetComponent<Button>().onClick.AddListener(() => goToPage(titlePage));

        openMenu();
    }

    void OnValidate()
    {
        goToPage(titlePage);
    }

    /// <summary>
    /// closes the intro screen and starts the game
    /// </summary>
    void enterGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    void openMenu()
    {
        goToPage(titlePage);
    }

    public void goToPage(GameObject page)
    {
        if (page == titlePage) { backObject.SetActive(false); }
        else { backObject.SetActive(true); }

        // set active or not active to make sure on page does not cover the other
        foreach (GameObject _page in this.pages)
        {
            if (_page == page)
            {
                _page.SetActive(true);
            }
            else
            {
                _page.SetActive(false);
            }
        }
    }
}
