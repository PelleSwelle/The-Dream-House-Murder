using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ConversationManager : MonoBehaviour
{
    public ConversationUI conversationUI;
    public ConversationsPage conversationPage;

    public ICharacter currentConversationCharacter;
    public List<Question> currentlyAvailableQuestions;

    public GameManager game;
    public GameObject notebookButton;
    public bool isInConversation = false;

    public static event Action<ICharacter> onSpeak;

    void Start()
    {
        currentlyAvailableQuestions = new List<Question>();
    }

    Question getUnlockedQuestion(ICharacter character)
    {
        Question previousQuestion = character.getLastAskedQuestionFromCurrentAct();
        if (character.currentAct != character.acts[1]) // if character is not in second act
        {
            Question unlockedQuestion = character.currentAct.conversation.Questions.Find(
                x => x.ID.val1 == previousQuestion.ID.val1 + 1);
            return unlockedQuestion;
        }
        else // if in second act
        {
            Question unlockedQuestion = character.currentAct.conversation.Questions.Find(
                x => x.ID.val1 == previousQuestion.ID.val1 + 1
                && x.ID.val2 == previousQuestion.ID.val2);
            return unlockedQuestion;
        }

    }

    List<Question> getUnlockedQuestions(ICharacter character)
    {
        List<Question> unlockedQuestions = new List<Question>();
        Question question = character.getLastAskedQuestionFromCurrentAct();
        QuestionID lastQuestionID = question.ID;

        int firstVal = lastQuestionID.val1 + 1;

        bool incomingQuestionWasSingleDigit = lastQuestionID.val2 == 0;
        bool incomingQuestionWasTwoDigit = lastQuestionID.val2 != 0 && lastQuestionID.val3 == 0;

        if (incomingQuestionWasSingleDigit)
        {
            unlockedQuestions.Add(character.currentAct.conversation.getQuestionByID(firstVal, 1));
            unlockedQuestions.Add(character.currentAct.conversation.getQuestionByID(firstVal, 2));
            if (character.currentAct.conversation.getQuestionByID(firstVal, 3) != null)
                unlockedQuestions.Add(character.currentAct.conversation.getQuestionByID(firstVal, 3));
        }
        else if (incomingQuestionWasTwoDigit)
        {
            unlockedQuestions.Add(character.currentAct.conversation.getQuestionByID(firstVal, lastQuestionID.val2, 1));
            unlockedQuestions.Add(character.currentAct.conversation.getQuestionByID(firstVal, lastQuestionID.val2, 2));
            if (character.currentAct.conversation.getQuestionByID(firstVal, lastQuestionID.val2, 3) != null)
                unlockedQuestions.Add(character.currentAct.conversation.getQuestionByID(firstVal, lastQuestionID.val2, 3));
        }
        else
            print("eeerrrhhhh");

        return unlockedQuestions;
    }

    Character getCharacterTalkedAbout()
    {
        string characterName = "";
        List<Question> questions = currentConversationCharacter.acts[1].conversation.Questions.FindAll(x => x.ID.val1 == 2);
        Question question = questions.Find(x => x.hasBeenSaid);

        if (question.sentence.Contains("Harry"))
            characterName = "Harry";
        else if (question.sentence.Contains("Mary"))
            characterName = "Mary";
        else if (question.sentence.Contains("James"))
            characterName = "James";

        Character characterToReturn = game.characters.Find(x => x.firstName == characterName);
        return characterToReturn;
    }

    void updateAvailableQuestions(ICharacter talkPartner)
    {
        currentlyAvailableQuestions.Clear();
        Question firstQuestionInCurrentRound = talkPartner.currentAct.conversation.getFirstQuestion();
        bool nothingAskedInCurrentRound = firstQuestionInCurrentRound.hasBeenSaid == false;

        if (nothingAskedInCurrentRound)
        {
            currentlyAvailableQuestions.Add(talkPartner.currentAct.conversation.getFirstQuestion());
        }
        else
        {
            if (talkPartner.currentAct.isFinished() == true)
            {
                if (talkPartner == game.officer && talkPartner.currentAct == talkPartner.acts[1])
                {
                    Character characterToUnlock = getCharacterTalkedAbout();
                    talkPartner.currentAct.conversation.onFinish(characterToUnlock);
                }
                else return;
            }
            else
            {
                bool latestQuestionBranchesOut = talkPartner.currentAct.conversation.getLastAskedQuestion().hasBranches;

                if (latestQuestionBranchesOut)
                {
                    List<Question> unlockedQuestions = getUnlockedQuestions(talkPartner);
                    currentlyAvailableQuestions.AddRange(unlockedQuestions);
                }
                else
                {
                    Question unlockedQuestion = getUnlockedQuestion(talkPartner);
                    currentlyAvailableQuestions.Add(unlockedQuestion);
                }
            }

        }
        conversationUI.updateQuestionButtons();
    }

    public void askQuestion(Question question, ICharacter character)
    {
        onSpeak?.Invoke(character);

        character.askedQuestions.Add(question);
        question.hasBeenSaid = true;

        // act 1 is over for all
        if (character.currentAct == character.acts[0] && game.actIsOverForAll())
        {
            game.officer.goToAct(1);
        }
        // act 2 is over for Harry, Mary & James
        if (game.act2IsOverForThree() && character != game.officer && character.currentAct == character.acts[1])
        {
            game.officer.goToAct(2);
        }

        if (character == game.officer && game.officer.acts[2].conversation.Questions[0].hasBeenSaid)
        {
            game.accusePanel.SetActive(true);
        }

        if (accusedSomeone())
        {
            game.endGame(game.getAccusedCharacter());
        }

        conversationUI.updateAnswerField(question.answer);
        updateAvailableQuestions(character);
        conversationUI.updateQuestionButtons();
        conversationPage.updateTileText(character);
    }

    bool accusedSomeone() // returns true if any of the "accuse" questions have been said
    {
        return game.officer.acts[2].conversation.getQuestionByID(2, 1, 0).hasBeenSaid
            || game.officer.acts[2].conversation.getQuestionByID(2, 2, 0).hasBeenSaid
            || game.officer.acts[2].conversation.getQuestionByID(2, 3, 0).hasBeenSaid;
    }


    public void initConversation(ICharacter character)
    {
        currentConversationCharacter = character;

        notebookButton.SetActive(false);
        onSpeak?.Invoke(character);

        character.hasMet = true;

        conversationUI.updateCharacterFields(character);
        conversationUI.toggleUI();

        // before the officer has been talked to.
        if (currentConversationCharacter != game.officer)
        {
            if (game.officer.hasMet)
            {
                updateAvailableQuestions(character);
                conversationUI.displayOpeningLine(character);
            }
            else
            {
                conversationUI.displayNothingToSay(character);
            }
        }
        else
        {
            updateAvailableQuestions(character);
            conversationUI.displayOpeningLine(character);
        }

        if (character == game.officer && character.currentAct == character.acts[1] && game.isWithVideo)
            game.cutsceneManager.playScene(1);
    }

    public void leaveConversation()
    {
        currentConversationCharacter = null;
        notebookButton.SetActive(true);
        conversationUI.toggleUI();
    }
}
