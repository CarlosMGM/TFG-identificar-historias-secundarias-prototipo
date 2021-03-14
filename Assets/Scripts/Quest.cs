using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    // clase "Lugar"
    public Place origin;
    public Place goal;
    
    // class "Scene"
    public StoryScene startScene;
    public StoryScene endScene;
    
    // class "State"?
    public MonoBehaviour playerState;
    /*
    // class NPC:
    public NPC questGiver;
    public NPC questFinisher;
    */
    
    // class Item
    public Item givenItem;
    public Item itemToGive;
    
    // Class QuestType?
    // public MonoBehaviour questType;

    public bool used;
    
    // Start is called before the first frame update
    public Quest()
    {

        startScene = new StoryScene();
        endScene = new StoryScene();

        givenItem = null;
        itemToGive = null;

        used = false;
    }


    public void BeginScene(StoryScene scene)
    {
        scene.StartDialogues();
    }
}
