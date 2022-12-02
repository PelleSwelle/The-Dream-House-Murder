using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public GameObject uiObject;
    public ConversationUI conversationUI;
    public ConversationsPage conversationPage;

    public AudioClip[] voiceLinesMale, voiceLinesFemale;
    public AudioSource voiceSource;

    public Character currentConversationCharacter;
    public List<Question> currentlyAvailableQuestions;

    public GameManager gameManager;

    void Start()
    {
        currentlyAvailableQuestions = new List<Question>();
    }

    /// <summary> gets the unlocked question in the case there is only one </summary>
    Question getUnlockedQuestion(Character character, Question previousQuestion)
    {
        if (character.currentAct != character.acts[1])
        {
            Question unlockedQuestion = character.currentAct.conversation.Questions.Find(x => x.ID.val1 == previousQuestion.ID.val1 + 1);
            return unlockedQuestion;
        }
        else
        {
            Question unlockedQuestion = character.currentAct.conversation.Questions.Find(
                x => x.ID.val1 == previousQuestion.ID.val1 + 1
                && x.ID.val2 == previousQuestion.ID.val2);
            return unlockedQuestion;
        }

    }

    /// <summary> gets a list of the unlocked questions in the case there are two </summary>
    List<Question> getUnlockedQuestions(Character character, Question question)
    {
        List<Question> unlockedQuestions = new List<Question>();
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

        Character characterToReturn = gameManager.characters.Find(x => x.firstName == characterName);
        print($"Character unlocked: {characterToReturn.firstName}");
        return characterToReturn;
    }

    void updateAvailableQuestions(Character talkPartner)
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
                if (talkPartner == gameManager.officer && talkPartner.currentAct == talkPartner.acts[1])
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
                    List<Question> unlockedQuestions = getUnlockedQuestions(talkPartner, talkPartner.currentAct.conversation.getLastAskedQuestion());
                    currentlyAvailableQuestions.AddRange(unlockedQuestions);
                }
                else
                {
                    Question unlockedQuestion = getUnlockedQuestion(talkPartner, talkPartner.currentAct.conversation.getLastAskedQuestion());
                    currentlyAvailableQuestions.Add(unlockedQuestion);
                }
            }

        }
        conversationUI.updateQuestionButtons();
    }

    public void askQuestion(Question question, Character character)
    {
        playRandomVoiceClip(character);

        character.questionsAsked.Add(question);
        question.hasBeenSaid = true;

        if (character.currentAct == character.acts[0] && gameManager.actIsOverForAll())
        {
            gameManager.officer.enterSecondAct();
        }

        conversationUI.updateAnswerField(question.answer);
        updateAvailableQuestions(character);
        conversationUI.updateQuestionButtons();
        conversationPage.updateTileText(character);
    }

    private void playRandomVoiceClip(Character character)
    {
        voiceSource.Stop();
        print($"Character: {character.firstName}");
        if (character.gender == "female")
            playRandomFemaleVoiceClip();
        else
            playRandomMaleVoiceClip();
    }

    public void playRandomFemaleVoiceClip()
    {
        int i = Random.Range(0, voiceLinesFemale.Length);
        voiceSource.PlayOneShot(voiceLinesFemale[i]);
    }

    public void playRandomMaleVoiceClip()
    {
        int i = Random.Range(0, voiceLinesMale.Length);
        voiceSource.PlayOneShot(voiceLinesMale[i]);
    }

    public void initConversation(Character character)
    {
        playRandomVoiceClip(character);

        currentConversationCharacter = character;
        character.hasBeenTalkedTo = true;

        conversationUI.setCharacterImage(character);
        conversationUI.toggleUI();

        // before the officer has been talked to.
        if (currentConversationCharacter != gameManager.officer)
        {
            if (gameManager.officer.hasBeenTalkedTo)
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
    }

    public void leaveConversation()
    {
        currentConversationCharacter = null;
        conversationUI.toggleUI();
    }
}
