using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuestStarterNPC : NPC
{
    // Start is called before the first frame update

    public Quest quest;
    public Item givenItem;
    
    public override void Interact()
    {
        if(!(quest.activated || quest.used))
        {
            Debug.Log("starting quest " + quest);
            quest.activated = true;
            QuestManager.StartQuest(quest);
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
        quest.givenItem = givenItem;
    }

}
