using UnityEngine;
using UnityEngine.UI;

public class ConversationsPage : MonoBehaviour
{
    public GameObject maryTile, jamesTile, officerTile, harryTile;
    public ConversationManager conversationManager;

    public void updateTileText(Character character)
    {
        // GameObject tile = character
        string tileText = character.getLastAskedQuestion().answer.sentence;
        // tile.transform.GetChild(2).GetComponent<Text>().text = tileText;
    }
}
