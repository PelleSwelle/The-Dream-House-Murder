using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// Manager for handling everything that has to do with conversation
/// incl: UI, questions, answers
/// </summary>
public class ConversationManager : MonoBehaviour
{
    public ConversationUI conversationUiHandler;
    public GameObject conversationUi;

    public Character character;
    public Player player;

    public Question[] currentlyAvailableQuestions;
    [SerializeField] Conversation[] conversations;
    public GameManager gameManager;
    public Conversation currentConversation;
    public Notebook notebook;
    public CharactersPage charactersPage;

    void Start()
    {
        conversationUi.SetActive(false);
    }

    /// <summary>
    /// toggles the active state of the conversation UI
    /// </summary>
    void toggleConversationUi()
    {
        if (conversationUi.activeSelf)
        {
            conversationUi.SetActive(false);
        }
        else if (!conversationUi.activeSelf)
        {
            conversationUi.SetActive(true);
        }
    }

    /// <summary>
    /// computes 4 questions from what the player knows about
    /// </summary>
    /// <returns>Question[4]</returns>
    Question[] computeAvailableQuestions()
    {
        // get stuff from gameManager.hasHeard to determine what the player knows about
        // TODO: do some fishy stuff to find out what questions are available to the player.
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Initiate a conversation with the given character
    /// instantiates a new conversation, 
    /// opens the conversationUI, 
    /// populates the the answer from the character.
    /// sets the character image at the UI
    /// populates the questions in the UI
    /// </summary>
    /// <param name="character"></param>
    public void initConversation(Character character)
    {
        character.printState();
        // start with a check to see wether to add the character to the character screen
        if (!character.hasBeenTalkedTo)
        {
            character.hasBeenTalkedTo = true;
            notebook.addCharacterToConversations(character);

            // add the character to the characters page
            charactersPage.populateButton(charactersPage.getNextAvailableButton(), character);
        }

        currentConversation = new Conversation(character);

        conversationUi.SetActive(true);
        conversationUiHandler.updateAnswerField(new Answer("Hemnlo am person", true, "own name is person"));
        conversationUiHandler.setCharacterImage(character);
        updateAvailableQuestions();
    }

    /// <summary>
    /// populates the question buttons with the currently available questions
    /// </summary>
    void updateAvailableQuestions()
    {
        updateCurrentlyAvailableQuestions();
        conversationUiHandler.updateQuestionButtons(currentlyAvailableQuestions);
    }

    /// <summary>
    /// Sets currentConversation to null and closes the UI
    /// </summary>
    public void endConversation()
    {
        currentConversation = null;
        conversationUi.SetActive(false);
    }

    public void onChooseQuestion(Question question)
    {
        Answer answer = computeAnswer(question, currentConversation.character);
        conversationUiHandler.updateAnswerField(answer);
        currentConversation.character.hasBeenTalkedToAbout.Add(question.subject);

        // notification
        StartCoroutine(conversationUiHandler.showNotification("asked a question", .5f));
    }

    /// <summary>
    /// updates the currently available answers depending on what testimonies the character has heard.
    /// </summary>
    void updateCurrentlyAvailableQuestions()
    {
        // check what the player has to go on
        currentlyAvailableQuestions = new Question[] {
            new Question("Where are my pants?", Subject.locationOfMurder),
            new Question("Where is my shirt?", Subject.locationOfMurder),
            new Question("Why can I never have cake?", Subject.weapon),
            new Question("What weapon was used?", Subject.locationOfMurder)
        };
        // set the question to currently available
    }

    /// <summary>
    /// computes an answer to the given question
    /// depending on the whether the character knows something and is truthful
    /// at the moment, there are three outcomes:
    /// knowsnothing, true
    /// knowsNothing, false
    /// knowsSomething, true
    /// </summary>
    /// <param name="question"></param>
    /// <param name="character"></param>
    /// <returns type="answer"> the computed answer</returns>
    Answer computeAnswer(Question question, Character character)
    {
        QuestionType isAbout = question.questionType;
        // ************* WHAT KIND OF QUESTION IS IT *************

        // TODO: fit all the other stuff in here.
        if (isAbout == QuestionType.aboutYou)
        {

        }
        else if (isAbout == QuestionType.aboutMurder)
        {

        }
        else if (isAbout == QuestionType.aboutVictim)
        {

        }
        else if (isAbout == QuestionType.exposeLie)
        {

        }
        else if (isAbout == QuestionType.confirmTestimony)
        {

        }
        else if (isAbout == QuestionType.aboutRelationship)
        {

        }

        // TODO: this should also account for type of question
        // ************* CHARACTER KNOWS NOTHING *************
        if (!character.knowsAbout.Contains<Subject>(question.subject))
        {
            return new Answer("I know nothing about that.", true, null);
        }

        // ************* CHARACTER KNOWS *************
        else
        {
            // ************* CHARACTER LIES *************
            if (character.liesAbout.Contains<Subject>(question.subject))
            {
                // TODO: this should also contain the reason for lying
                return new Answer($"I know nothing about {question.subject}", false, $"doesn't know {question.subject}");
            }

            // ************* CHARACTER IS HONEST *************
            else
            {
                Answer answer = new Answer($"I know something about {question.subject}", true, $"knows about {question.subject}");
                // enter a new testimony into the notebook
                player.testimonies.Add(new Testimony(character, answer.line, true));

                return answer;
            }
        }
    }

}
