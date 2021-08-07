using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narrative_Engine;

public class QuestManager
{
    public static void StartQuest(Quest quest)
    {
        // Da objeto/inicia las rutinas, etc
        quest.BeginScene(quest.startScene);
        if (!(quest.startScene.itemToGive is null))
        {
            Inventory.AddItem(quest.startScene.itemToGive);
            Debug.Log("¡Obtuviste " + quest.startScene.itemToGive + "!");
        }
    }

    public static void DoScene(Quest quest)
    {
        // Da objeto/inicia las rutinas, etc
        quest.BeginScene(quest.CurrentStoryScene());
        
        if (!(quest.endScene.itemToTake is null))
        {
            if (Inventory.FindItem(quest.endScene.itemToTake))
            {
                Debug.Log("Entregaste " + quest.endScene.itemToTake);
                Inventory.RemoveItem(quest.endScene.itemToTake);
            }
            else
            {
                Debug.Log("¡Te falta " + quest.endScene.itemToTake + "!");
                return;
            }
        }
        
        if (!(quest.CurrentStoryScene().itemToGive is null))
        {
            Inventory.AddItem(quest.startScene.itemToGive);
            Debug.Log("¡Obtuviste " + quest.startScene.itemToGive + "!");
        }
    }
    
    public static void LoadQuest(Narrative_Engine.Quest engineQuest)
    {
        /*
         * Paso 1: Leer quest de motor.
         * Paso 2: Crear quest de Unity.
         * Paso 3: Asignarla a los NPCs correspondientes.
         * Paso 4: Cargar los dialogos.
         */

        Debug.Log("loading quest " + engineQuest.m_id);

        Quest quest = new Quest();

        //quest.nextQuestId = engineQuest.m_next;
        quest.questId = engineQuest.m_id;

        StoryScene previousScene = null;

        int index = 0;
            
        foreach (var engineScene in engineQuest.scenes)
        {
            StoryScene scene = new StoryScene();
            scene.quest = quest;
            scene.place = GameObject.Find(engineScene.m_place).GetComponent<Place>();

            bool starter = previousScene is null;

            if (!starter)
                previousScene.nextScene = scene;
            else
                quest.startScene = scene;
                
            if (engineScene.m_itemToGive != "")
                scene.itemToGive = GameObject.Find(engineScene.m_itemToGive).GetComponent<Item>();
                    
            if (engineScene.m_itemToTake != "")
                scene.itemToGive = GameObject.Find(engineScene.m_itemToTake).GetComponent<Item>();

                
            DialogManager.GetInstance().loadDialogues(engineScene);
            int dialogIndex = 0;
            foreach(var dialog in engineScene.dialogs)
            {
                GameObject character;
                if (dialog.init == "MainCharacter")
                {
                    Debug.Log("Pensamiento");
                    character = scene.place.gameObject;
                }
                else
                    character = GameObject.Find(dialog.init);
                if (!(character is null))
                {
                    var npc = AssignCharacter(scene, character, starter, index, dialogIndex);

                    if (npc.place is null)
                        npc.place = scene.place;
                    
                    if (!(scene.place.characters.Exists(x => npc.gameObject == x)))
                    {
                        npc.Teleport(scene.place);
                    }

                    if (dialog.init == "MainCharacter")
                    {
                        character.GetComponent<Place>().createTrigger(npc);
                    }
                }
                dialogIndex++;
            }

                
            previousScene = scene;
                
            quest.storyScenes.Add(scene);

            index++;
        }
    }

    private static NPC AssignCharacter(StoryScene scene, GameObject character, bool starter, int sceneNumber, int dialogIndex)
    {
        Place place = null;
        NPC previousNPC = character.GetComponent<NPC>();
        if (previousNPC != null)
            place = previousNPC.place;
        if (starter)
        {
            QuestStarterNPC npc = character.GetComponent<QuestStarterNPC>();
            if (npc == null) 
                npc = character.AddComponent<QuestStarterNPC>();
            npc.quest = scene.quest;
            npc.itemToGive = scene.itemToGive;
            npc.enabled = true;
            npc.place = place;
            return npc;
        }
        else
        {
            QuestFinisherNPC npc = character.GetComponent<QuestFinisherNPC>();;
            if (npc == null) 
                npc = character.AddComponent<QuestFinisherNPC>();
            npc.quest = scene.quest;
            npc.itemToGive = scene.itemToGive;
            npc.itemToTake = scene.itemToTake;
            npc.enabled = true;
            npc.sceneNumber = sceneNumber;
            if(npc.dialogIndex == -1) npc.dialogIndex = dialogIndex;
            npc.place = place;
            return npc;
        }
        
    }
}
