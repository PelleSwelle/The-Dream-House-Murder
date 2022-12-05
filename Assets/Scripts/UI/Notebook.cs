using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Notebook : MonoBehaviour
{
    public GameObject conversationsPage, messengerPage;

    public List<GameObject> pages;
    public bool isOpen;
    public static event Action onPageTurn;

    void Start() => pages = new List<GameObject> { conversationsPage, messengerPage };

    void closeAllPages()
    {
        foreach (GameObject _page in pages)
            _page.SetActive(false);
    }

    public void goToPage(GameObject page)
    {
        onPageTurn?.Invoke();

        foreach (GameObject _page in this.pages)
        {
            if (_page == page)
                _page.SetActive(true);
            else
                _page.SetActive(false);
        }
    }
}
