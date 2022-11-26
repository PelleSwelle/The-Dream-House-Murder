using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;
// using System;

/// <summary>
/// Manager for handling everything that has to do with conversation
/// incl: UI, questions, answers and state
/// </summary>
public class ConversationManager : MonoBehaviour
{
    public ConversationsPage conversationPage;
    public AudioClip[] voiceLinesMale, voiceLinesFemale;
    public AudioSource audioSource;
    public ConversationUI conversationUiHandler; // the script attached to the UI object
    public GameObject conversationUi; // the gameobject containing the ui
    public List<Question> currentlyAvailableQuestions; // dynamic list of available questions at any given time
    public GameManager gameManager; // the game manager
    // public Notebook notebook; // the script attached to the notebook object
    public bool isInConversation; // whether the player is in a conversation or not
    public Character conversationPartner;
    // public ConversationTile maryTile, boyfriendTile, officerTile, reaTile;

    void Start()
    {
        conversationUi.SetActive(false);
        currentlyAvailableQuestions = new List<Question>();
        setCharacterConversations();
    }

    /// <summary> gets the unlocked question in the case there is only one </summary>
    Question getUnlockedQuestion(Character character, Question question)
    {
        Question unlockedQuestion = character.questions.Find(x => x.ID.val1 == question.ID.val1 + 1);
        return unlockedQuestion;
    }


    /// <summary> gets a list of the unlocked questions in the case there are two </summary>
    List<Question> getUnlockedQuestions(Character character, Question question)
    {
        List<Question> unlockedQuestions = new List<Question>();
        QuestionID lastQuestionID = question.ID;

        // first value always goes forward
        int firstVal = lastQuestionID.val1 + 1;
        // second and third can be beside each other
        int secondVal, thirdVal;

        // seeing whether we come from one digit or two
        bool comesFromSingleDigit = lastQuestionID.val2 == 0;
        bool comesFromTwoDigit = lastQuestionID.val2 != 0 && lastQuestionID.val3 == 0;

        if (comesFromSingleDigit)
        {
            print("comes from single digit");
            // first question
            secondVal = 1;
            unlockedQuestions.Add(getQuestionByID(character, firstVal, secondVal));

            // second question
            secondVal = 2;
            unlockedQuestions.Add(getQuestionByID(character, firstVal, secondVal));
        }
        else if (comesFromTwoDigit)
        {
            print("comes from double digit");
            secondVal = lastQuestionID.val2;
            // first question
            thirdVal = 1;
            unlockedQuestions.Add(getQuestionByID(character, firstVal, secondVal, thirdVal));
            // second question
            thirdVal = 2;
            unlockedQuestions.Add(getQuestionByID(character, firstVal, secondVal, thirdVal));
        }
        else
            print("uuuhhhh");

        return unlockedQuestions;
    }


    /// <summary> gets the question with the matching ID</summary>
    Question getQuestionByID(Character character, int val1, int val2 = 0, int val3 = 0)
    {
        return character.questions.Find(x => x.ID.val1 == val1 && x.ID.val2 == val2 && x.ID.val3 == val3);
    }


    void updateAvailableQuestions(Character conversationPartner)
    {
        currentlyAvailableQuestions.Clear(); // remove current questions from list of available questions

        bool nothingAskedYet = conversationPartner.getLastAskedQuestion() == null;

        if (nothingAskedYet)
            currentlyAvailableQuestions.Add(conversationPartner.questions.Find(x => x.ID.val1 == 1));
        else
        {
            if (conversationPartner.getLastAskedQuestion().isEndPoint)
            {
                print("no mas questionosos");
                return;
            }
            else
            {
                bool latestQuestionBranchesOut = conversationPartner.getLastAskedQuestion().hasBranches;

                if (latestQuestionBranchesOut)
                {
                    List<Question> unlockedQuestions = getUnlockedQuestions(conversationPartner, conversationPartner.getLastAskedQuestion());
                    currentlyAvailableQuestions.AddRange(unlockedQuestions);
                }
                else
                {
                    Question unlockedQuestion = getUnlockedQuestion(conversationPartner, conversationPartner.getLastAskedQuestion());
                    print("the question continues linearly: " + unlockedQuestion.sentence);
                    currentlyAvailableQuestions.Add(unlockedQuestion);
                }
            }

        }
        conversationUiHandler.updateQuestionButtons();
    }

    public void askQuestion(Question question, Character character)
    {
        if (character.gender == "female")
            playRandomFemaleVoiceClip();
        else
            playRandomMaleVoiceClip();

        question.hasBeenSaid = true;
        conversationUiHandler.updateAnswerField(question.answer);
        updateAvailableQuestions(character);
        conversationUiHandler.updateQuestionButtons();
    }

    public void playRandomFemaleVoiceClip()
    {
        int index = Random.Range(0, voiceLinesFemale.Length);
        audioSource.PlayOneShot(voiceLinesFemale[index]);
    }


    public void playRandomMaleVoiceClip()
    {
        int index = Random.Range(0, voiceLinesMale.Length);
        audioSource.PlayOneShot(voiceLinesMale[index]);
    }


    public void initConversation(Character character)
    {
        if (character.gender == "female")
            playRandomFemaleVoiceClip();
        else
            playRandomMaleVoiceClip();

        conversationPartner = character;
        print(conversationPartner.firstName);

        character.hasBeenTalkedTo = true;
        // notebook.addCharacterToConversations(character);
        // charactersPage.populateButton(charactersPage.getNextAvailableButton(), character);

        updateAvailableQuestions(character);

        conversationUi.SetActive(true);
        conversationUiHandler.displayOpeningLine(character);
        conversationUiHandler.setCharacterImage(character);
    }


    public void leaveConversation()
    {
        conversationPartner.hasBeenTalkedTo = true;

        Answer lastGivenAnswer = conversationPartner.questions.FindLast(x => x.hasBeenSaid = true).answer;



        // sorry. Was going fast
        // if (conversationPartner == gameManager.mary)
        //     maryTile.updateAnswer(lastGivenAnswer);
        // else if (conversationPartner == gameManager.officer)
        //     officerTile.updateAnswer(lastGivenAnswer);
        // else if (conversationPartner == gameManager.boyfriend)
        //     boyfriendTile.updateAnswer(lastGivenAnswer);
        // else if (conversationPartner == gameManager.rea)
        //     reaTile.updateAnswer(lastGivenAnswer);

        conversationPartner = null;
        print("left conversation");
        conversationUi.SetActive(false);
    }


    void setCharacterConversations()
    {
        gameManager.mary.questions = QuestionConstants.maryQuestions;
        gameManager.officer.questions = QuestionConstants.officerQuestions;
        gameManager.rea.questions = QuestionConstants.harryQuestions;
        gameManager.boyfriend.questions = QuestionConstants.jamesQuestions;
    }
}
