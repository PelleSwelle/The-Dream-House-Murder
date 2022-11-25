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
    public Button conversationsButton, cluesButton, CharactersButton;

    // canvas groups for controlling the alpha and interactability
    public GameObject notebook, cluesPage, conversationsPage, charactersPage, bioPage, conversationPage, deductionPage;
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

        pages = new GameObject[] { cluesPage, conversationsPage, charactersPage, bioPage, conversationPage, deductionPage };
    }


    void Start()
    {
        // EVENT LISTENERS ON TABS
        Button openCharactersTab = CharactersButton.GetComponent<Button>();
        Button openCluesTab = cluesButton.GetComponent<Button>();
        Button openConversationsTab = conversationsButton.GetComponent<Button>();

        openCharactersTab.onClick.AddListener(() => goToPage(charactersPage, true));
        openCluesTab.onClick.AddListener(() => goToPage(cluesPage, true));
        openConversationsTab.onClick.AddListener(() => goToPage(conversationsPage, true));
    }


    /// <summary>
    /// Navigation in the notebook
    /// opens a page in the notebook, while simultaneously closing the others
    /// </summary>
    /// <param name="page">CanvasGroup</param>
    public void goToPage(GameObject page, bool withSound)
    {
        print("opened page: " + page.name);
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

    // /// <summary>
    // /// add the character to the list of conversations
    // /// </summary>
    // /// <param name="character"></param>
    // public void addCharacterToConversations(Character character)
    // {
    //     if (conversationsPage.transform.GetChild(1).gameObject.activeInHierarchy)
    //     {
    //         // TODO: make this a function in notebook
    //         conversationsPage.transform.GetChild(1).gameObject.SetActive(false);
    //     }
    //     // Show notification that the character is added to conversations
    //     StartCoroutine(conversationUiHandler.showNotification($"{character.firstName} added to conversations log", .5f));
    //     GameObject tile = GameObject.Instantiate(conversationTilePrefab, new Vector3(0, 0, 0), Quaternion.identity, conversationsParent);
    //     tile.name = character.firstName + "Tile";
    //     tile.transform.GetChild(0).GetComponent<Image>().sprite = character.photo;
    //     tile.transform.GetChild(1).GetComponent<Text>().text = character.firstName;
    //     // updateConversationTile(tile, )
    // }

    /// <summary>
    /// Sets the conversation tile to reflect the latest sentence said.
    /// </summary>
    /// <param name="tile"></param>
    public void updateConversationTile(GameObject tile, Answer answer)
    {
        tile.transform.GetChild(2).GetComponent<Text>().text = answer.sentence;
    }

    /// <summary>
    /// Adds the lines of the conversation to the conversations log
    /// </summary>
    /// <param name="conversation"></param>
    /// <param name="question"></param>
    /// <param name="answer"></param>
    public void addConversationLine(Conversation conversation, bool isPlayer, string line)
    {
        throw new System.NotImplementedException();
        // check wether the character exists in the conversations log
        // if it does add the lines to the character conversations
        // if the character does not exist
        // instantiate the characterConversation
        // add the conversation to the character conversations
        // if end of conversation, add a endOfConversation marker.
    }

    /// <summary>
    /// Adds a testimony to the 'clues?' poge
    /// </summary>
    /// <param name="character"></param>
    /// <param name="answer"></param>
    public void addTestimony(Character character, Answer answer)
    {

    }
}
