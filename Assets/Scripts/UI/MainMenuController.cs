using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour
{
    public GameObject titlePage, settingsPage, aboutPage, creditsPage, helpPage;
    public Button playButton, settingsButton, aboutButton, creditsButton, helpButton;
    public GameObject backObject;
    public List<GameObject> pages;
    public static event Action onButtonPress;
    public static event Action onOpen;

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
    void enterGame() => SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

    void openMenu() => onOpen?.Invoke();

    public void goToPage(GameObject page)
    {
        onButtonPress?.Invoke();

        if (page == titlePage)
            backObject.SetActive(false);
        else
            backObject.SetActive(true);

        foreach (GameObject _page in this.pages)
        {
            if (_page == page)
                _page.SetActive(true);
            else
                _page.SetActive(false);
        }
    }
}
