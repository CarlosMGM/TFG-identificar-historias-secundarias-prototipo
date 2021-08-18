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
    public int dialogIndex = -1;
    public bool dialogConsumed = false;

    public override void Interact()
    {
        if(CanInteract())
        {
            Debug.Log("Finishing quest " + quest);
            Narrative_Engine.Quest engineQuest = NarrativeEngine.getChapterById(quest.questId);
            DialogManager.GetInstance().StartDialog(engineQuest.scenes[quest._sceneCount].dialogs[dialogIndex], 0, this);
            QuestManager.DoScene(quest);
            /*if (quest.used)
            {
                var nextQuest = NarrativeEngine.getNextChapterById(quest.questId);
                QuestManager.LoadQuest(nextQuest);
            }*/
        }
        else
        {
            if(TryGetComponent<QuestStarterNPC>(out var npc))
                npc.Interact();
            else
                base.Interact();
        }
    }

    public override void DialogEnded(bool success)
    {
        Debug.Log("continuing quest? " + success);
        if(!dialogConsumed) dialogConsumed = success;
    } // DialogEnded

    protected new void Start()
    {
        base.Start();
        //quest = new Quest();
        // quest.activated = true;
       // quest.itemToTake = itemToGive;
    }

    public override void Update()
    {
        base.Update();
        quest.ProgressQuest();
    }

    protected override bool NeedsToTeleport()
    {
        return base.NeedsToTeleport() && dialogIndex == quest._sceneCount;
    }

    public override bool CanInteract()
    {
        return quest.activated && !quest.used && quest._sceneCount == sceneNumber && !dialogConsumed;
    }
}
