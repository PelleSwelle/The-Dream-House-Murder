using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // we need the notebook to grab some functions, that we also need here.
    // this is not a very elegant way of doing it, but I haven't come up with something else.
    public Notebook notebook;
    // getting all of the child pages
    public CanvasGroup titlePage, settingsPage, aboutPage, creditsPage, helpPage;
    public CanvasGroup[] pages;
    public Button playButton, settingsButton, aboutButton, creditsButton, helpButton;
    public Button[] titleButtons;

    void Start()
    {
        this.pages = new CanvasGroup[] { titlePage, settingsPage, aboutPage, creditsPage, helpPage };
        // start on the title screen
        notebook.goToPage(titlePage);

        Button playListener = playButton.GetComponent<Button>();
        // setting listeners
        playListener.onClick.AddListener(goToPlay);
    }

    void goToPlay()
    {
        notebook.setGroupInactive(this.GetComponent<CanvasGroup>());
        print("start game");
    }

    void Update()
    {

    }
}
