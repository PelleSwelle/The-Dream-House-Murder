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

    public void toggleNotebook()
    {
        if (notebook.isOpen)
        {
            animator.Play("closeNotebook");
            notebook.isOpen = false;
            audioSource.Play();
        }
        else
        {
            animator.Play("openNotebook");
            notebook.isOpen = true;
            // by default the opening page is the characters page
            notebook.goToPage(notebookObject.GetComponent<Notebook>().conversationsPage, withSound: true);
        }
    }

}
