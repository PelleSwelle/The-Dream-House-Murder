using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UI Handler")]
public class UiHandler : ScriptableObject
{
    public GameObject[] pages;
    public GameObject backButton;

    public Button[] buttons;


    public void goToPage(GameObject page)
    {
        foreach (GameObject _page in pages)
        {
            if (_page == page)
            {
                Debug.Log($"found: {_page}");
                setGroupActive(_page);
            }
            else if (_page != page)
            {
                Debug.Log($"{_page} is not the one");
                setGroupInactive(_page);

            }
        }
    }

    public void close()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
    }

    public void open(GameObject uiComponent)
    {
        // set to active
        uiComponent.SetActive(true);
        uiComponent.transform.GetChild(0).gameObject.SetActive(true);
    }

    // helper functions for toggling canvasgroups
    /// <summary>
    /// sets alpha to 1 and enables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    void setGroupActive(GameObject group)
    {
        // set the alpha all the way up, so it is visible
        group.GetComponent<CanvasGroup>().alpha = 1f;
        // make the group interactable
        group.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// sets alpha to 0 and disables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    void setGroupInactive(GameObject group)
    {
        group.GetComponent<CanvasGroup>().alpha = 0f; // make the group invisible
        group.GetComponent<CanvasGroup>().blocksRaycasts = false; // make the group uninteractable
    }


    // foreach (GameObject _page in pages)
    // {
    //     // TODO: Why does this work in reverse from what I think?
    //     if (_page != pageToGoTo)
    //     {
    //         pageToGoTo.SetActive(false);
    //         Debug.Log($"set {pageToGoTo.name} to false");
    //     }
    //     else if (_page == pageToGoTo)
    //     {
    //         pageToGoTo.SetActive(true);
    //         Debug.Log("opened: " + pageToGoTo.ToString());
    //     }
    // }

    // public void showBack()
    // {
    //     // only show the back button if the player is on another page than main
    //     if (!pages[0].activeSelf)
    //     {
    //         backButton.SetActive(true);
    //     }
    //     else
    //     {
    //         backButton.SetActive(false);
    //     }
    // }
}
