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
        
        quest.ProgressQuest();
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
        quest.ProgressQuest();
    }
    
    public static void LoadQuests(Narrative_Engine.Quest engineQuest)
    {
        /*
         * Paso 1: Leer quest de motor.
         * Paso 2: Crear quest de Unity.
         * Paso 3: Asignarla a los NPCs correspondientes.
         * Paso 4: Cargar los dialogos.
         */

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

            if(engineScene.dialogs.Count > 0)
            {
                AssignCharacter(scene, GameObject.Find(engineScene.dialogs[0].init), starter, index);
            }

                
            previousScene = scene;
                
            quest.storyScenes.Add(scene);

            index++;
        }
    }

    private static void AssignCharacter(StoryScene scene, GameObject character, bool starter, int sceneNumber)
    {
        if (starter)
        {
            var npc = character.AddComponent<QuestStarterNPC>();
            npc.quest = scene.quest;
            npc.itemToGive = scene.itemToGive;
            npc.enabled = true;
        }
        else
        {
            var npc = character.AddComponent<QuestFinisherNPC>();
            npc.quest = scene.quest;
            npc.itemToGive = scene.itemToGive;
            npc.itemToTake = scene.itemToTake;
            npc.enabled = true;
            npc.sceneNumber = sceneNumber;
        }
    }
}
