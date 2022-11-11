using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines an answer given from a character. can be either true or not true
/// </summary>
public class Answer : ISayable
{
    // TODO: some way of checking whether the answer has been given
    private string line;
    private int idRound, idVariant;
    private int[] id;
    public bool isTrue;
    public bool hasBeenSaid = false;

    public Question[] unlocks; // TODO: do this

    // an answer can unlock a range of questions, depending on the nature of the question
    public Question[] unlocksQuestions;

    public Answer(int _idRound, int _idVariant, string _line, bool _isTrue = true, Question[] _unlocks = null)
    {
        this.line = _line;
        this.idRound = _idRound;
        this.idVariant = _idVariant;
        this.ID = new int[] { this.idRound, this.idVariant };
        this.isTrue = _isTrue;
        this.unlocks = _unlocks;
    }

    public int[] ID
    {
        get { return id; }
        set { id = value; }
    }

    public string sentence
    {
        get { return line; }
        set { line = value; }
    }

    public int ID_round
    {
        get { return idRound; }
        set { idRound = value; }
    }
    public int ID_variant
    {
        get { return idVariant; }
        set { idVariant = value; }
    }


    public Question getUnlockedQuestions(Character character, float answerId)
    {
        // the character
        // return character
        throw new System.NotImplementedException();
    }
}
