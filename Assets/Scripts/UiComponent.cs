using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// General fields and functions all UI components
/// </summary>
public class UiComponent : ScriptableObject
{
    // every UI component has pages
    CanvasGroup[] pages;

    /// <summary>
    /// Navigation in the notebook
    /// opens a page in the notebook, while simultaneously closing the others
    /// </summary>
    /// <param name="page">CanvasGroup</param>
    public void goToPage(CanvasGroup page)
    {
        foreach (CanvasGroup _page in this.pages)
        {
            if (_page != page)
            {
                setGroupInactive(_page);
            }
            else if (_page == page)
            {
                setGroupActive(_page);
                Debug.Log("opened: " + page.ToString());
            }
        }
    }

    // helper functions for toggling canvasgroups
    /// <summary>
    /// sets alpha to 1 and enables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    public void setGroupActive(CanvasGroup group)
    {
        // set the alpha all the way up, so it is visible
        group.alpha = 1f;
        // make the group interactable
        group.blocksRaycasts = true;
    }
    /// <summary>
    /// sets alpha to 0 and disables raycasting for the group
    /// </summary>
    /// <param name="group"></param>
    public void setGroupInactive(CanvasGroup group)
    {
        group.alpha = 0f; // make the group invisible
        group.blocksRaycasts = false; // make the group uninteractable
    }
}
