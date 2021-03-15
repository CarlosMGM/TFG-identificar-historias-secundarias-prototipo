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
            Inventory.AddItem(quest.givenItem);
            Debug.Log("¡Obtuviste " + quest.givenItem + "!");
        }
        
        quest.startScene._used = true;
    }

    public static void EndQuest(Quest quest)
    {
        // Quita objeto/da recompensa?/rutinas finales, 
        quest.BeginScene(quest.endScene);
        if (!(quest.itemToGive is null))
        {
            if (Inventory.FindItem(quest.itemToGive))
            {
                Debug.Log("Entregaste " + quest.itemToGive);
                Inventory.RemoveItem(quest.itemToGive);
            }
            else
            {
                Debug.Log("¡Te falta " + quest.itemToGive + "!");
                return;
            }
        }
        
        quest.endScene._used = true;
        quest.used = true;
    }
}
