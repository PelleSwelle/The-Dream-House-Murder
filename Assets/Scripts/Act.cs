using System.Collections.Generic;

public class Act
{
    public int actNumber;
    public List<Question> questions;
    public bool isUnlocked = false;
    public Act(int _actNumber, List<Question> _questions)
    {
        actNumber = _actNumber;
        questions = _questions;
    }

    public Question getQuestionByID(int val1, int val2 = 0, int val3 = 0)
    {
        return questions.Find(x => x.ID.val1 == val1 && x.ID.val2 == val2 && x.ID.val3 == val3);
    }
}
