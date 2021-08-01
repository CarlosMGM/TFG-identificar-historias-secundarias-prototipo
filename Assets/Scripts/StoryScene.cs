using Narrative_Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScene
{    
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

    public bool dialogConsumed = false;

    public StoryScene()
    {
        itemToGive = null;
        itemToTake = null;
    }

    public void ConsumeDialog()
    {
        dialogConsumed = true;
    }

    public bool AreDialogsConsumed()
    {
        return dialogConsumed;
    }
}
