using Narrative_Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScene
{
    // Lista de diálogos disponibles
    private List<MonoBehaviour> _dialogues;
    
    // Lugar donde ocurre la escena
    public Place place;

    public Quest quest { get; set; }
    
    // Siguiente escena
    public StoryScene nextScene;

    public GameObject character;
    
    // Si la escena está consumida o no.
    public bool _used;

    // class Item
    public Item itemToGive;
    public Item itemToTake;

    public StoryScene()
    {
        itemToGive = null;
        itemToTake = null;
    }

    public void StartDialogues()
    {
        /*Narrative_Engine.Quest engineQuest = NarrativeEngine.getChapterById(quest.questId);
        DialogManager.GetInstance().StartDialog(engineQuest.scenes[quest._sceneCount].dialogs[0], 0, character.gameObject);*/
    }
}
