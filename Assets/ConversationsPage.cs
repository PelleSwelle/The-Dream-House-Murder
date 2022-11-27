using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConversationsPage : MonoBehaviour
{
    public ConversationManager conversationManager;
    public ConversationTile maryTile, jamesTile, officerTile, harryTile;
    private List<ConversationTile> tiles;

    void Start()
    {
        tiles = new List<ConversationTile>() { maryTile, jamesTile, officerTile, harryTile };
    }

    public void updateTileText(Character character)
    {
        var tile = tiles.Find(x => x.character == character);
        tile.updateText(character.getLastAskedQuestion().answer);
    }
}
