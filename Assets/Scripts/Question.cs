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