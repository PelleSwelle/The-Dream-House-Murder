using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Cursor : MonoBehaviour
{
    public GameObject cursor;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;
    public bool cursorIsVisible = true;

    void OnValidate()
    {
        cursor = this.transform.GetChild(0).gameObject;
    }
    void Start()
    {
        cursor.SetActive(cursorIsVisible);
    }


}
