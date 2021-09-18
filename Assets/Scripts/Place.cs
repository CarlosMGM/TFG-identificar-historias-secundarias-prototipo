using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Managers;
using Narrative_Engine;
using UnityEngine;
using UnityEngine.UIElements;

public class Place : MonoBehaviour
{
    // Personajes existentes.
    public List<GameObject> characters;
    // Objetos localizados en el lugar.
    private List<Item> items;
    // Lugares vecinos.
    private List<Place> neighbouringPlaces;

    public Vector2 upLeftCoordinates;
    public Vector2 downRightCoordinates;

    private Transform playerTransform;
    private bool dialogsLoaded = false;
    private bool questsLoaded = false;

    public List<Vector2> validCoordinates;

    public QuestTrigger questTrigger = null;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsInPlace())
        {
            if (!dialogsLoaded)
            {
                dialogsLoaded = true;
                LoadGenericDialogs();
            }
            if (!questsLoaded)
            {
                questsLoaded = true;
                LoadQuests();
            }

            if (!(questTrigger is null))
            {
                questTrigger.ActivateTrigger();
                if (questTrigger.enabled == false)
                {
                    Destroy(questTrigger);
                    questTrigger = null;
                }
            }
        }
        else
        {
            if(questsLoaded) questsLoaded = false;
        }

    }

    public bool PlayerIsInPlace()
    {
        return playerTransform.position.x > upLeftCoordinates.x
                       && playerTransform.position.x < downRightCoordinates.x
                       && playerTransform.position.y > downRightCoordinates.y
                       && playerTransform.position.y < upLeftCoordinates.y;
    }

    private void LoadQuests()
    {
        var engineQuests = NarrativeEngine.GetChaptersByPlace(name);
        foreach (var engineQuest in engineQuests)
        {
            QuestManager.LoadQuest(engineQuest);
        }
    }

    public void CreateTrigger(NPC npc)
    {
        questTrigger = gameObject.AddComponent<QuestTrigger>();
        questTrigger.trigger = npc;
    }
    private void LoadGenericDialogs()
    {
        DialogManager.GetInstance().LoadGenericDialogsByPlace(gameObject.name);
    }
}
