using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConversationsPage : MonoBehaviour
{
    public GameManager gameManager;
    public Button maryButton, jamesButton, officerButton, harryButton;
    public ConversationTile maryTile, jamesTile, officerTile, harryTile;
    public MessengerPage messengerPage;

    public Notebook notebook;
    private List<ConversationTile> tiles;

    void Start()
    {
        maryTile.character = gameManager.mary;
        jamesTile.character = gameManager.james;
        officerTile.character = gameManager.officer;
        harryTile.character = gameManager.harry;

        tiles = new List<ConversationTile>() { maryTile, jamesTile, officerTile, harryTile };

        maryButton.onClick.AddListener(() => openConversationLog(maryTile.character));
        jamesButton.onClick.AddListener(() => openConversationLog(jamesTile.character));
        officerButton.onClick.AddListener(() => openConversationLog(officerTile.character));
        harryButton.onClick.AddListener(() => openConversationLog(harryTile.character));
    }

    void openConversationLog(Character character)
    {
        messengerPage.populate(character);
        notebook.goToPage(notebook.messengerPage);
    }

    public void updateTileText(Character character)
    {
        ConversationTile tile = tiles.Find(x => x.character == character);
        Answer answer = character.getLastAskedQuestion().answer;
        tile.updateText(answer);
    }
}
