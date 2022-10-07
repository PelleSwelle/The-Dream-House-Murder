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
                _page.SetActive(true);
            }
            else if (_page != page)
            {
                _page.SetActive(false);
            }
        }
    }

    // TODO: This is dumb. Delete this
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
}
