using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatableConversation : IConversation, IResetable
{
    public List<Question> questions;

    List<Question> IConversation.Questions { get => questions; set => questions = value; }

    public repeatableConversation(List<Question> _questions)
    {
        questions = _questions;
    }

    Question IConversation.getFirstQuestion()
    {
        return questions.Find(x => x.ID.val1 == 1);
    }

    Question IConversation.getLastAskedQuestion()
    {
        return questions.FindLast(x => x.hasBeenSaid);
    }

    Question IConversation.getQuestionByID(int _val1, int _val2, int _val3)
    {
        return questions.Find(x => x.ID.val1 == _val1 && x.ID.val2 == _val2 && x.ID.val3 == _val3);
    }

    List<Question> IConversation.getEndPoints()
    {
        return questions.FindAll(x => x.isEndPoint);
    }

    public void onFinish(Character character)
    {
        reset();
        unlockCharacterAct(character, 2);
    }

    void unlockCharacterAct(Character character, int actNumber)
    {
        character.goToAct(actNumber);
    }

    public void reset()
    {
        Debug.Log("reset questions");
        foreach (Question q in questions)
            q.hasBeenSaid = false;
    }
}
