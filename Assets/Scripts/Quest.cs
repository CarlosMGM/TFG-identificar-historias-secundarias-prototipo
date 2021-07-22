using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string nextQuestId;
    
    // class "Scene"
    public StoryScene startScene;
    public StoryScene endScene { get; }

    public List<StoryScene> storyScenes { get; } = new List<StoryScene>();

    private int _sceneCount = 0;
    
    // class "State"?
    public MonoBehaviour playerState;
    /*
    // class NPC:
    public NPC questGiver;
    public NPC questFinisher;
    */
    
    
    // Class QuestType?
    // public MonoBehaviour questType;

    public bool used;
    public bool activated;
    
    // Start is called before the first frame update
    public Quest()
    {

        startScene = new StoryScene();
        endScene = new StoryScene();

        used = false;
    }


    public void BeginScene(StoryScene scene)
    {
        scene.StartDialogues();
    }

    public StoryScene CurrentStoryScene()
    {
        return storyScenes[_sceneCount];
    }

    public void ProgressQuest()
    {
        storyScenes[_sceneCount]._used = true;
        _sceneCount++;
        if (_sceneCount == storyScenes.Count)
            used = true;
    }
}
