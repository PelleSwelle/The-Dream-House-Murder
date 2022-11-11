using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public UiHandler mainMenuHandler;
    // getting all of the child pages
    public GameObject titlePage, settingsPage, aboutPage, creditsPage, helpPage;
    public Button playButton, settingsButton, aboutButton, creditsButton, helpButton;
    public GameObject backButton;

    void Awake()
    {
        // Instantiate(playButton, this.transform.position, this.transform.rotation, this.transform);
        // this is dumb but neccessary
        Button play = playButton.GetComponent<Button>();
        Button settings = settingsButton.GetComponent<Button>();
        Button about = aboutButton.GetComponent<Button>();
        Button credits = creditsButton.GetComponent<Button>();
        Button help = helpButton.GetComponent<Button>();
        Button back = backButton.GetComponent<Button>();

        // onclick listeners
        play.onClick.AddListener(() => enterGame());

        settings.onClick.AddListener(() => mainMenuHandler.goToPage(settingsPage));
        settings.onClick.AddListener(() => backButton.SetActive(true));

        about.onClick.AddListener(() => mainMenuHandler.goToPage(aboutPage));
        about.onClick.AddListener(() => backButton.SetActive(true));

        credits.onClick.AddListener(() => mainMenuHandler.goToPage(creditsPage));
        credits.onClick.AddListener(() => backButton.SetActive(true));

        help.onClick.AddListener(() => mainMenuHandler.goToPage(helpPage));
        help.onClick.AddListener(() => backButton.SetActive(true));

        // back button
        back.onClick.AddListener(() => mainMenuHandler.goToPage(titlePage));
        back.onClick.AddListener(() => backButton.SetActive(false));

        // start on title page
        mainMenuHandler.goToPage(titlePage);
    }

    void OnValidate()
    {
        mainMenuHandler.goToPage(titlePage);
        // titlePage.SetActive(true);
        // settingsPage.SetActive(false);
        // aboutPage.SetActive(false);
        // creditsPage.SetActive(false);
        // helpPage.SetActive(false);
    }

    /// <summary>
    /// closes the intro screen and starts the game
    /// </summary>
    void enterGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        print("start game");
    }
}
