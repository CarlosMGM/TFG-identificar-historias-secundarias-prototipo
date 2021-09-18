using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
    public Interactable objectToInteract = null;

    
    public void InputReceived(InputAction.CallbackContext c)
    {
        if (objectToInteract != null && c.performed && !DialogManager.GetInstance().IsOnDialog())
        {
            objectToInteract.InputReceived(c);
        }
    }
}
