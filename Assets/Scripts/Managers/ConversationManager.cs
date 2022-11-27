using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;
// using System;

/// <summary>
/// Delegating all tasks that have to do with conversations
/// </summary>
public class ConversationManager : MonoBehaviour
{
    #region memberVariables
    public GameObject conversationUi; // the gameobject containing the ui
    public ConversationUI conversationUiHandler; // the script attached to the UI object
    public ConversationsPage conversationPage;

    public AudioClip[] voiceLinesMale, voiceLinesFemale;
    public AudioSource audioSource;

    public List<Question> currentlyAvailableQuestions; // dynamic list of available questions at any given time
    public GameManager gameManager; // the game manager
    public Character currentConversationCharacter;
    #endregion

    void Start()
    {
        conversationUi.SetActive(false);
        currentlyAvailableQuestions = new List<Question>();
        setInitialConversations();
    }

    /// <summary> gets the unlocked question in the case there is only one </summary>
    Question getUnlockedQuestion(Character character, Question previousQuestion)
    {
        Question unlockedQuestion = character.questionsInCurrentAct.Find(x => x.ID.val1 == previousQuestion.ID.val1 + 1);
        return unlockedQuestion;
    }

    /// <summary> gets a list of the unlocked questions in the case there are two </summary>
    List<Question> getUnlockedQuestions(Character character, Question question)
    {
        List<Question> unlockedQuestions = new List<Question>();
        QuestionID lastQuestionID = question.ID;

        int firstVal = lastQuestionID.val1 + 1;

        // seeing whether we come from one digit or two
        bool incomingQuestionWasSingleDigit = lastQuestionID.val2 == 0;
        bool incomingQuestionWasTwoDigit = lastQuestionID.val2 != 0 && lastQuestionID.val3 == 0;

        if (incomingQuestionWasSingleDigit)
        {
            unlockedQuestions.Add(character.currentAct.getQuestionByID(firstVal, 1));
            unlockedQuestions.Add(character.currentAct.getQuestionByID(firstVal, 2));
        }
        else if (incomingQuestionWasTwoDigit)
        {
            unlockedQuestions.Add(character.currentAct.getQuestionByID(firstVal, lastQuestionID.val2, 1));
            unlockedQuestions.Add(character.currentAct.getQuestionByID(firstVal, lastQuestionID.val2, 2));
        }
        else
            print("eeerrrhhhh");

        return unlockedQuestions;
    }


    void updateAvailableQuestions(Character talkPartner)
    {
        currentlyAvailableQuestions.Clear(); // remove current questions from list of available questions

        bool nothingAskedYet = talkPartner.getLastAskedQuestionFromCurrentAct() == null;

        if (nothingAskedYet)
            currentlyAvailableQuestions.Add(talkPartner.getFirstQuestionInCurrentAct());
        else
        {
            if (talkPartner.getLastAskedQuestionFromCurrentAct().isEndPoint)
            {
                conversationUiHandler.showNotification($"act: {talkPartner.currentAct.actNumber}", 1000);
                talkPartner.goToNextAct();
                return;
            }
            else
            {
                bool latestQuestionBranchesOut = talkPartner.getLastAskedQuestionFromCurrentAct().hasBranches;

                if (latestQuestionBranchesOut)
                {
                    List<Question> unlockedQuestions = getUnlockedQuestions(talkPartner, talkPartner.getLastAskedQuestionFromCurrentAct());
                    currentlyAvailableQuestions.AddRange(unlockedQuestions);
                }
                else
                {
                    Question unlockedQuestion = getUnlockedQuestion(talkPartner, talkPartner.getLastAskedQuestionFromCurrentAct());
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

        character.questionsAsked.Add(question);
        question.hasBeenSaid = true;
        conversationUiHandler.updateAnswerField(question.answer);
        updateAvailableQuestions(character);
        conversationUiHandler.updateQuestionButtons();
        conversationPage.updateTileText(currentConversationCharacter);
    }

    private void playVoiceClip(Character character)
    {
        if (character.gender == "female")
            playRandomFemaleVoiceClip();
        else
            playRandomMaleVoiceClip();
    }
    // REPONSIBILITY: playing sound
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
        playVoiceClip(character);

        currentConversationCharacter = character;

        character.hasBeenTalkedTo = true;

        updateAvailableQuestions(character);

        conversationUi.SetActive(true);
        conversationUiHandler.displayOpeningLine(character);
        conversationUiHandler.setCharacterImage(character);
    }


    public void leaveConversation()
    {
        currentConversationCharacter = null;
        conversationUi.SetActive(false);
    }

    // RESPONSIBILITY: setting constants
    void setInitialConversations()
    {
        gameManager.mary.questionsInCurrentAct = Constants.maryQuestions;
        gameManager.officer.questionsInCurrentAct = Constants.officerQuestions;
        gameManager.harry.questionsInCurrentAct = Constants.harryQuestions;
        gameManager.james.questionsInCurrentAct = Constants.jamesQuestions;
    }
}
