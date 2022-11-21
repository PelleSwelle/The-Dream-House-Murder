using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

/// <summary>
/// Manager for handling everything that has to do with conversation
/// incl: UI, questions, answers and state
/// </summary>
public class ConversationManager : MonoBehaviour
{
    public List<Question> maryQuestions, boyfriendQuestions, reaQuestions;
    public List<Answer> maryAnswers, boyfriendAnswers, reaAnswers;
    public ConversationUI conversationUiHandler; // the script attached to the UI object
    public GameObject conversationUi; // the gameobject containing the ui

    public Character character;
    public Player player;

    public List<Question> currentlyAvailableQuestions; // dynamic list of available questions at any given time
    public GameManager gameManager; // the game manager
    public Notebook notebook; // the script attached to the notebook object
    public CharactersPage charactersPage; // the script attached to the characters page object
    public bool isInConversation; // whether the player is in a conversation or not
    public Character conversationPartner;

    // TODO: every "round" of conversation should check for unlocks
    void Start()
    {
        // ID structure: round.question
        conversationUi.SetActive(false);
        gameManager.mary.printCharacter();
        currentlyAvailableQuestions = new List<Question>();
        
         gameManager.mary.conversation = new Conversation(
            new List<Question>()
            {
                new Question(1, 1, "Who are you to Olivia"),
                new Question(2, 1, "What were you doing that day?"),
                new Question(2, 2, "How many were you in the house that day?"),
                new Question(3, 1, "Where were you at the time of the murder of Olivia?"),
                new Question(4, 1, "Why did you leave the house?"),
                new Question(4, 2, "Why wasn't Olivia with you?"),
                new Question(5, 1, "Why did you leave the house?"),
                //new Question(5, 2, "Why didn't you bring Olivia with you?"),
                new Question(6, 1, "Who was on the phone?"),
                new Question(6, 2, "When did you return to the house?"),
                new Question(7, 1, "Did the two of you fight?"),
                new Question(7, 2, "Do you know the real estate agent?"),
                new Question(7, 3, "Did you see who it was?"),
                new Question(7, 4, "Was Mujaffa with you?"),
                new Question(8, 1, "Who do you suspect the murderer to be?")
            },
            new List<Answer>()
            {
                new Answer(0, 1, "Hello, I am Mary and this is my opening line"),
                new Answer(1, 1, "I'm her best friend. We do everything together, who could do this?"),
                new Answer(2, 1, "Me and Olivia were just playing cards and talking about my relationship issues while playing cards"),
                new Answer(2, 2, "It was me and Olivia in our room with Mujaffa in the living room watching sports"),
                new Answer(3, 1, "I was drinking a local bar to calm down..."),
                new Answer(4, 1, "We had an argument, that made me upset."),
                new Answer(4, 2, "We had an argument, that made me upset."),
                new Answer(5, 1, "I heard her speaking to another guys on the phone. Which made me angry because, she has a boyfriend, so I ran away"),
                //new Answer(5, 2, "I heard her speaking to another guys on the phone. Which made me angry because, she has a boyfriend, so I ran away"),
                new Answer(6, 1, "Her secret lover, I belive he's an Real-Estate Agent"),
                new Answer(6, 2, "I returned to the house an hour later, and saw someone running out of the house."),
                new Answer(7, 1, "No we would never! I left as soon as I felt it was necessary to escape the argument"),
                new Answer(7, 2, "No, I just hear Olivia speak to him over the phone very often"),
                new Answer(7, 3, "It was quite dark, so i could't see his face. But it did't look like Mujaffa. "),
                new Answer(7, 4, "No he was not, he was going for the pizza, when I left the house."),
                new Answer(8, 1, "It must have been that person running away! If only I knew who it was! How could he just do this"),
            });

        gameManager.officer.conversation = new Conversation(
            new List<Question>()
            {
                new Question(1, 1, "Who was Involved?"),
                new Question(2, 1, "What time did this happen?"),
                new Question(3, 1, "Who was at home when you arrived?"),
                new Question(4, 1, "Did you examine the dead body?"),
                new Question(5, 1, "Do you have any idea who the murderer is?")
            },
            new List<Answer>()
            {
                new Answer(0, 1, "Hello, I am police person."),
                new Answer(1, 1, "Based on the information we have gathered, the possible suspects are Mary, boyfriend and REA"),
                new Answer(2, 1, "We arrived at the crime scene at 1 am"),
                new Answer(3, 1, "It was only Mary, who was the one that called the cops. Mujaffa showed up 30 minutes later."),
                new Answer(4, 1, "Yes, the victim was stabbed multiple times by a kitchen knife"),
                new Answer(5, 1, "No, based on the information we have gathered from each of the suspects, the murderer could be any of them...")
            }
        );
        gameManager.rea.conversation = new Conversation(
            new List<Question>()
            {
                new Question(1, 1, "Who are you to Olivia?"),
                new Question(2, 1, "How long have you known Olivia for?"),
                new Question(2, 2, "When did you speak with Olivia?"),
                new Question(3, 1, "Where were you at the time of the murder of Olivia?"),
                new Question(4, 1, "Did you know that Olivia has a boyfriend?"),
                new Question(4, 2, "Did you and Olivia ever meet up?"),
                new Question(5, 1, "Why did you call Olivia?"),
                new Question(6, 1, "Why did you want to buy her house?"),
                new Question(6, 2, "Why did the call end?"),
                new Question(7, 1, "Did olivia refuse to sell her house?"),
                new Question(7, 2, "Who will own the house now?"),
                new Question(7, 3, "Did you go to the house afterwards?"),
                new Question(7, 4, "Did you have a secret relationship with Olivia?"),
                new Question(8, 1, "Who do you suspect the murderer to be?")
            },
            new List<Answer>()
            {
                new Answer(1, 1, "I am her real estate agent"),
                new Answer(2, 1, "About 6 months now"),
                new Answer(2, 2, "I called her last week", false),
                new Answer(3, 1, "I was at home by myself.", false),
                new Answer(4, 1, "Yes, but she didn't mention him very often"),
                new Answer(4, 2, "Occasionally yes"),
                new Answer(5, 1, "I was just talking to Olivia about selling me her house"),
                new Answer(6, 1, "It was for profit"),
                new Answer(6, 2, "I heard an argument between her and her friend which ended up with screams, so I hung up"),
                new Answer(7, 1, "Yes, she kept mentioning that she will keep it as a memory of her"),
                new Answer(7, 2, "Uh due to her unfortunate death, the house will go up for sale"),
                new Answer(7, 3, "No! I did not want to get involved in their drama, so I remained silent"),
                new Answer(7, 4, "Uhm I guess you could say that. We started to like each other more over time"),
                new Answer(8, 1, "I have no idea! I did not expect this to ever even happen. I should'nt even be here!")
    }
        );
        gameManager.boyfriend.conversation = new Conversation(
            new List<Question>()
            {
                new Question(1, 1, "Who are you to Olivia?"),
                new Question(2, 1, "How many were you guys that day?"),
                new Question(2, 2, "What were you guys doing at the house?"),
                new Question(3, 1, "Where were you at the time of the murder of Olivia?"),
                new Question(4, 1, "Where was the pizzas ordered from?"),
                new Question(4, 2, "Who ordered the pizzas?"),
                new Question(5, 1, "How long did it take you to collect the pizzas?"),
                new Question(6, 1, "Why so long?"),
                new Question(6, 2, "Why did you bring the pizzas?"),
                new Question(7, 1, "When did you return home with the pizzas?"),
                new Question(7, 2, "Who is Mary to you?"),
                new Question(8, 1, "Who do you suspect the murderer to be?")
            },
            new List<Answer>()
            {
                new Answer(1, 1, "I'm her boyfriend for the past 3 years"),
                new Answer(2, 1, "We were just three people"),
                new Answer(2, 2, "We were having fun talking about vacation ideas together"),
                new Answer(3, 1, "I was on my way to gcollect the pizzas that we ordered beforehand.", false),
                new Answer(4, 1, "They were from Papa Jones"),
                new Answer(4, 2, "It was Mary who ordered them! "),
                new Answer(5, 1, "It took me around an 45 minutes to get there", false),
                new Answer(6, 1, "I drove into too much traffic which caused a big delay", false),
                new Answer(6, 2, "I dont know, It must have been Mary who set this up!", false),
                new Answer(7, 1, "I came home 1 hour later and was too late, all the cops were there", false),
                new Answer(7, 2, "I thought she was Olivias best friend. It  appears they argued a lot lately which seems strange"),
                new Answer(8, 1, "It has to be Mary! She setup all of this and called the cops")
            }
        );
    }

    public Answer getAnswerByQuestion(Character character, Question question)
    {
        return character.conversation.answers.Find(x => x.ID_round == question.ID_round);
    }

    public Answer getAnswerByID(int[] id, List<Answer> list)
    {
        return list.Find(x => x.ID == id);
    }

    /// <summary>
    /// gets the opening line from the character.
    /// the opening line is the Answer with id: 0.1f
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    Answer getOpeningLine(Character character)
    {
        Answer openingLine = character.conversation.answers.Find(x => x.ID_round == 0);
        return openingLine;
    }


    /// <summary>
    /// updates the currently available questions
    /// </summary>
    void updateAvailableQuestions(Character conversationPartner)
    {
        currentlyAvailableQuestions.Clear();

        int questionRound = getAvailableQuestionRound(conversationPartner);

        List<Question> newlyAvailableQuestions = new List<Question>();
        foreach (Question q in conversationPartner.conversation.questions)
        {
            if (q.ID_round == questionRound)
                newlyAvailableQuestions.Add(q);
        }

        currentlyAvailableQuestions.AddRange(newlyAvailableQuestions);

        conversationUiHandler.updateQuestionButtons();
    }

    /// <summary>
    /// gets the id of the next available round of questions. 
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    int getAvailableQuestionRound(Character character)
    {
        Answer lastAnswer = character.conversation.answers.FindLast(x => x.hasBeenSaid == true);
        // TODO: if no more questions
        return lastAnswer.ID_round + 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="character"></param>
    /// <param name="inputID"></param>
    /// <returns></returns>
    List<Question> getQuestionRoundByID(Character character, int idRound)
    {
        List<Question> questions = new List<Question>();
        questions.Add(character.conversation.questions.Find(x => x.ID_round == idRound));
        return questions;
    }

    /// <summary>
    /// dispatches a question, updates the UI and updates the available questions
    /// </summary>
    /// <param name="question"></param>
    /// <param name="character"></param>
    public void askQuestion(Question question, Character character)
    {
        question.hasBeenSaid = true; // tag the question in the list

        Answer answer = getAnswerByQuestion(character, question);
        print("hasbeensaid: " + answer.hasBeenSaid);
        answer.hasBeenSaid = true;
        print("this should be true: " + getAnswerByQuestion(character, question).hasBeenSaid);
        conversationUiHandler.updateAnswerField(answer);
        updateAvailableQuestions(character);
        printAvailableQuestions();

        conversationUiHandler.updateQuestionButtons();

        // notification
        StartCoroutine(conversationUiHandler.showNotification("asked a question", .5f));
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
    Answer getAnswerByID(int _idRound, int _idVariant, Character character)
    {
        return character.conversation.answers.Find(x => x.ID_round == _idRound);
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
        conversationPartner = character;
        print(conversationPartner.firstName);
        // start with a check to see wether to add the character to the character screen
        if (!character.hasBeenTalkedTo)
        {
            character.hasBeenTalkedTo = true;
            notebook.addCharacterToConversations(character);
            // add the character to the characters page
            charactersPage.populateButton(charactersPage.getNextAvailableButton(), character);
        }

        character.conversation.answers.Find(x => x.ID_round == 0).hasBeenSaid = true;
        updateAvailableQuestions(character);

        conversationUi.SetActive(true);
        conversationUiHandler.updateAnswerField(getOpeningLine(character));
        conversationUiHandler.setCharacterImage(character);
    }

    /// <summary>
    /// Sets currentConversation to null and closes the UI
    /// </summary>
    public void leaveConversation()
    {
        conversationPartner = null;
        print("left conversation");
        conversationUi.SetActive(false);
    }

    void printAvailableQuestions()
    {
        foreach (Question q in currentlyAvailableQuestions)
        {
            print("Question: " + q.sentence);
        }
    }
}
