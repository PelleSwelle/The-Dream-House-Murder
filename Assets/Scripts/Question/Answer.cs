using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines an answer given from a character. can be either true or not true
/// </summary>
public class Answer
{
    public string sentence;
    public bool isTrue, hasBeenSaid = false;

    public Answer(string _sentence, bool _isTrue = true)
    {
        this.sentence = _sentence;
        this.isTrue = _isTrue;
    }
}
