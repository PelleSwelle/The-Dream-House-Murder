using UnityEngine;
using UnityEngine.UI;

public class NotebookButton : MonoBehaviour
{
    public Animator notebookAnimator;
    public GameObject notebook;
    public AudioSource audioSource;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => toggleNotebook());
        notebookAnimator.SetBool("isOpen", false);
    }
    /// <summary>
    /// sets the canvas group component attached to the notebook
    /// to active or inactive
    /// </summary>
    public void toggleNotebook()
    {
        bool isOpen = notebookAnimator.GetBool("isOpen");
        print("notebook state: " + isOpen.ToString());
        audioSource.Play();

        if (isOpen)
        {
            notebookAnimator.SetBool("isOpen", false);
            print("closed notebook");
        }
        else if (!isOpen)
        {
            print("opened notebook");
            notebookAnimator.SetBool("isOpen", true);
        }
    }
}
