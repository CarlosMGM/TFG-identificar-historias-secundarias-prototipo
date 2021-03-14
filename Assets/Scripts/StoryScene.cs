using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScene
{
    // Lista de diálogos disponibles
    private List<MonoBehaviour> _dialogues;
    
    // Lugar donde ocurre la escena
    public Place place;
    
    // Siguiente escena
    public StoryScene nextScene;
    
    // Si la escena está consumida o no.
    public bool _used;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogues()
    {
        // Suelta la rutina de diálogos correspondiente.
        Debug.Log("Bla bla bla");
    }
}
