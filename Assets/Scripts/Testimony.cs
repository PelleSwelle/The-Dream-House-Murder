using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A representation of something a character has said. Fairly similar to an answer, but functioning in another way
/// </summary>
public class Testimony
{
    public Character character;
    public string testimony;
    public bool isTrue;

    // to be filled out by the player, during gameplay, maybe setting the color of a tile or something
    public bool believedToBeTrue;

    public Testimony(Character _character, string _testimony, bool _isTrue = true)
    {
        this.character = _character;
        this.testimony = _testimony;
        this.isTrue = _isTrue;
    }
}
