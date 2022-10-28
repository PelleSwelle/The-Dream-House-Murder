using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player
public class Player : MonoBehaviour
{
    public List<Testimony> testimonies;

    void Start()
    {
        testimonies = new List<Testimony>();
    }
}
