using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
    public Interactable objectToInteract = null;
    
    
    
    public void InputReceived(InputAction.CallbackContext c)
    {
        Debug.Log("Hey, se ha pulsado una tecla");
        if (objectToInteract != null)
        {
            Debug.Log("Interactuando con " + objectToInteract.gameObject);
            objectToInteract.InputReceived(c);
        }
    }
}
