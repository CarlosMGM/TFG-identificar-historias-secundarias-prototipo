using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Narrative_Engine;
using UnityEngine;
using UnityEngine.UIElements;

public class Place : MonoBehaviour
{
    // Personajes existentes.
    public List<GameObject> characters;
    // Objetos localizados en el lugar.
    private List<Item> _items;
    // Lugares vecinos.
    private List<Place> _neighbouringPlaces;

    public Vector2 upLeftCoordinates;
    public Vector2 downRightCoordinates;

    private Transform _playerTransform;
    private bool _dialogsLoaded = false;
    private bool _questsLoaded = false;

    public List<Vector2> validCoordinates;

    public QuestTrigger questTrigger = null;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsInPlace())
        {
            if (!_dialogsLoaded)
            {
                _dialogsLoaded = true;
                LoadGenericDialogs();
            }
            if (!_questsLoaded)
            {
                _questsLoaded = true;
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
            if(_questsLoaded) _questsLoaded = false;
        }

    }

    public bool PlayerIsInPlace()
    {
        return _playerTransform.position.x > upLeftCoordinates.x
                       && _playerTransform.position.x < downRightCoordinates.x
                       && _playerTransform.position.y > downRightCoordinates.y
                       && _playerTransform.position.y < upLeftCoordinates.y;
    }

    private void LoadQuests()
    {
        var engineQuests = NarrativeEngine.getChaptersByPlace(name);
        foreach (var engineQuest in engineQuests)
        {
            QuestManager.LoadQuest(engineQuest);
        }
    }

    public void createTrigger(NPC npc)
    {
        questTrigger = gameObject.AddComponent<QuestTrigger>();
        questTrigger.trigger = npc;
    }
    private void LoadGenericDialogs()
    {
        DialogManager.GetInstance().LoadGenericDialogsByPlace(gameObject.name);
    }
}
