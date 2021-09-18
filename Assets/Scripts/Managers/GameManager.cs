using UnityEngine;
using TFGNarrativa.FileManagement;
using Narrative_Engine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private class NamesHolder
    {
        public string[] names = null;
    }

    [FormerlySerializedAs("m_names")] 
    public Names names;
    private NamesHolder stringNames; 

    private static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            var path = Application.dataPath;

            Debug.Log("path: " + path);

            path = path.Remove(0, 2);
            
            NarrativeEngine.Init(path + "/JSON");

            DontDestroyOnLoad(gameObject); // Make it don't destroy
        } // if
    } // Awake

    private void Start()
    {
        QuestManager.LoadQuest(NarrativeEngine.GetMainQuest());
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
