using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{

    public NPC trigger;
    // Start is called before the first frame update

    public void ActivateTrigger()
    {
        if(trigger.CanInteract())
        {
            trigger.Interact();
            this.enabled = false;
        }
    }
}
