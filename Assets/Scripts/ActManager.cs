using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    void setCharacterAct(Character character, int i)
    {
        character.currentAct = character.acts[i];
    }
}
