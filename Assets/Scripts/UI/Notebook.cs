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
    public Button conversationsButton, deductionButton;

    public GameObject notebook, conversationsPage, conversationPage, deductionPage;
    public Transform conversationsParent;

    public List<GameObject> pages;
    public AudioSource audioSource;
    // public Animator animator;
    public bool isOpen;
    void OnValidate()
    {
        notebook = this.gameObject;

        pages = new List<GameObject> { conversationsPage, conversationPage, deductionPage };
    }


    void Start()
    {
        conversationsButton.onClick.AddListener(() => goToPage(conversationsPage, true));
        deductionButton.onClick.AddListener(() => goToPage(deductionPage, true));
    }

    public void goToPage(GameObject page, bool withSound)
    {
        if (withSound)
            audioSource.Play();

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
