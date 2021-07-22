using System.Collections;
using System.Collections.Generic;
using Narrative_Engine;
using UnityEngine;
using UnityEngine.UIElements;

public class Place : MonoBehaviour
{
    // Personajes existentes.
    private List<NPC> _characters;
    // Objetos localizados en el lugar.
    private List<Item> _items;
    // Lugares vecinos.
    private List<Place> _neighbouringPlaces;

    public Vector2 upLeftCoordinates;
    public Vector2 downRightCoordinates;

    private Transform _playerTransform;
    private bool _loaded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_loaded
            && _playerTransform.position.x > upLeftCoordinates.x
            && _playerTransform.position.x < downRightCoordinates.x
            && _playerTransform.position.y > downRightCoordinates.y
            && _playerTransform.position.y < upLeftCoordinates.y)
        {
            _loaded = true;
            LoadQuests();
        }
    }

    private void LoadQuests()
    {
        var engineQuests = NarrativeEngine.getChaptersByPlace(name);
        foreach (var engineQuest in engineQuests)
        {
            QuestManager.LoadQuests(engineQuest);
        }
    }
}
