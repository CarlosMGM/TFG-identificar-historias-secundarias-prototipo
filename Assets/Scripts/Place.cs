using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class Place : MonoBehaviour
{
    // Personajes existentes.
    private List<NPC> _characters = new List<NPC>();
    // Objetos localizados en el lugar.
    private List<Item> _items = new List<Item>();
    /*
    // Lugares vecinos.
    private List<Place> _neighbouringPlaces;
*/

    public Vector2Int _areaUpLeftTile;
    public Vector2Int _areaDownRightTile;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        var totalItems = FindObjectsOfType<Item>();
        foreach (var item in totalItems)
        {
            if(item.transform.position.x >= _areaUpLeftTile.x 
               && item.transform.position.y <= _areaUpLeftTile.y
               && item.transform.position.x <= _areaDownRightTile.x
               && item.transform.position.y >= _areaDownRightTile.y)
                _items.Add(item);
                
        }

        var totalNPCs = FindObjectsOfType<NPC>();
        foreach (var npc in totalNPCs)
        {
            if(npc.transform.position.x >= _areaUpLeftTile.x 
               && npc.transform.position.y <= _areaUpLeftTile.y
               && npc.transform.position.x <= _areaDownRightTile.x
               && npc.transform.position.y >= _areaDownRightTile.y)
                _characters.Add(npc);
                
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
