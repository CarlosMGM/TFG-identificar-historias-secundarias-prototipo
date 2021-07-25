using System.Collections;
using System.Collections.Generic;
using Narrative_Engine;
using UnityEngine;

public class QuestFinisherNPC : NPC
{
    // Start is called before the first frame update
    
    public Quest quest;
    public Item itemToGive;
    public Item itemToTake;
    public int sceneNumber;
    
    public override void Interact()
    {
        if(quest.activated && !quest.used && quest._sceneCount == sceneNumber)
        {
            Debug.Log("Finishing quest " + quest);
            QuestManager.DoScene(quest);
            if (quest.used)
            {
                var nextQuest = NarrativeEngine.getNextChapterById(quest.questId);
                QuestManager.LoadQuests(nextQuest);
            }
        }
        else
        {
            base.Interact();
        }
    }
    
    protected new void Start()
    {
        base.Start();
        quest = new Quest();
        quest.activated = true;
       // quest.itemToTake = itemToGive;
    }
}
