/// <summary>
/// respresentation of a question in game.
/// questions and answers come in pairs
/// </summary>
public class Question
{
    public Answer answer;
    public string sentence;
    public QuestionID ID;
    public bool hasBeenSaid;
    // public QuestionID unlocksQuestion, unlocks2;
    public bool hasBranches, isEndPoint;
    public Question(int id1, int id2, int id3, string _sentence, Answer _answer, bool _hasBranches = false, bool _isEndPoint = false)
    {
        ID = new QuestionID(id1, id2, id3);
        sentence = _sentence;
        answer = _answer;
        hasBranches = _hasBranches;
        isEndPoint = _isEndPoint;

        hasBeenSaid = false;
    }
}