using Narrative_Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    //public string nextQuestId;
    public string questId;
    
    // class "Scene"
    public StoryScene startScene;
    public StoryScene endScene { get; }

    public List<StoryScene> storyScenes { get; } = new List<StoryScene>();

    public int _sceneCount = 0;
    
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
        scene.ConsumeDialog();
    }

    public StoryScene CurrentStoryScene()
    {
        return storyScenes[_sceneCount];
    }

    public void ProgressQuest()
    {
        if (CanProgressQuest())
        {
            storyScenes[_sceneCount]._used = true;
            _sceneCount++;
            if (_sceneCount == storyScenes.Count)
            {
                used = true;
                var nextQuest = NarrativeEngine.getNextChapterById(questId);
                if(nextQuest != null) QuestManager.LoadQuest(nextQuest);
            }
        }
    }

    private bool CanProgressQuest()
    {
        return _sceneCount < storyScenes.Count
            && !storyScenes[_sceneCount].place.PlayerIsInPlace()
            && storyScenes[_sceneCount].AreDialogsConsumed();
    }
}
