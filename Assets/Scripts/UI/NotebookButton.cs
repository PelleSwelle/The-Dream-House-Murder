using UnityEngine;
using UnityEngine.UI;
using System;

public class NotebookButton : MonoBehaviour
{
    public Animator animator;
    public GameObject notebookObject;
    public Notebook notebook;
    public static event Action onToggle;

    void Start() => GetComponent<Button>().onClick.AddListener(() => toggleNotebook());

    public void toggleNotebook()
    {
        onToggle?.Invoke();
        if (notebook.isOpen)
        {
            animator.Play("closeNotebook");
        }
        else
        {
            animator.Play("openNotebook");
            // by default the opening page is the characters page
            notebook.goToPage(notebookObject.GetComponent<Notebook>().conversationsPage);
        }
        notebook.isOpen = !notebook.isOpen;
    }

}
