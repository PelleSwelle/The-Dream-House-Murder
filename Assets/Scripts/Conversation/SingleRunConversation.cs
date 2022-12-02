using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRunConversation : IConversation
{
    List<Question> IConversation.Questions
    {
        get => questions;
        set => questions = value;
    }

    public List<Question> questions;

    public SingleRunConversation(List<Question> _questions)
    {
        questions = _questions;
    }

    public List<Question> getEndPoints()
    {
        return questions.FindAll(x => x.isEndPoint);
    }

    public Question getQuestionByID(int val1, int val2 = 0, int val3 = 0)
    {
        return questions.Find(x => x.ID.val1 == val1 && x.ID.val2 == val2 && x.ID.val3 == val3);
    }
    public Question getLastAskedQuestion()
    {
        return questions.FindLast(x => x.hasBeenSaid == true);
    }

    public Question getFirstQuestion()
    {
        Question question = questions.Find(x => x.ID.val1 == 1);
        return question;
    }

    public void onFinish(Character character)
    {
        return;
    }
}
