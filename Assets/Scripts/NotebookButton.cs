using UnityEngine;
using UnityEngine.UI;

public class NotebookButton : MonoBehaviour
{
    public Animator animator;
    public GameObject notebookObject;
    public AudioSource audioSource;
    public Notebook notebook;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => toggleNotebook());
    }
    /// <summary>
    /// sets the canvas group component attached to the notebook
    /// to active or inactive
    /// </summary>
    public void toggleNotebook()
    {
        if (notebook.isOpen)
        {
            print("closed notebook");
            animator.Play("closeNotebook");
            notebook.isOpen = false;
        }
        else
        {
            print("opened notebook");
            animator.Play("openNotebook");
            notebook.isOpen = true;
            // by default the opening page is the characters page
            notebook.goToPage(notebookObject.GetComponent<Notebook>().charactersPage, false);
        }
    }

}
