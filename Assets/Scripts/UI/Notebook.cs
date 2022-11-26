using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// The Notebook class contains the player's current knowledge
/// this includes:
///     character sheet
///     conversation log
///     clues log
/// </summary>
public class Notebook : MonoBehaviour
{
    // BUTTONS
    public Button conversationsButton, deductionButton;

    // canvas groups for controlling the alpha and interactability
    public GameObject notebook, conversationsPage, conversationPage, deductionPage;
    public Transform conversationsParent;

    public GameObject[] pages;
    public AudioSource audioSource;
    public GameObject conversationTilePrefab;
    public ConversationUI conversationUiHandler;
    public Animator animator;
    public bool isOpen;
    void OnValidate()
    {
        notebook = this.gameObject;

        pages = new GameObject[] { conversationsPage, conversationPage, deductionPage };
    }


    void Start()
    {
        Button conversationsButton = this.conversationsButton.GetComponent<Button>();

        conversationsButton.onClick.AddListener(() => goToPage(conversationsPage, true));
    }

    public void goToPage(GameObject page, bool withSound)
    {
        if (withSound)
            audioSource.Play();

        // set active or not active to make sure on page does not cover the other
        foreach (GameObject _page in this.pages)
        {
            if (_page == page)
            {
                _page.SetActive(true);
            }
            else
            {
                _page.SetActive(false);
            }
        }
    }

    public void updateConversationTile(GameObject tile, Character character)
    {
        Answer lastAnswerGiven = character.getLastAskedQuestion().answer;
        tile.transform.GetChild(2).GetComponent<Text>().text = lastAnswerGiven.sentence;
    }

    public void addlineToConversationsLog(Character character, bool isPlayer, string line)
    {
        throw new System.NotImplementedException();
        // check wether the character exists in the conversations log
        // if it does add the lines to the character conversations
        // if the character does not exist
        // instantiate the characterConversation
        // add the conversation to the character conversations
        // if end of conversation, add a endOfConversation marker.
    }
}
