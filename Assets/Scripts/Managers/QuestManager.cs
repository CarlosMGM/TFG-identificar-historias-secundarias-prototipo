using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StartQuest(Quest quest)
    {
        // Da objeto/inicia las rutinas, etc
        quest.BeginScene(quest.startScene);
        if (!(quest.givenItem is null))
        {
            ; // Dar objeto}
        }
        
        quest.startScene._used = true;
    }

    public static void EndQuest(Quest quest)
    {
        // Quita objeto/da recompensa?/rutinas finales, 
        quest.BeginScene(quest.endScene);
        if (!(quest.itemToGive is null))
        {
            ; // Quitar objeto, si se tiene. Si no, ups, no puedes acabar la quest.
        }
        
        quest.endScene._used = true;
        quest.used = true;
    }
}
