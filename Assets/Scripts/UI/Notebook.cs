using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// The Notebook class contains the player's current knowledge
/// this includes:
///     character sheet
///     conversation log
///     clues log
/// </summary>
public class Notebook : MonoBehaviour
{
    public GameObject notebook, conversationsPage, conversationPage;
    public Transform conversationsParent;

    public List<GameObject> pages;
    public AudioSource musicSource, soundFxSource;
    public bool isOpen;
    void OnValidate()
    {
        notebook = this.gameObject;
        pages = new List<GameObject> { conversationsPage, conversationPage };
    }

    void closeAllPages()
    {
        foreach (GameObject _page in pages)
        {
            _page.SetActive(false);
        }
    }

    public void goToPage(GameObject page, bool withSound)
    {
        if (withSound)
            soundFxSource.Play();

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
