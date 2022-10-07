using System.Collections;
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
    // BUTTONS
    public Button conversationsButton, cluesButton, CharactersButton;

    // canvas groups for controlling the alpha and interactability
    public GameObject notebook, cluesPage, conversationsPage, charactersPage, bioPage;

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
                StartCoroutine(openPage(.5f, page));
                // setGroupActive(_page);
                print("opened: " + page.ToString());
            }
        }
    }

    // TODO: this should be in uiHandler
    /// <summary>
    /// The animation for the notebook page turning
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    private IEnumerator openPage(float duration, GameObject page)
    {
        Vector3 targetPos = new Vector3(700, 1400, 0);
        Vector3 startPos = new Vector3(0, 0, 0);
        float t = 0f;
        while (t < duration)
        {
            page.transform.position = Vector3.Lerp(startPos, targetPos, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
