using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionID
{
    public int val1, val2, val3;

    // ****** ONE DIGIT ******
    public QuestionID(int val1)
    {
        this.val1 = val1;
        this.val2 = 0;
        this.val3 = 0;
    }
    // ****** TWO DIGIT ******
    public QuestionID(int val1, int val2)
    {
        this.val1 = val1;
        this.val2 = val2;
        this.val3 = 0;
    }

    // ****** THREE DIGIT ******
    public QuestionID(int val1, int val2, int val3)
    {
        this.val1 = val1;
        this.val2 = val2;
        this.val3 = val3;
    }
}
