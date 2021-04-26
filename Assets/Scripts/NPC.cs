using UnityEngine;
public class NPC : Interactable
{


    public override void Interact()
    {
        Debug.Log("Saying dialogue I guess");
    }

    public virtual void DialogEnded(){}
}
