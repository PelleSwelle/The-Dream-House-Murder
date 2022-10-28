using UnityEngine;
public class Question
{
    public string line;

    public Answer[] possibleAnswers; // TODO: i think this is being done somewhere else.
    public Subject subject;
    public QuestionType questionType;

    public Question(string _line, Subject _subject, QuestionType _type = QuestionType.aboutYou)
    {
        this.line = _line;
        this.subject = _subject;
        this.questionType = _type;

        // TODO: delete this
        this.possibleAnswers = new Answer[]
        {
            new Answer("It hurts when I pee", true, "hurts when pees"),
            new Answer("My cousin is also my dad.", false, "cousin is dad"),
            new Answer("Proper hydration is the key to my powers", true, "hydration is power"),
            new Answer("I once ate a blackberry", false, "ate a blackberry")
    };
    }

    /// <summary>
    /// a random answer. Not intented for use in final product
    /// </summary>
    /// <returns>a random answer</returns>
    public Answer randomAnswer()
    {
        int answerSeed = Random.Range(0, possibleAnswers.Length);
        return possibleAnswers[answerSeed];
    }
}


// TODO: the two below might conflict
/// <summary>
/// The things to be checked against when in conversation
/// </summary>
public enum Subject
{
    // weapon
    weapon,
    locationOfMurder,
    locationOfBody,
    victim,
    character
}
public enum QuestionType
{
    aboutYou, // about the character (where where you, why where you there, etc.)
    aboutMurder, // about the murder (location, weapon, etc.)
    aboutVictim, // facts about the victim
    exposeLie, // catch in a lie
    confirmTestimony, // confirm something someone else has said
    aboutRelationship // relationship between two characters
}

// maybe just for thinking
public enum aboutYouQuestions
{
}
public enum aboutMurderQuestions
{

}
public enum aboutVictimQuestions
{

}
public enum exposeLieQuestions
{

}
public enum confirmTestimonyQuestions
{

}
public enum aboutRelationShipQuestions
{

}

