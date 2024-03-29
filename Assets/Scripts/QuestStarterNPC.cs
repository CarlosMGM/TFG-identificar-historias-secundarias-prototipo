﻿using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Narrative_Engine;

public class QuestStarterNPC : NPC
{
    // Start is called before the first frame update

    public Quest quest;
    [FormerlySerializedAs("givenItem")] public Item itemToGive;
    public bool dialogConsumed = false; 
    
    public override void Interact()
    {
        if(CanInteract())
        {
            Narrative_Engine.Quest engineQuest = NarrativeEngine.GetChapterById(quest.questId);
            DialogManager.GetInstance().StartDialog(engineQuest.scenes[0].dialogs[0], 0, this);
        } // if
        // Faltaría un else if con un diálogo básico
        else
        {
            base.Interact();
        } // else
    } // Interact

    public override void DialogEnded(bool success)
    {
        Debug.Log("starting quest? " + success);
        quest.activated = success;
        if(!dialogConsumed) dialogConsumed = success;
        else
        {
            if (TryGetComponent<QuestFinisherNPC>(out var npc))
                npc.DialogEnded(success);
        }
        if (success) QuestManager.StartQuest(quest);
    } // DialogEnded

    protected new void Start()
    {
        base.Start();
    } // Start

    public override void Update()
    {
        base.Update();
        quest.ProgressQuest();
    }
    
    public override bool CanInteract()
    {
        return !(quest.activated || quest.used) && !dialogConsumed;
    }
    
} // QuestStarterNPC
