using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConversation
{
    List<Question> Questions { get; set; }
    Question getFirstQuestion();
    Question getLastAskedQuestion();
    Question getQuestionByID(int val1, int val2 = 0, int val3 = 0);
    List<Question> getEndPoints();
    void onFinish(Character character);
}
