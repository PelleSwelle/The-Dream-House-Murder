using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Question")]
public class Question : ScriptableObject
{
    public string question;
    public Answer unlock;
}
