using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
    public Interactable objectToInteract = null;

    
    public void InputReceived(InputAction.CallbackContext c)
    {
        if (objectToInteract != null && c.performed)
        {
            if (objectToInteract.GetComponent<QuestStarterNPC>() && !DialogManager.GetInstance().IsOnDialog())
            {
                DialogManager.GetInstance().StartDialog(0, 0);
            }
            objectToInteract.InputReceived(c);
        }
    }
}
