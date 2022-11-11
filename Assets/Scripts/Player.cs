using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player
public class Player : MonoBehaviour
{
    public List<Testimony> testimonies;
    public List<Question> questions;

    void Start()
    {
        testimonies = new List<Testimony>();
    }
}
