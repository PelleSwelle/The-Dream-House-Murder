using UnityEngine;
using UnityEngine.UI;

// TODO: put more stuff in here. 
[CreateAssetMenu(menuName = "UI Handler")]
public class UiHandler : ScriptableObject
{
    public GameObject[] pages;
    public GameObject backButton;

    public Button[] buttons;


    // TODO: this does not actually do anything
    public void goToPage(GameObject page)
    {
        foreach (GameObject _page in pages)
        {
            if (_page == page)
            {
                Debug.Log($"found: {_page}");
                // _page.SetActive(true);
            }
            else if (_page != page)
            {
                // _page.SetActive(false);
            }
        }
    }
}
