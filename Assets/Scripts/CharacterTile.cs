using UnityEngine;

public class CharacterTile : MonoBehaviour
{
    public bool isPopulated;

    public void onPopulate()
    {
        this.isPopulated = true;
    }
}
