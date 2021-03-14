using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuestStarterNPC : NPC
{
    // Start is called before the first frame update

    public Quest quest;
    
    public override void Interact()
    {
        Debug.Log("starting quest " + quest);
        QuestManager.StartQuest(quest);
    }
    
    
    void Start()
    {
        base.Start();
        quest = new Quest();
    }

}
