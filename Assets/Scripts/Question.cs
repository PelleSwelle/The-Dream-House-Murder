/// <summary>
/// respresentation of a question in game.
/// questions and answers come in pairs
/// </summary>
public class Question : ISayable
{
    private string line;
    private int id_round;
    private int id_variant;
    private int[] id;
    public bool hasBeenSaid = false;
    public Question(int _idRound, int _idVariant, string _line)
    {
        this.id_round = _idRound;
        this.id_variant = _idVariant;
        this.line = _line;
        // gather the two variables in the id to one variable
        this.ID = new int[] { this.id_round, this.id_variant };
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
        get { return id_round; }
        set { id_round = value; }
    }
    public int ID_variant
    {
        get { return id_variant; }
        set { id_variant = value; }
    }

}



// public enum QuestionType
// {
//     aboutYou, // about the character (where where you, why where you there, etc.)
//     aboutMurder, // about the murder (location, weapon, etc.)
//     aboutVictim, // facts about the victim
//     exposeLie, // catch in a lie
//     confirmTestimony, // confirm something someone else has said
//     aboutRelationship // relationship between two characters
// }

// maybe just for thinking
// public enum aboutYouQuestions
// {
// }
// public enum aboutMurderQuestions
// {

// }
// public enum aboutVictimQuestions
// {

// }
// public enum exposeLieQuestions
// {

// }
// public enum confirmTestimonyQuestions
// {

// }
// public enum aboutRelationShipQuestions
// {

// }

