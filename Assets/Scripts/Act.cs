using System.Collections.Generic;
using UnityEngine;

public class Act
{
    public int actNumber;
    public IConversation conversation;
    public Act(int _actNumber, IConversation _conversation)
    {
        actNumber = _actNumber;
        conversation = _conversation;
    }

    public bool isFinished()
    {
        List<Question> endPoints = conversation.getEndPoints();
        Question hasBeenSaid = endPoints.Find(x => x.hasBeenSaid == true);
        
        if (hasBeenSaid == null)
            return false;
        else
        {
            Debug.Log($"finished act: {actNumber}");
            return true;
        }
    }
}
