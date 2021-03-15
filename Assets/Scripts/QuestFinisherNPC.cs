using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinisherNPC : NPC
{
    // Start is called before the first frame update
    
    public Quest quest;
    public Item itemToGive;
    
    public override void Interact()
    {
        if(quest.activated && !quest.used)
        {
            Debug.Log("Finishing quest " + quest);
            QuestManager.EndQuest(quest);
        }
        else
        {
            base.Interact();
        }
    }
    
    new void Start()
    {
        base.Start();
        quest = new Quest();
        quest.activated = true;
        quest.itemToGive = itemToGive;
    }
}
