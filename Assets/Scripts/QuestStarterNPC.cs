﻿using UnityEngine;
using UnityEngine.Serialization;
using Narrative_Engine;

public class QuestStarterNPC : NPC
{
    // Start is called before the first frame update

    public Quest quest;
    [FormerlySerializedAs("givenItem")] public Item itemToGive;
    public bool dialogConsumed; 
    
    public override void Interact()
    {
        if(!(quest.activated || quest.used) && !dialogConsumed)
        {
            Narrative_Engine.Quest engineQuest = NarrativeEngine.getChapterById(quest.questId);
            DialogManager.GetInstance().StartDialog(engineQuest.scenes[0].dialogs[0], 0, gameObject);
        } // if
        // Faltaría un else if con un diálogo básico
        else
        {
            base.Interact();
        } // else
    } // Interact

    public override void DialogEnded(bool success)
    {
        Debug.Log("starting quest " + quest);
        quest.activated = true;
        dialogConsumed = success;
        QuestManager.StartQuest(quest);
    } // DialogEnded

    protected new void Start()
    {
        base.Start();
    } // Start
} // QuestStarterNPC
