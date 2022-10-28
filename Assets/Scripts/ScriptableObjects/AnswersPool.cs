using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: is this the way to go?
[CreateAssetMenu(menuName = "Answers")]
public class AnswersPool : ScriptableObject
{
    public Answer[] isGuiltyAnswers;
    public Answer[] isWitnessAnswers;
}
