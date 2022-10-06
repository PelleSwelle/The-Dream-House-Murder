using UnityEngine;
using UnityEngine.UI;

public class NotebookButton : MonoBehaviour
{
    public GameObject notebook;

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
        if (notebook.activeInHierarchy)
        {
            notebook.SetActive(false);
            Debug.Log("closed notebook");
        }
        else if (!notebook.activeInHierarchy)
        {
            notebook.SetActive(true);
            Debug.Log("opened notebook");
        }
    }
}
