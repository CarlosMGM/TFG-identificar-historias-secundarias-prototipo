using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinisherNPC : NPC
{
    // Start is called before the first frame update
    
    public Quest quest;
    public override void Interact()
    {
        Debug.Log("Finishing quest " + quest);
        QuestManager.EndQuest(quest);
    }
    
    void Start()
    {
        base.Start();
        quest = new Quest();
    }
}
