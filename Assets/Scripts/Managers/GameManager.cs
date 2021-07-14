using UnityEngine;
using TFGNarrativa.FileManagement;
using Narrative_Engine;

public class GameManager : MonoBehaviour
{
    private class NamesHolder
    {
        public string[] names = null;
    }

    public Names m_names;
    private NamesHolder m_stringNames; 

    private static GameManager g_instance;

    private void Awake()
    {
        if(g_instance == null)
        {
            g_instance = this;

            var path = Application.dataPath;

            path = path.Remove(0, 2);
            
            NarrativeEngine.init(path + "/JSON");

            DontDestroyOnLoad(gameObject); // Make it don't destroy
        } // if
    } // Awake

    public static GameManager GetInstance()
    {
        return g_instance;
    }

    public string GetCharacterName(int bundle, int index)
    {
        // Temporal, just 
        m_stringNames = JsonUtility.FromJson<NamesHolder>(GetInstance().m_names.m_names[bundle].text);

        // TODO: Check if index is not out of bounds
        return m_stringNames.names[index];
    }
}
