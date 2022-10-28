using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines an answer given from a character. can be either true or not true
/// </summary>
public class Answer
{
    public string line;
    public bool isTrue;

    // the statement is a sharp formulation of the answer. to be used for checking against various things
    public string statement; // TODO: possibly refactor this to it's own class

    // an answer can unlock a range of questions, depending on the nature of the question
    public Question[] unlocksQuestions;

    public Answer(string _line, bool _isTrue, string statement)
    {
        this.line = _line;
        this.isTrue = _isTrue;
    }
}
